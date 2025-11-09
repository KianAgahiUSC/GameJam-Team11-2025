using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class ProjectileAI : MonoBehaviour
    {
        [SerializeField] float lifespan;
        GameObject player;
        PlayerManager playerManager;
        private void Update()
        {

        }

        private void Start()
        {
            StartCoroutine(Lifespan());
            player = GameObject.Find("Player");
            playerManager = player.GetComponent<PlayerManager>();
        }

        IEnumerator Lifespan()
        {
            yield return new WaitForSeconds(lifespan);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == player.gameObject)
            {
                playerManager.TakeDamage(0);    
            }
            Destroy(gameObject);
        }
    }
}
