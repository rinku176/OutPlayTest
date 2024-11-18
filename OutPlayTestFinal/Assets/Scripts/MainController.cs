using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Transform[] waypoints; // Set P1, P2, P3 in the Inspector
    float speed = 5f;      // Movement speed
    public GameObject particleEffectPrefab; // Prefab for the particle effect
    public AudioClip collisionSound;       // Sound to play on collision

    private int currentWaypointIndex = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Move towards the current waypoint
            Transform target = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the object has reached the waypoint
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Debug.LogWarning("Point Reached!");
                currentWaypointIndex++;
            }
        }
        else
        {
            // Final waypoint reached
            TriggerEffectsAndDestroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerEffectsAndDestroy();
    }

    void TriggerEffectsAndDestroy()
    {
        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

        // Play collision sound
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }

        // Destroy the main object
        Destroy(gameObject);
    }
}
