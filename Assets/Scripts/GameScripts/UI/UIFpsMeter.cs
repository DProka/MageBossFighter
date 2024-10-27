
using UnityEngine;
using TMPro;

public class UIFpsMeter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;
    
    private float fpsTimer;

    public void ShowFPS()
    {
        if (fpsText.enabled)
        {
            float fps = 1.0f / Time.deltaTime;
            fpsTimer += Time.deltaTime;

            if (fpsTimer >= 0.3f)
            {
                fpsText.text = "FPS: " + Mathf.RoundToInt(fps);
                fpsTimer = 0f;
            }
        }
    }

}
