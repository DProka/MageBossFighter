
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerComboPart : MonoBehaviour
{
    [SerializeField] HealthBar playerComboBar;
    [SerializeField] TextMeshProUGUI comboText;

    [SerializeField] Image[] skillImages;

    private bool skill1Active = false;
    private bool skill2Active = false;

    public void Init()
    {
        ResetSkills();
    }

    public void UpdateComboBar(float maxValue, float currentValue)
    {
        playerComboBar.SetFillAmount(maxValue, currentValue);
        CheckSkills(maxValue, currentValue);
        SetComboText(currentValue);
    }

    private void SetComboText(float currentValue)
    {
        if (currentValue != 0)
            comboText.text = "Combo x" + currentValue;
        else
            comboText.text = "Combo";
    }

    #region Skills

    public void CheckSkills(float maxValue, float currentValue)
    {
        //if(currentValue > 0)
        //{
        //    float halfValue = maxValue / 2;

        //    if (currentValue >= halfValue)
        //    {
        //        if (!skill1Active)
        //        {
        //            skill1Active = true;
        //            skillImages[0].DOFade(1f, 0.5f);
        //        }
        //    }
        //    else if (currentValue >= maxValue)
        //    {
        //        if (!skill2Active)
        //        {
        //            skill2Active = true;
        //            skillImages[1].DOFade(1f, 0.5f);
        //        }
        //    }
        //}
        //else
        //{
        //    ResetSkills();
        //}

        if (currentValue > 0)
        {
            if (GameController.Instance.player.comboSkill1IsActive)
            {
                //skill1Active = true;
                skillImages[0].DOFade(1f, 0.5f);
            }

            if (GameController.Instance.player.comboSkill2IsActive)
            {
                //skill2Active = true;
                skillImages[1].DOFade(1f, 0.5f);
            }
        }
        else
            ResetSkills();
    }

    public void ResetSkills()
    {
        for (int i = 0; i < skillImages.Length; i++)
        {
            skillImages[i].DOFade(0.3f, 0);
        }

        skill1Active = false;
        skill2Active = false;
    }

    #endregion
}
