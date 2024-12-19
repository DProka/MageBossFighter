
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraUpdater : MonoBehaviour
{
    private void OnEnable()
    {
        // Подписываемся на событие загрузки новой сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Отписываемся от события
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Найти новую камеру в сцене по тегу "MainCamera"
        Camera newMainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();

        if (newMainCamera != null)
        {
            Camera.main.gameObject.SetActive(false); // Деактивировать старую камеру
            newMainCamera.tag = "MainCamera"; // Установить новый тег
        }
        else
        {
            Debug.LogWarning("MainCamera not found in the new scene!");
        }
    }
}
