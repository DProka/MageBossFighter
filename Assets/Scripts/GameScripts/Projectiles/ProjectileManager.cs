using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    public List<Projectile> projectilesList;

    private ProjectileBase projBase;

    public ProjectileManager(ProjectileBase _projBase)
    {
        projBase = _projBase;
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

    public void InstantiateProjectile(Vector3 position, UnitGeneral target, bool isPlayer)
    {
        ProjectileSettings settings = isPlayer ? projBase.playerProjectilesArray[0] : projBase.enemyProjectilesArray[0];

        Projectile projectile = UnityEngine.Object.Instantiate(projBase.projectilePrefab, position, Quaternion.identity);
        projectile.Init(this, target, settings, isPlayer);
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
