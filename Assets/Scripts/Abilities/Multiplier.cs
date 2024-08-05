using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo;
using UnityEngine.UI;
using Hanzo.AudioSystem;

public class Multiplier : MonoBehaviour, IAbilities
{
    public GameObject MultiplierUI;
    public GameObject childSparkFX;
    public Slider multiplierSlider;

    public Transform Player;
    int multiplier = 2;
    public float timer = 25f;

    public bool isMultiplied = false;

    public Material initialMaterial;
    public Material multiplierMaterial;



    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MultiplierUI = GameObject.FindGameObjectWithTag("MultiplierUI");


    }

    public void ActivateAbility()
    {
        if (!isMultiplied)
        {
            AudioManager.Instance.PlaySFX("PowerUp");
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(ActivateCoinMultiplier(timer));

#if UNITY_EDITOR
            Debug.Log("Abilty Is Activated");
#endif     
        }
    }

    IEnumerator ActivateCoinMultiplier(float t)
    {
        isMultiplied = true;
        PlayerScript playerScript = Player.GetComponent<PlayerScript>();

       
        int originalCoinScore = playerScript.coinScore;

        playerScript.coinScore *= multiplier;

        // CoinMaterialEffect(multiplierMaterial);
        // childSparkFX.SetActive(false);

        SetActiveMultiplierUIChildren(true);
        GetSliderByChild();

        float elapsedTime = 0f;

        while (elapsedTime < t)
        {
            elapsedTime += Time.deltaTime;

            // Update your UI slider or any other visual representation of the timer
            multiplierSlider.value = t - elapsedTime;

            yield return null; // Wait for the next frame
        }

        SetActiveMultiplierUIChildren(false);

        // Ensure the timer display is accurate at the end
        multiplierSlider.value = 0f;

        // CoinMaterialEffect(initialMaterial);

        // Restore the original coin score
        playerScript.coinScore = originalCoinScore;

        isMultiplied = false;
        Destroy(gameObject, 2f);
    }


    // void CoinMaterialEffect(Material material)
    // {
    //     GameObject[] coins = GameObject.FindGameObjectsWithTag("CoinChild");
    //     foreach (var coin in coins)
    //     {
    //         coin.GetComponent<MeshRenderer>().material = material;
    //     }
    // }

    void SetActiveMultiplierUIChildren(bool active)
    {
        foreach (Transform child in MultiplierUI.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    void GetSliderByChild()
    {
        // Find the Slider child by name
        Transform sliderChild = MultiplierUI.transform.Find("Slider");

        // Check if the child is found and if it has a Slider component
        if (sliderChild != null)
        {
            multiplierSlider = sliderChild.GetComponent<Slider>();
             multiplierSlider.maxValue = timer;

            if (multiplierSlider == null)
            {
                Debug.LogError("The child named 'SliderChildName' does not have a Slider component.");
            }
        }
        else
        {
            Debug.LogError("The child named 'SliderChildName' not found under MultiplierUI.");
        }
    }


}
