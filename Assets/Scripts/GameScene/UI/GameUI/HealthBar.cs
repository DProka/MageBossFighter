
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image frontImage;
    [SerializeField] float animationTime = 0.3f;

    public void SetFillAmount(float maxValue, float currentValue)
    {
        float percentage = currentValue / maxValue;
        frontImage.DOFillAmount(percentage, animationTime);
    }

    public void ResetBar()
    {
        frontImage.DOFillAmount(1f, 0);
    }
}
