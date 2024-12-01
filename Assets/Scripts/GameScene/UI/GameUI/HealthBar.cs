
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image frontImage;
    [SerializeField] float animationTime = 0.3f;

    public void SetHealth(float maxHealth, float currentHealth)
    {
        float percentage = currentHealth / maxHealth;
        frontImage.DOFillAmount(percentage, animationTime);
    }

    public void ResetBar()
    {
        frontImage.DOFillAmount(1f, 0);
    }
}
