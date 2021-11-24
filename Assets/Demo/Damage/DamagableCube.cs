using UnityEngine;

namespace Demo
{
    public class DamagableCube : Damagable
    {
        [SerializeField] private Transform obj;
        [SerializeField] private Transform parts;
        public override void InitiateDestroy()
        {
            obj.gameObject.SetActive(false);
            parts.gameObject.SetActive(true);
        }
    }
}