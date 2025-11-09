 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SG 
{ 
    public class PlayerManager : CharacterManager
    {
        public PlayerLocomotionManager playerLocomotionManager;
        public PlayerAnimationController playerAnimationManager;

        [SerializeField] UI_StatBar healthBar;

        [SerializeField] List<int> damages;

        [SerializeField] float height;

        public bool isDodging = false;
        protected override void Awake()
        {
            base.Awake();
            damages = new List<int> { 5, 10, 15};
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimationManager = GetComponent<PlayerAnimationController>();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, height, gameObject.transform.position.z);

            if (health <= 0)
            {
                SceneManager.LoadScene(1);
                
            }
;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
        }

        public override void TakeDamage(int projectileType)
        {

            if (isDodging)
                return;
            base.TakeDamage(projectileType);

            Debug.Log(damages[projectileType]);
            health -= damages[projectileType];
            healthBar.SetStat(health);
        }
    }
}
