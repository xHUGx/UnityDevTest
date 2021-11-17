using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private float lifetime = 5f;
    private Coroutine _lifetime;

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        BulletReset();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyHealth health))
        {
            health.TakeDamage(50);
            BulletReset();
        }
    }
    private void BulletReset()
    {
        bulletRB.velocity = Vector3.zero;
        gameObject.SetActive(false);
        if (_lifetime!= null){StopCoroutine(_lifetime);}
    }

    public void Shoot(Vector3 direction, Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        _lifetime = StartCoroutine(BulletLifetime());
        bulletRB.AddForce(direction * 400f , ForceMode.Acceleration);
    }
}