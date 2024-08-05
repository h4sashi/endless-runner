using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public enum AnimalType
    {
        HORSE,
        RAM

    }

    public AnimalType animalType;
    float movement;



    public float speed = 5.0f;  // Adjust the speed as needed

    void Update()
    {
        switch (animalType)
        {
            case AnimalType.HORSE:
                // Move along the negative z-axis
                movement = speed * Time.deltaTime;
                transform.Translate(movement, 0, 0);
                break;

            case AnimalType.RAM:
                // Move along the negative z-axis
                movement = speed * Time.deltaTime;
                transform.Translate(Vector3.forward * movement);
                break;


        }

    }
}
