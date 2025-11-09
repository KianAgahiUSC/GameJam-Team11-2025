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

        [Header("Dodge")]
        private Vector3 rollDirection;

        private Vector3 targetRotationDirection;
        private Vector3 moveDirection;

        [SerializeField] float movementSpeed;
        [SerializeField] float rotationSpeed;

        [SerializeField] float dodgeLock;

        protected override void Awake()
        {
            base.Awake();
            movementSpeed = 4;
            rotationSpeed = 15;
            player = GetComponent<PlayerManager>();
        }
        public void HandleAllMovement()
        {
            if (player.isPerformingAction)
               return;
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;

             
        }

        private void HandleGroundedMovement()
        {

            if (!player.canMove)
                return;

            //Debug.Log("HANDLING GROUND MOVEMENT");
            GetVerticalAndHorizontalInputs();



            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (PlayerInputManager.instance.moveAmount > 0)
            {
                //MOVE
                //Debug.Log(movementSpeed);
                player.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void AttemptDodge()
        {
            if (player.isPerformingAction)
                return;
            if (PlayerInputManager.instance.moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;

                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimationManager.PlayerTargetActionAnimation("Dodge", false, true);
                player.playerAnimationManager.PlayerTargetActionAnimation("MoveBack", false, true);
                StartCoroutine(DodgeMovement());
            }
            else
            {
                //BACKSTEP?
            }
        }

        IEnumerator DodgeMovement()
        {
            player.isPerformingAction = true;
            player.canMove = false;
            player.isDodging = true;
            StartCoroutine(SmoothLerp(2.0f));
            yield return new WaitForSeconds(dodgeLock);
            player.isPerformingAction = false;
            player.canMove = true;
            yield return new WaitForSeconds(0.4f);
            player.isDodging = false;
        }

        private IEnumerator SmoothLerp(float time)
        {
            Vector3 startingPos = player.transform.position;
            Vector3 finalPos = player.transform.position + new Vector3(-2, 0, 0);

            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}  