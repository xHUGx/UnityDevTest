using System;
using System.Collections;
using UnityEngine;

namespace Demo
{
    public class Rifle : Weapon
    {
        [Header("Shoot Queue")]
        [SerializeField] private int shootQueueAmount = 3;
        [SerializeField] private float delayInSecondsBetweenShoots = 0.1f;

        public override void Shoot(Vector3 direction)
        {
            StartCoroutine(ShootWithDelay(() => base.Shoot(direction)));
        }

        IEnumerator ShootWithDelay(Action action)
        {
            for (var i=0; i < shootQueueAmount;  i++)
            {
                yield return new WaitForSeconds(delayInSecondsBetweenShoots);
                action();
            }
        }
    }
}