
using UnityEngine;

[CreateAssetMenu(fileName = "LevelBase", menuName = "ScriptableObject/Game/LevelBase")]
public class LevelBase : ScriptableObject
{
    public string[] lvlName;
    
    public string[] BossName;

    public GameObject[] bossModel;

    public GameObject[] arenaModel;
}
