using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoDispenser : MonoBehaviour
{
    [SerializeField] GameObject torpedoPrefab;
    [SerializeField] Transform torpedoSpawnPoint;

    public void SpawnTorpedo()
    {
        Instantiate(torpedoPrefab, torpedoSpawnPoint);
    }
}
