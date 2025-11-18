using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnim : SoldierAnim
{
    private SpriteRenderer[] spriteRenderers;
    private bool isFlip;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FSM = GetComponentInParent<FSM>();
        isFlip = spriteRenderers[0].flipX;

    }

    private void Update()
    {
        if ((FSM.targetSoldier != null))
        {
            float tempX = FSM.targetSoldier.transform.position.x - transform.position.x;
            foreach (SpriteRenderer animRenderer in spriteRenderers)
            {
                if (tempX < 0)
                    animRenderer.flipX = !isFlip;
                else animRenderer.flipX = isFlip;
                //animRenderer.transform.forward = Camera.main.transform.forward;

            }

        }

    }

   
}
