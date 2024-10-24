using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationManager
{
    private Animator animator;

    private bool animIsPlaying;
    private float animTimer;

    public BossAnimationManager(Animator _animatior)
    {
        animator = _animatior;
        animIsPlaying = false;
    }

    public void UpdateScript()
    {
        UpdateAnimTimer();
    }

    public void PlayAnimation(Anim newAnim)
    {
        switch (newAnim)
        {
            case Anim.Idle:
                PlayIdle();
                break;

            case Anim.Attack:
                PlayAttack();
                break;

            case Anim.Rotate:
                PlayRotate();
                break;

            case Anim.GetHit:
                PlayGetHit();
                break;

            case Anim.Death:
                PlayDeath();
                break;
        }
    }

    public enum Anim
    {
        Idle,
        Attack,
        Rotate,
        GetHit,
        Death
    }

    private void PlayRotate()
    {
        animator.SetBool("Rotate", true);
    }

    private void PlayAttack()
    {
        animator.SetBool("Throw", true);
        animIsPlaying = true;
        animTimer = 0.5f;
    }

    private void PlayGetHit()
    {
        animator.SetBool("GetHit", true);
        animIsPlaying = true;
        animTimer = 0.7f;
    }

    private void PlayDeath()
    {
        animator.SetTrigger("Death");
    }

    private void PlayIdle()
    {
        animator.SetBool("Throw", false);
        animator.SetBool("Rotate", false);
        animator.SetBool("GetHit", false);
        animator.ResetTrigger("Death");
    }

    private void UpdateAnimTimer()
    {
        if (animIsPlaying)
        {
            if (animTimer > 0)
                animTimer -= Time.deltaTime;
            else
            {
                PlayAnimation(Anim.Idle);
                animIsPlaying = false;
            }
        }
    }
}
