using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] float explosionDelay = 5f, blastRadius = 5f, force = 700f, beforceTouchedSpeed = 5, afterTouchedSpeed = 10;
    [SerializeField] GameObject explosionEffect;  
    bool beenTouched = false;

    void FixedUpdate()
    {
        transform.position += Vector3.down * (beenTouched ? afterTouchedSpeed : beforceTouchedSpeed) * Time.deltaTime;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force, transform.position, blastRadius);
        }
        Destroy(gameObject);
        yield return null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!beenTouched && collider.gameObject.tag == "Player")
        {
            beenTouched = true;
            StartCoroutine(Explode());
        }
    }
}