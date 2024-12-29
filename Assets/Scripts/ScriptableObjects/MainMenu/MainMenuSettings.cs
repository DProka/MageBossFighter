
using UnityEngine;

[CreateAssetMenu(fileName = "MainMenuSettings", menuName = "ScriptableObject/MainMenu/MainMenuSettings")]
public class MainMenuSettings : ScriptableObject
{
    public MainMenuArenaBase arenaBase;
    public BossBase bossBase;
    public PlayerBase playerBase;
    public CameraSettings cameraSettings;
}
