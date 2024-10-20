using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    public Action<Projectile> onProjectileDeleted;

    private List<Projectile> projectilesList;

    public void Init()
    {
        projectilesList = new List<Projectile>();
        onProjectileDeleted += RemoveProjectileFromList;
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

    public void IntstantiateNewProjectile(bool isPalyer)
    {
        Projectile projectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(pos));
        projectile.SetBulletActive(shootPoint.position, pos);
        projectile.damage = damage;
    }

    private void RemoveProjectileFromList(Projectile projectile)
    {
        projectilesList.Remove(projectile);
    }
}
