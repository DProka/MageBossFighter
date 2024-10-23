
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image frontImage;

    public void SetHealth(float maxHealth, float currentHealth)
    {
        float percentage = currentHealth / maxHealth;
        frontImage.DOFillAmount(percentage, 0.3f);
    }

    public void ResetBar()
    {
        frontImage.DOFillAmount(1f, 0);
    }
}
