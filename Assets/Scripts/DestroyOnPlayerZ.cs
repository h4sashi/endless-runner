using UnityEngine;

public class DestroyOnPlayerZ : MonoBehaviour
{
    // Initial position of the GameObject
    private Vector3 initialPosition;

    // Reference to the player (or camera) Transform
    private Transform player;

    // Distance at which the object should be destroyed
    public float destroyDistance = 20f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Cache the Transform component for better performance
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Calculate the squared distance between the player and the initial position
        float sqrDistance = (player.position - initialPosition).sqrMagnitude;

        // Compare with the squared destroy distance for efficiency
        if (sqrDistance > destroyDistance * destroyDistance)
        {
            // Deactivate the GameObject this script is attached to
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
