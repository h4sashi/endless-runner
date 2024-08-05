using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Disposable"))
        {
            // Uncomment these lines to add a Rigidbody if not already present.

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Rigid body is not present");
            }
            else if (rb == null)
            {
                rb = other.gameObject.AddComponent<Rigidbody>();
                 Debug.Log("Rigid body is added");
            }


            // Generate a random force to apply to the obstacle.
            // Vector3 randomForce = new Vector3(Random.Range(-6f, 8f), Random.Range(-3f, 4.4f), 0f);
              Vector3 randomForce = new Vector3(Random.Range(-1f, 1.5f), Random.Range(1.2f, 2.2f), 0f);
             StartCoroutine(player.GetComponent<TraumaInducer>().Shake(0.245f));
                //Player is dead.
            other.transform.Translate(randomForce);
            // Apply the random force to the obstacle's Rigidbody.
            // rb.AddForce(randomForce, ForceMode.Impulse);
        }
    }
}
