using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG { 
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;


        [Header("Camera Settings")]
        private Vector3 cameraVelocity;
        [SerializeField] private float cameraSmoothSpeed;
        private void Awake()
        {
            cameraSmoothSpeed = 1;
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                Debug.Log("FOLLOW");
                FollowTarget();
            }
        }

        private void FollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }
    }
}
