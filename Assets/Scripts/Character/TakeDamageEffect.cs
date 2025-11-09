using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class TakeDamageEffect : InstantCharacterEffect
    {

        [Header("Damage")]
        public float damage1 = 0;
        public float damage2 = 0;
        public float damage3 = 0;


        [Header("Final Damage")]
        private float findalDamageDealt = 0;


        [Header("Animation")]
        public bool playDamageAnimation = true;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip damageSoundFX;

        [Header("Damage Direction")]
        public float angleHitFrom;
        public Vector3 contactPoint;

        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);

            if (character.isDead)
                return;


        }
    }
}
