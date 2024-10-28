using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    public List<Projectile> projectilesList;

    private ProjectileBase projBase;
    private Transform parent;

    public ProjectileManager(ProjectileBase _projBase, Transform _parent)
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

    public void InstantiateProjectile(Vector3 position, Vector3 targetPosition, bool isPlayer, float damage)
    {
        ProjectileSettings settings = isPlayer ? projBase.playerProjectilesArray[0] : projBase.enemyProjectilesArray[0];
        //UnitGeneral target = isPlayer ? GameController.Instance.enemy : GameController.Instance.player;

        Projectile projectile = UnityEngine.Object.Instantiate(projBase.projectilePrefab, position, Quaternion.identity, parent);
        projectile.Init(this, targetPosition, settings, isPlayer, damage);
        projectilesList.Add(projectile);
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
