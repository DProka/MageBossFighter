
using UnityEngine;
using DG.Tweening;

public class MovePointCirclePrefab : MonoBehaviour
{
    [SerializeField] SpriteRenderer circleRenderer;

    private MovePointSettings settings;

    private bool isPlaying;

    public void Init(MovePointSettings _settings)
    {
        settings = _settings;

        ResetAnimation();
    }

    public void SetStatus(ArenaManager.PointStatus status)
    {
        ResetAnimation();

        switch (status)
        {
            case ArenaManager.PointStatus.Freeze:
                isPlaying = true;
                StartStatusAnimation(1f);
                break;
        }
    }

    public void ResetAnimation()
    {
        circleRenderer.enabled = false;
        isPlaying = false;
        circleRenderer.transform.DOScale(4f, 0);
    }

    private void StartAttackAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleAttackColor;

        StartZoomOutAnimation(animTime);
    }
    
    private void StartStatusAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleAttackColor;

        StartPunchAnimation(animTime);
    }
    
    private void StartBonusAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleBonusColor;

        StartPunchAnimation(animTime);
    }

    private void StartZoomOutAnimation(float animTime)
    {
        circleRenderer.transform.DOScale(4f, 0);
        circleRenderer.transform.DOScale(1.5f, animTime);//.OnComplete(() => ResetAnimation());
    }

    private void StartPunchAnimation(float animTime)
    {
        if(isPlaying)
            circleRenderer.transform.DOPunchScale(settings.circlePunchSize, animTime, 3).OnComplete(() => StartPunchAnimation(animTime));
    }
}
