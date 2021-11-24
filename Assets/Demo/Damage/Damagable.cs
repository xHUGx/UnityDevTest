using UnityEngine;

namespace Demo
{
    public class Damagable : MonoBehaviour, IDamagable
    {
        [SerializeField] private float health;
        
        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                InitiateDestroy();
            }
        }

        public virtual void InitiateDestroy()
        {
            Destroy(gameObject);
        }
    }
}