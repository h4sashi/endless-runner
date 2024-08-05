using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadStart : MonoBehaviour, IAbilities
{
    public GameObject Player;
    public bool isHeadstart = false;
    [SerializeField] private GameObject airFX;

    public GameObject headStartUI;
    public Slider headstartSlider;


    public float headStartSpeed = 3f;
    public float timer = 7f;

    public float initialAnimationSpeed;
    public float headStartAnimationSpeed = 1.25f;


    public void ActivateAbility()
    {
        // AudioManager.Instance.PlaySFX("PowerUp");
        StartCoroutine(ActivateHeadStart(timer));
    }


    IEnumerator ActivateHeadStart(float t)
    {
        isHeadstart = true;

        if (airFX != null)
            airFX.SetActive(true);

        Player.GetComponent<Collider>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;

        initialAnimationSpeed = Player.GetComponent<Animator>().speed;

        Player.GetComponent<Animator>().speed = headStartAnimationSpeed;

        float initialZPosition = Player.transform.position.z;
        float targetZPosition = initialZPosition + headStartSpeed * t;

        while (Player.transform.position.z < targetZPosition)
        {
            float step = headStartSpeed * Time.deltaTime;
            Player.transform.Translate(0, 0, step);
            yield return null;
        }

        isHeadstart = false;

        if (airFX != null)
            airFX.SetActive(false);

        Player.GetComponent<Animator>().speed = initialAnimationSpeed;
        Player.GetComponent<Collider>().enabled = true;
        Player.GetComponent<Rigidbody>().isKinematic = false;
    }


    private void Start()
    {
        Player = this.gameObject;
    }


        void SetActiveHeadStartUIChildren(bool active)
    {
        foreach (Transform child in headStartUI.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

     void GetSliderByChild()
    {
        // Find the Slider child by name
        Transform sliderChild = headStartUI.transform.Find("Slider");

        // Check if the child is found and if it has a Slider component
        if (sliderChild != null)
        {
            headstartSlider = sliderChild.GetComponent<Slider>();
             headstartSlider.maxValue = timer;

            if (headstartSlider == null)
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
