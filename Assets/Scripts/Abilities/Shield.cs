using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hanzo.AudioSystem;

public class Shield : MonoBehaviour, IAbilities
{
    public GameObject shieldFX;
    public GameObject Air;
    public bool isShieldActive = false;
    public float timer = 25f;
    float elapsedTime = 0f;

    public GameObject player;

    public Slider ShieldSlider;
    public GameObject ShieldUI;

    private ShieldManager shieldManager;
    int collisionCount = 0;

    private void Start()
    {
        shieldManager = GameObject.FindObjectOfType<ShieldManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        ShieldUI = GameObject.Find("ShieldUI");

        Air = player.GetComponent<Hanzo.PlayerScript>().Air;
    }

    void Update()
    {
        if (isShieldActive == true)
        {

            SetObstacle("Obstacle", "Disposable");
        }


    }

    public void ActivateAbility()
    {
        Debug.Log("Activating Shield Ability");
        GetComponent<MeshRenderer>().enabled = false;

        if (isShieldActive)
        {
            // If shield is already active, reset the timer and check the shield count
            elapsedTime = 0f;

            shieldManager.CollisionCount++;
            collisionCount = shieldManager.CollisionCount;

            if (collisionCount >= 2)
            {
                collisionCount = 0; // Reset the counter
                shieldManager.CollisionCount = collisionCount;


                StartCoroutine(ActivateShield(timer)); // Restart the coroutine
            }
        }
        else
        {
            StartCoroutine(ActivateShield(timer));
        }
    }




    IEnumerator ActivateShield(float t)
    {
        isShieldActive = true;


        shieldFX.SetActive(true);
        Air.SetActive(true);
        AudioManager.Instance.PlaySFX("PowerUp");

        SetObstacle("Obstacle", "Disposable");

        SetActiveSheildUIChildren(true);
        GetSliderByChild();



        while (elapsedTime < t)
        {
            elapsedTime += Time.deltaTime;
            ShieldSlider.value = t - elapsedTime;
            yield return null;

        }

        // Ensure proper deactivation

        isShieldActive = false;
        shieldFX.SetActive(false);
        Air.SetActive(false);
        SetObstacle("Disposable", "Obstacle");
        SetActiveSheildUIChildren(false);  // Deactivate Shield UI

        Destroy(gameObject);
    }


    void SetObstacle(string initialTag, string finalTag)
    {

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(initialTag);
        foreach (var obstacle in obstacles)
        {
            obstacle.tag = finalTag;

        }

    }

    public List<Transform> shieldTransforms;

    void SetActiveSheildUIChildren(bool active)
    {
        // Clear the list before adding new elements
        shieldTransforms = new List<Transform>();

        foreach (Transform child in ShieldUI.transform)
        {
            shieldTransforms.Add(child);
            child.gameObject.SetActive(active);
        }
    }


    void GetSliderByChild()
    {
        // Find the Slider child by name
        Transform sliderChild = ShieldUI.transform.Find("Slider");

        // Check if the child is found and if it has a Slider component
        if (sliderChild != null)
        {
            ShieldSlider = sliderChild.GetComponent<Slider>();
            if (ShieldSlider != null)
            {
                ShieldSlider.maxValue = timer;
            }
            else
            {
                Debug.LogError("The child named 'Slider' does not have a Slider component.");
            }
        }
        else
        {
            Debug.LogError("The child named 'Slider' not found under ShieldUI.");
        }
    }



}
