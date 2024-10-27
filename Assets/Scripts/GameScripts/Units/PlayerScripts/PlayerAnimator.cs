
using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;

    public PlayerAnimator(Animator _animator)
    {
        animator = _animator;
    }

    public void StartAnimation(Clip animation)
    {
        switch (animation)
        {
            case Clip.Idle:
                PlayIdle();
                break;

            case Clip.Attack:
                PlayAttack();
                break;

            case Clip.MoveLeft:
                PlayStepLeft();
                break;

            case Clip.MoveRight:
                PlayStepRight();
                break;

            case Clip.GetHit:
                PlayGetHit();
                break;

            case Clip.Death:
                PlayDeath();
                break;
        }
    }

    public enum Clip
    {
        Idle,
        Attack,
        MoveLeft,
        MoveRight,
        GetHit,
        Death
    }

    private void PlayIdle()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Attack", false);
        animator.SetBool("GetHit", false);
        animator.SetBool("Death", false);
    }

    private void PlayAttack()
    {
        animator.SetBool("Attack", true);
    }

    private void PlayStepLeft()
    {
        animator.SetBool("Left", true);
    }

    private void PlayStepRight()
    {
        animator.SetBool("Right", true);
    }

    private void PlayGetHit()
    {
        animator.SetBool("GetHit", true);
    }

    private void PlayDeath()
    {
        animator.SetBool("Death", true);
    }

}
