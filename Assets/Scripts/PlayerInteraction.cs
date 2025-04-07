using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public enum PlayerTool { None, Screwdriver, Wrench, Welder, Torpedo }

    Camera mainCam;
    [SerializeField] InteractableBase currentInteractable;
    [SerializeField] InteractableBase newInteractable;

    [Header("Tools")]
    [SerializeField] Animator toolAnimator;
    [SerializeField] Transform weaponDropTransform;
    [SerializeField] PlayerTool currentPlayerTool;
    [SerializeField] GameObject screwdriverGO;
    [SerializeField] GameObject screwdriverPrefab;
    [SerializeField] GameObject wrenchGO;
    [SerializeField] GameObject wrenchPrefab;
    [SerializeField] GameObject welderGO;
    [SerializeField] GameObject welderSparksPrefab;
    [SerializeField] GameObject welderPrefab;
    [SerializeField] GameObject torpedoGO;
    [SerializeField] GameObject torpedoPrefab;
    bool toolAnimation = false;

    [Header("Control Variables")]
    [SerializeField] float interactionDistance;
    [SerializeField] LayerMask interactableLayerMask;

    float escapeTimer;
    bool escapeTimerActive;
    [SerializeField] GameObject escText;

    float AITimer;

    private void Start()
    {
        mainCam = Camera.main;
        DisableAllToolsVisuals();
        escapeTimerActive = false;
        escText.SetActive(false);
    }

    private void Update()
    {
        if (escapeTimerActive)
            escapeTimer -= Time.deltaTime;

        if (escapeTimer <= 0)
        {
            escapeTimerActive = false;
            escText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escapeTimerActive)
            {
                escapeTimer = 3f;
                escapeTimerActive = true;
                escText.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        if (SubmarineManager.Instance.IsGameOver()) { return; }

        AnimateTools();

        if (Input.GetMouseButtonDown(1))
        {
            DisableAllToolsVisuals();
            GameObject droppedObject = null;
            switch (currentPlayerTool)
            {
                case PlayerTool.None:
                    break;
                case PlayerTool.Screwdriver:
                    droppedObject = screwdriverPrefab;
                    break;
                case PlayerTool.Wrench:
                    droppedObject = wrenchPrefab;
                    break;
                case PlayerTool.Welder:
                    droppedObject = welderPrefab;
                    break;
                case PlayerTool.Torpedo:
                    droppedObject = torpedoPrefab;
                    break;
                default:
                    break;
            }

            if (droppedObject != null)
            {
                GameObject droppedTool = Instantiate(droppedObject, weaponDropTransform.position, Quaternion.identity);
                Rigidbody droppedToolRigidbody = droppedTool.GetComponent<Rigidbody>();
                droppedToolRigidbody.velocity = mainCam.transform.forward * 5 * Time.deltaTime;
                currentPlayerTool = PlayerTool.None;
            }

        }

        if (Input.GetMouseButtonDown(0))
            if (currentInteractable != null)
            {
                currentInteractable.OnInteractionStart(currentPlayerTool);
                //Debug.Log(currentInteractable.name + " START INTERACT");
            }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteractionEnd();
                // Debug.Log(currentInteractable.name + " END INTERACT");
            }
        }

        AITimer -= Time.deltaTime;
        if (AITimer <= 0)
        {
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider != null)
                {
                    hit.collider.TryGetComponent<InteractableBase>(out newInteractable);
                    if (currentInteractable == newInteractable)
                        return;
                    else
                    {
                        if (currentInteractable != null)
                            currentInteractable.OnInteractionEnd();
                        currentInteractable = newInteractable;
                    }
                }
            }
            else if (hit.collider == null || newInteractable == null)
            {
                if (currentInteractable != null)
                    currentInteractable.OnInteractionEnd();
                currentInteractable = null;
            }
            AITimer = 0.1f;
        }
    }

    void AnimateTools()
    {
        if (toolAnimation)
            switch (currentPlayerTool)
            {
                case PlayerTool.None:
                    break;
                case PlayerTool.Screwdriver:
                    toolAnimator.Play("Screwdriver");
                    break;
                case PlayerTool.Wrench:
                    toolAnimator.Play("Wrench");
                    break;
                case PlayerTool.Welder:
                    welderSparksPrefab.SetActive(true);
                    break;
                case PlayerTool.Torpedo:
                    break;
                default:
                    break;
            }

        else
        {

            switch (currentPlayerTool)
            {
                case PlayerTool.Welder:
                    welderSparksPrefab.SetActive(false);
                    break;
                default:
                    toolAnimator.Play("Toolidle");
                    break;
            }
        }
    }

    public bool SetPlayerTool(PlayerTool tool)
    {
        if (currentPlayerTool != PlayerTool.None)
            return false;

        switch (tool)
        {
            case PlayerTool.None:
                DisableAllToolsVisuals();
                break;
            case PlayerTool.Screwdriver:
                screwdriverGO.SetActive(true);
                break;
            case PlayerTool.Wrench:
                wrenchGO.SetActive(true);
                break;
            case PlayerTool.Welder:
                welderGO.SetActive(true);
                break;
            case PlayerTool.Torpedo:
                torpedoGO.SetActive(true);
                break;
            default:
                break;
        }
        currentPlayerTool = tool;
        return true;
    }

    public void RemovePlayerTools()
    {
        currentPlayerTool = PlayerTool.None;
        DisableAllToolsVisuals();
    }

    void DisableAllToolsVisuals()
    {
        screwdriverGO.SetActive(false);
        wrenchGO.SetActive(false);
        welderGO.SetActive(false);
        torpedoGO.SetActive(false);
    }

    public void SetToolActive(bool active)
    {
        toolAnimation = active;
        //Debug.Log("PLAYER TOOL ACTIVE");
    }
}
