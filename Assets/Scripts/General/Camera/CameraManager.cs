using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] Camera mainCamera;
    [SerializeField] Canvas cameraCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем между сценами
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующий объект
        }
    }

    public void SetCameraPosition(Vector3 position, Quaternion rotation, bool canvasIsActive)
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = position;
            mainCamera.transform.rotation = rotation;

            if(cameraCanvas != null)
                cameraCanvas.enabled = canvasIsActive;
        }
    }

    public void SetCameraTarget(Transform target)
    {
        if (mainCamera != null)
        {
            mainCamera.transform.LookAt(target);
        }
    }
}

