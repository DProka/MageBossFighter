using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UITextReward : MonoBehaviour
{
    public float animationTime { get; private set; }

    private RectTransform parent;
    private TextMeshProUGUI[] childArray;
    private Vector3[] initialPosition;

    public void Init()
    {
        parent = GetComponent<RectTransform>();
        childArray = new TextMeshProUGUI[parent.childCount];
        initialPosition = new Vector3[childArray.Length];

        for (int i = 0; i < parent.childCount; i++)
        {
            childArray[i] = parent.GetChild(i).GetComponent<TextMeshProUGUI>();
        }

        animationTime = 0.3f + 0.5f + 0.3f + (0.1f * childArray.Length);

        ResetPile();

        SwitchActive(false);
    }

    public void StartPlusAnim(string text, float percentage, float animTime)
    {
        ResetPile();
        SwitchActive(true);

        percentage = (1 - percentage) * 10;
        float animStep = animTime / childArray.Length;
        float delay = 0f;

        for (int i = 0; i < childArray.Length; i++)
        {
            if (i <= percentage)
            {
                childArray[i].text = "+" + text;

                Sequence partSequence = DOTween.Sequence();

                partSequence
                .Append(childArray[i].transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack))
                .Append(childArray[i].transform.DOMove(new Vector3(childArray[i].transform.position.x, childArray[i].transform.position.y + 100f, 0f), 0.5f).SetEase(Ease.InCirc))
                .Append(childArray[i].transform.DOScale(0f, 0.3f).SetDelay(0.3f).SetEase(Ease.OutBack));
                partSequence.Play();

                delay += animStep;
            }
        }

        Invoke("ResetPosition", animationTime);
    }

    private void ResetPosition()
    {
        for (int i = 0; i < childArray.Length; i++)
        {
            childArray[i].transform.position = initialPosition[i];
        }
    }

    private void ResetPile()
    {
        for (int i = 0; i < childArray.Length; i++)
        {
            initialPosition[i] = childArray[i].transform.position;
            childArray[i].transform.DOScale(0, 0.1f);
        }
    }

    public void SwitchActive(bool isActive) { gameObject.SetActive(isActive); }
}
