using System.Collections;
using Demo;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float speedFactor = 400f;
    [SerializeField] private float damage = 50;
    private Coroutine _lifetime;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable health))
        {
            health.TakeDamage(damage);
            BulletReset();
        }
    }

    private IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        BulletReset();
    }

    private void BulletReset()
    {
        bulletRB.velocity = Vector3.zero;
        gameObject.SetActive(false);
        if (_lifetime != null) StopCoroutine(_lifetime);
    }

    public void Shoot(Vector3 direction, Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        _lifetime = StartCoroutine(BulletLifetime());
        bulletRB.AddForce(direction * speedFactor, ForceMode.Acceleration);
    }
}