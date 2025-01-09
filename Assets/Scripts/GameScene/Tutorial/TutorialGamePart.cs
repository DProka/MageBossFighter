
using UnityEngine;

public class TutorialGamePart : MonoBehaviour
{
    [SerializeField] GameObject tutorialObj;
    [SerializeField] GameObject tutorialButton;

    public void SwitchTutorial(bool isActive)
    {
        tutorialObj.SetActive(isActive);
        tutorialButton.SetActive(isActive);
    }
}
