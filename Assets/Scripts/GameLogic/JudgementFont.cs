using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementFont : MonoBehaviour
{
    public Sprite[] sprites;
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerJudge(int spriteNumber)
    {
        spriteRenderer.sprite = sprites[spriteNumber];
        animator.Play("Judgement", -1, 0f);
    }
}
