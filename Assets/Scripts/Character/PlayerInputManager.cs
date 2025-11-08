using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInputManager : MonoBehaviour
{
    UserActions playerControls;

    public static PlayerInputManager instance;

    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

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
        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        //Debug.Log(verticalInput);
        //Debug.Log(horizontalInput);
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));
    }

    private void Update()
    {
        HandleMovementInput();
    }
}
