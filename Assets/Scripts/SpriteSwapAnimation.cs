using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float animSpeed;
    float timer;
    int currentSprite = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animSpeed)
        {
            timer = 0;
            currentSprite++;
            if (currentSprite == sprites.Length)
                currentSprite = 0;

            spriteRenderer.sprite = sprites[currentSprite];
        }
    }
}
