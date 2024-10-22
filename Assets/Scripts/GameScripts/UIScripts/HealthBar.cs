
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image frontImage;

    public void ResetBar()
    {
        frontImage.transform.localScale = new Vector3(1, 1, 0);
    }

    public void SetHealth(float maxHealth, float currentHealth)
    {
        frontImage.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 0);
    }
}
