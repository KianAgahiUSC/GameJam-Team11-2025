using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        private Vector3 moveDirection;

        [SerializeField] float movementSpeed;

        protected override void Awake()
        {
            base.Awake();
            movementSpeed = 4;
            player = GetComponent<PlayerManager>();
        }
        public void HandleAllMovement()
        {
            HandleGroundedMovement();
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;

             
        }

        private void HandleGroundedMovement()
        {
            Debug.Log("HANDLING GROUND MOVEMENT");
            GetVerticalAndHorizontalInputs();

            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (PlayerInputManager.instance.moveAmount > 0)
            {
                //MOVE
                Debug.Log(movementSpeed);
                player.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
        }
    }
}  