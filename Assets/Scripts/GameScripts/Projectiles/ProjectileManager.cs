using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    public List<Projectile> projectilesList;

    private Projectile projectilePrefab;

    public ProjectileManager(Projectile _projectilePrefab)
    {
        projectilePrefab = _projectilePrefab;
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

    public void InstantiateProjectile(Vector3 position, UnitGeneral target)
    {
        Projectile projectile = UnityEngine.Object.Instantiate(projectilePrefab, position, Quaternion.identity);
        projectile.Init(this, target);
        projectilesList.Add(projectile);
    }

    public void AddProjectile(Projectile projectile)
    {
        projectilesList.Add(projectile);
    }
    
    public void RemoveProjectile(Projectile projectile)
    {
        projectilesList.Remove(projectile);
    }

    public void ClearList()
    {
        for (int i = 0; i < projectilesList.Count; i++)
        {
            projectilesList[i].DestroyBullet();
        }
    }
}
