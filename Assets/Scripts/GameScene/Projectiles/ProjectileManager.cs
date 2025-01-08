using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    public List<Projectile> projectilesList;

    private ProjectileManagerSettings projBase;
    private Transform parent;

    public ProjectileManager(ProjectileManagerSettings _projBase, Transform _parent)
    {
        projBase = _projBase;
        parent = _parent;
        projectilesList = new List<Projectile>();
    }

    public void UpdateList()
    {
        if(projectilesList.Count > 0)
        {
            for (int i = 0; i < projectilesList.Count; i++)
            {
                projectilesList[i].UpdateProjectile();
            }
        }
    }

    public void InstantiatePlayerProjectile(Vector3 position, Vector3 targetPosition, float damage, float speed)
    {
        Projectile projectile = UnityEngine.Object.Instantiate(projBase.playerBase.projectilePrefabArray[0], position, Quaternion.identity, parent);
        projectile.Init(this, targetPosition, true, damage, speed);
        projectilesList.Add(projectile);
    }
    
    //public void InstantiateEnemyProjectile(Vector3 position, Vector3 targetPosition, int enemyNum, float damage, float speed)
    public void InstantiateEnemyProjectile(Vector3 position, Vector3 targetPosition, Projectile projectilePrefab, float damage, float speed)
    {
        //Projectile projectile = UnityEngine.Object.Instantiate(projBase.enemyBase.projectilePrefabArray[enemyNum], position, Quaternion.identity, parent);
        Projectile projectile = UnityEngine.Object.Instantiate(projectilePrefab, position, Quaternion.identity, parent);
        projectile.Init(this, targetPosition, false, damage, speed);
        projectilesList.Add(projectile);
    }
    
    //public void InstantiateEnemyPointProjectile(Vector3 targetPosition, int num, float damage)
    public void InstantiateEnemyPointProjectile(Vector3 targetPosition, ProjectilePoint projectilePrefab, float damage)
    {
        //ProjectilePoint projectile = UnityEngine.Object.Instantiate(projBase.enemyBase.projectilePointPrefabArray[num], targetPosition, Quaternion.identity);
        ProjectilePoint projectile = UnityEngine.Object.Instantiate(projectilePrefab, targetPosition, Quaternion.identity);
        projectile.Init(damage);
    }

    public void AddProjectile(Projectile projectile) => projectilesList.Add(projectile);
    
    public void RemoveProjectile(Projectile projectile) => projectilesList.Remove(projectile);
    
    public void ClearList()
    {
        for (int i = 0; i < projectilesList.Count; i++)
        {
            projectilesList[i].DestroyBullet();
        }

        projectilesList = new List<Projectile>();
    }
}
