
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCameraPosition(CameraSettings settings)//Vector3 position, Quaternion rotation, bool canvasIsActive)
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = settings.position;

            Quaternion rotation = Quaternion.Euler(settings.rotation.x, settings.rotation.y, settings.rotation.z);
            mainCamera.transform.rotation = rotation;

            mainCamera.orthographic = settings.isOrthographic;

            if (settings.isOrthographic)
                mainCamera.orthographicSize = settings.size;

            if (cameraCanvas != null)
                cameraCanvas.enabled = settings.canvasIsActive;
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

