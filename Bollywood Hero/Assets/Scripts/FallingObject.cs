using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [System.Serializable]
    class Offset {
        public float xPositionOffset, yPositionOffset, zPositionOffset, xRotationOffset, yRotationOffset, zRotationOffset;
    }
    [SerializeField] Offset offsets;

    [SerializeField] float delay = 5f, blastRadius = 5f, force = 700f;
    float downSpeed = 0, countdown = 3;

    [SerializeField] GameObject explosionEffect;  
    bool isFalling = false;

    Vector3 rotation;

    void Start()
    {
        transform.position += new Vector3(Random.Range(-offsets.xPositionOffset, offsets.xPositionOffset), 
                                          Random.Range(-offsets.yPositionOffset, offsets.yPositionOffset),
                                          Random.Range(-offsets.zPositionOffset, offsets.zPositionOffset));

        rotation = new Vector3(Random.Range(-offsets.xRotationOffset, offsets.xRotationOffset),
                                          Random.Range(-offsets.yRotationOffset, offsets.yRotationOffset),
                                          Random.Range(-offsets.zRotationOffset, offsets.zRotationOffset));
    }

    void Update()
    {
        transform.Rotate(rotation);

        if (isFalling)
        {
            downSpeed += Time.deltaTime / 50;
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y - downSpeed,
                                             transform.position.z);

            if (countdown >= 0f)
                countdown -= Time.deltaTime;
            else
                Explode();
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

    void OnTriggerEnter(Collider collider)
    {
        if (!isFalling && collider.gameObject.tag == "Player")
        {
            isFalling = true;
            countdown = delay;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(offsets.xPositionOffset, offsets.yPositionOffset, offsets.zPositionOffset));
    }
}