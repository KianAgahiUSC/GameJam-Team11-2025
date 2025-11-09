using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [SerializeField] UI_StatBar healthBar;

        [HideInInspector] public PlayerUIManager playerUIHudManager;

        public void SetNewHealthValue(int oldValue, int newValue)
        {
            healthBar.SetStat(newValue);
        }

        public void SetMaxHealthValue(int newValue)
        {
            healthBar.SetMaxStat(newValue);
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            playerUIHudManager = GetComponentInChildren<PlayerUIManager>();
        }
    }
}