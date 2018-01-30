using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{

    [SerializeField] float delay = 5f, blastRadius = 5f, force = 700f;
    [SerializeField] GameObject explosionEffect;

    float downSpeed = 0, countdown = 3;
    bool hasExploded = false, isFalling = false;

    void OnTriggerEnter(Collider collider)
    {
        if (!isFalling && collider.gameObject.tag == "Player")
        {
            isFalling = true;
            countdown = delay;
        }
    }

    void Update()
    {
        if (isFalling)
        {
            downSpeed += Time.deltaTime / 50;
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y,
                                             transform.position.z);

            if (countdown >= 0f)
                countdown -= Time.deltaTime;
            else
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force, transform.position, blastRadius);
        }
        Destroy(gameObject);
    }
}