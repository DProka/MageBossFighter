
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIEndRoundScreen : MonoBehaviour, IMenuScreen
{
    [SerializeField] TextMeshProUGUI endText;

    [SerializeField] Image enemyHpBar;
    [SerializeField] Image playerHpBar;
    [SerializeField] TextMeshProUGUI earnedCoinText;

    private Canvas mainCanvas;

    public void Init()
    {
        mainCanvas = GetComponent<Canvas>();

        SwitchMainCanvas(false);
    }

    public void CallScreen(bool win)
    {
        endText.text = win ? "YOU WIN" : "YOU LOSE";
        OpenScreen();

        ResetBar();
        StartEndScreenAnimation();
    }

    private void StartEndScreenAnimation()
    {
        float bossMaxHP = GameController.Instance.enemy._settings.maxHealth;
        float bossCurrentHealth = GameController.Instance.enemy._currentHealth;
        float percentageBoss = bossCurrentHealth / bossMaxHP;

        float playerMaxHP = GameController.Instance.player._settings.maxHealth;
        float playerCurrentHealth = GameController.Instance.player._currentHealth;
        float percentagePlayer = playerCurrentHealth / playerMaxHP;


        float earnedCoins = (bossMaxHP - bossCurrentHealth) * 2f;
        float substractedCoins = playerMaxHP - playerCurrentHealth;
        float coins = earnedCoins - substractedCoins;

        enemyHpBar.DOFillAmount(percentageBoss, 2f).SetDelay(1f).OnComplete(() => 
        {
            earnedCoinText.text = "" + earnedCoins;

            playerHpBar.DOFillAmount(percentagePlayer, 2f).OnComplete(() => 
            {
                earnedCoinText.text = "" + coins;
            });
        });

        Sequence sequence = DOTween.Sequence();

        DataHolder.playerCoins += (int)coins;
    }

    public void ResetBar()
    {
        earnedCoinText.text = "" + 0;
        enemyHpBar.DOFillAmount(1f, 0);
        playerHpBar.DOFillAmount(1f, 0);
    }

    #region Screen Part

    public void OpenScreen()
    {
        SwitchMainCanvas(true);
    }

    public void CloseScreen()
    {
        SwitchMainCanvas(false);
    }

    public void SwitchMainCanvas(bool isActive) => mainCanvas.enabled = isActive;

    #endregion
}
