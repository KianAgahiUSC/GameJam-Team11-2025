using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour

{

    [SerializeField] float attackCooldown;

    [SerializeField] GameObject projectile_;


    [SerializeField] GameObject player;
    [SerializeField] float projectileSpeed;
    SphereCollider zone;
    GameObject currProjectile;
    Rigidbody rb;

    bool canAttack = true;
    private void Awake()
    {

    }

    private void Update()
    {
        SpawnAttack();
    }

    private void SpawnAttack()
    {
        if (canAttack)
        {
            //ATTACK


            Vector3 direction;

            direction = player.transform.position - gameObject.transform.position;
            direction.Normalize();

            currProjectile = Instantiate(projectile_);
            currProjectile.transform.position = gameObject.transform.position + (direction * 2);

            Debug.Log(direction * projectileSpeed);

            currProjectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

            Debug.Log(currProjectile.GetComponent<Rigidbody>().velocity);
            StartCoroutine(Cooldown());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
            StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        var position = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));

        StartCoroutine(SmoothLerp(0.5f));

        yield return new WaitForSeconds(2.0f);

        gameObject.transform.position = player.transform.position + position;

    }

    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + new Vector3(0, -5, 0);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
