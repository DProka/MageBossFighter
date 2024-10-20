using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInitializer : MonoBehaviour
{
    [Header("Game Part")]

    [SerializeField] GameController gameController;
    [SerializeField] ArenaManager arenaManager;
    [SerializeField] EnemyController enemy;
    [SerializeField] ProjectilesManager projectilesManager;

    [Header("UI Part")]

    [SerializeField] UIController uiController;

    void Awake()
    {
        
    }

    void Update()
    {
        
    }
}
