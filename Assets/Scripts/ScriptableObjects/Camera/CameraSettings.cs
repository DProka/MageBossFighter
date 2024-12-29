
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObject/Game/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    public Vector3 position;
    public Vector3 rotation;
    public bool canvasIsActive;
    public bool isOrthographic;
    public float size;
}
