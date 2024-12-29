
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObject/Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public float timeToStart;

    public GameObject[] playerPrefab;
    public ArenaBase arenaBase;
    public BossBase bossBase;
    public VfxBase vfxBase;
    public CameraSettings cameraSettings;
}
