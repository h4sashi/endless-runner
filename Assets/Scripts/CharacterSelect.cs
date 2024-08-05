using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Hanzo;
using Hanzo.AudioSystem;

// public const string  = "SELECTED_PLAYER";

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] players;
    private int index;


    // Variables for swipe detection
    private Vector2 swipeStartPos;
    private bool isSwiping = false;
    private float minSwipeDistance = 50f;

    public TextMeshProUGUI nameText;

    [SerializeField] GameObject[] ObjectToDisable;
    [SerializeField] GameObject[] ObjectToEnable;


    private void Start()
    {
        index = PlayerPrefs.GetInt(Hanzo.SelectedPlayer.SELECTED_PLAYER.ToString(), 0);

        SetActivePlayer();
    }

    private void SetActivePlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(i == index);
            if (players[i].activeSelf)
            {
                nameText.text = players[i].gameObject.name;
            }


        }
    }

    private void Update()
    {
        HandleSwipeInput();
    }

    private void HandleSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPos = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isSwiping = false;
                Vector2 swipeDelta = touch.position - swipeStartPos;
                if (swipeDelta.magnitude > minSwipeDistance)
                {
                    float xSwipe = swipeDelta.x;
                    if (Mathf.Abs(xSwipe) > Mathf.Abs(swipeDelta.y))
                    {
                        if (xSwipe < 0)
                        {
                            Next();
                            AudioManager.Instance.PlaySFX("Swoosh");
                        }
                        else
                        {
                            Previous();
                            AudioManager.Instance.PlaySFX("Swoosh");
                        }
                    }
                }
            }

        }

        // Handle mouse swipe input
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPos = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            Vector2 swipeDelta = (Vector2)Input.mousePosition - swipeStartPos;
            if (swipeDelta.magnitude > minSwipeDistance)
            {
                float xSwipe = swipeDelta.x;
                if (Mathf.Abs(xSwipe) > Mathf.Abs(swipeDelta.y))
                {
                    if (xSwipe < 0)
                    {
                        Next();
                    }
                    else
                    {
                        Previous();
                    }
                }
            }
        }
    }

    public void Select()
    {
        //Disable Select Button, CharacterSelect GameObject
        //Enable Tap to play button and characterSelect button

        PlayerPrefs.SetInt(Hanzo.SelectedPlayer.SELECTED_PLAYER.ToString(), index);
        foreach (var obj in ObjectToDisable)
        {
            obj.SetActive(false);
        }
        foreach (var obj in ObjectToEnable)
        {
            obj.SetActive(true);
        }

#if UNITY_EDITOR
        Debug.Log("Selected player: " + index);
#endif
    }

    public void Next()
    {
        AudioManager.Instance.PlaySFX("Swoosh");
        index = (index + 1) % players.Length;
        SetActivePlayer();

    }

    public void Previous()
    {
        AudioManager.Instance.PlaySFX("Swoosh");
        index = (index - 1 + players.Length) % players.Length;
        SetActivePlayer();
    }
}

