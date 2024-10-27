using System.Collections;
using System.Collections.Generic;
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
            arenaPrefab = Instantiate(settings.arenaBase.arenasArray[num], arenaParent);
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
        }
    }

    private void ClearUnitPreviev()
    {
        Destroy(unitPrefab);
    }
}
