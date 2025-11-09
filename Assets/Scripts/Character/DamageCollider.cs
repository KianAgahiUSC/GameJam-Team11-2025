using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG {
    public class DamageCollider : MonoBehaviour
    {
        [Header("Damage")]
        public float damage1 = 0;
        public float damage2 = 0;
        public float damage3 = 0;

        private Vector3 contactPoint;

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                DamageTarget(damageTarget);
            }
    
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            TakeDamageEffect damageEffect;

        }

    }
}
