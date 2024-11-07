
using UnityEngine;

public class MainMenuPreviewManager : MonoBehaviour
{
    [SerializeField] Transform arenaParent;
    [SerializeField] Transform unitParent;

    private MainMenuSettings settings;
    private GameObject arenaPrefab;
    private GameObject unitPrefab;

    public void Init(MainMenuSettings _settings)
    {
        settings = _settings;
    }

    public void SpawnArenaByNum(int num)
    {
        GameObject newArena = settings.arenaBase.arenasArray[num];
        
        if(arenaPrefab != newArena)
        {
            ClearArenaPreviev();
            arenaPrefab = Instantiate(settings.arenaBase.arenasArray[num], arenaParent);
        }

        Debug.Log("Arena is loaded");
    }

    public void SpawnPlayerByNum(int num)
    {
        GameObject newPlayer = settings.playerBase.playerPrefabsArray[num];

        if(unitPrefab != newPlayer)
        {
            ClearUnitPreviev();
            unitPrefab = Instantiate(settings.playerBase.playerPrefabsArray[num], unitParent);
        }
    }

    public void SpawnBossByNum(int num)
    {
        GameObject newBoss = settings.bossBase.bossPrefabsArray[num];

        if (unitPrefab != newBoss)
        {
            ClearUnitPreviev();
            unitPrefab = Instantiate(settings.bossBase.bossPrefabsArray[num], unitParent);
            unitPrefab.transform.localScale = new Vector3(25, 25, 25);

            switch (num)
            {
                case 0:
                    unitPrefab.transform.localScale = new Vector3(45, 45, 45);
                    break;
            
                case 1:
                    unitPrefab.transform.localScale = new Vector3(35, 35, 35);
                    break;
            
                case 2:
                    unitPrefab.transform.localScale = new Vector3(80, 80, 80);
                    break;

                case 3:
                    unitPrefab.transform.localScale = new Vector3(45, 45, 45);
                    break;
            }
        }
    }

    public EnemySettings GetEnemySettings()
    {
        if (unitPrefab.TryGetComponent<BossScript>(out BossScript boss))
        {
            return boss._settings;
        }
        else
            return null;
    }

    public void ClearManager()
    {
        ClearUnitPreviev();
        ClearArenaPreviev();
    }

    private void ClearUnitPreviev() => Destroy(unitPrefab);
    
    private void ClearArenaPreviev() => Destroy(arenaPrefab);
}
