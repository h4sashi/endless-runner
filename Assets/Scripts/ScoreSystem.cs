using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinText;
    private Transform player;
    [HideInInspector] public float initialZPosition;
    public float distanceScore;
    public int coinScore;
    [HideInInspector] public int topDistance;
    string TopDistancePref;

    private bool hasRestored = false;
    public float distance;

    public float Distance => distance;
    private void Start()
    {
        // PlayerPrefs.DeleteAll();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        TopDistancePref = Hanzo.ScoreParam.TOP_DISTANCE.ToString();
        topDistance = PlayerPrefs.GetInt(TopDistancePref);

        initialZPosition = player.position.z;
        distanceScore = 0;
    }

    void Update()
    {

        coinText.text = coinScore.ToString();


        float currentZPosition = player.position.z;

        distanceScore = Mathf.Max(0, currentZPosition - initialZPosition);
        int distanceScoreInt = Mathf.RoundToInt(distanceScore);
        distanceText.text = distanceScoreInt.ToString();


        if (distanceScore > topDistance)
        {
            topDistance = (int)distanceScore;
            PlayerPrefs.SetInt(TopDistancePref, topDistance);
        }
    }
}
