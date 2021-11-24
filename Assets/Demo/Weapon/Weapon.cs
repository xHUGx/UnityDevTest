using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform muzzle;

    public virtual void Shoot(Vector3 direction)
    {
        var pooledProjectile = BulletsPooler.SharedInstance.GetPooledObject();
        if (pooledProjectile == null)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        pooledProjectile.Shoot(direction, muzzle.position);
        Debug.Log("Shoot");
    }
}