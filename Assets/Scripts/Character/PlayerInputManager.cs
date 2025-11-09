using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;


namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        UserActions playerControls;

        public static PlayerInputManager instance;

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput;
        [HideInInspector] public float verticalInput;
        [HideInInspector] public float horizontalInput;
        [HideInInspector] public float moveAmount;

        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false;

        public PlayerManager player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            SceneManager.activeSceneChanged += OnSceneChange;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            instance.enabled = true;

        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            instance.enabled = true;
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new UserActions();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerMovement.Movement.canceled += i => movementInput = i.ReadValue<Vector2>();

                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.canceled += i => cameraInput = i.ReadValue<Vector2>();

                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Dodge.canceled += i => dodgeInput = false;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

         

        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            //Debug.Log(verticalInput);
            //Debug.Log(horizontalInput);
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            player.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount);
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        private void HandleDodgeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;

                //COROUTINE

                player.playerLocomotionManager.AttemptDodge();
            }
        }

       

        private void Update()
        {
            HandleAllInputs();



        }

        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
        }
    }
}
