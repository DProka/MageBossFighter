
using UnityEngine;
using DG.Tweening;

public class MovepointCirclePrefab : MonoBehaviour
{
    [SerializeField] SpriteRenderer circleRenderer;

    private MovePointSettings settings;

    public bool isPlaying;

    public void Init(MovePointSettings _settings)
    {
        settings = _settings;

        isPlaying = false;

        ResetAnimation();
    }

    public void StartAttackAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleAttackColor;

        StartZoomOutAnimation(animTime);
    }
    
    public void StartStatusAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleAttackColor;

        StartPunchAnimation(animTime);
    }
    
    public void StartBonusAnimation(float animTime)
    {
        circleRenderer.enabled = true;
        circleRenderer.color = settings.circleBonusColor;

        StartPunchAnimation(animTime);
    }

    public void ResetAnimation()
    {
        circleRenderer.enabled = false;
        isPlaying = false;
        circleRenderer.transform.DOScale(4f, 0);
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
