using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{ 
    public class CharacterManager : MonoBehaviour
    {


        [HideInInspector] public Animator animator;

        [HideInInspector] public CharacterController characterController;

        [Header("Status")]
        public bool isDead = false;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool canRotate = true;
        public bool canMove = true;

        [Header("Stats")]
        public int health = 100;
        public int maxHealth = 100;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {

        }    

        protected virtual void LateUpdate()
        {

        }

        public virtual void OnSpawn() { 
      

        }

        public virtual void TakeDamage(int projectileType)
        {

        }
    }
}
