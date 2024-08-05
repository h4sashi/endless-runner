using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Hanzo;


public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinScore;
    [SerializeField] TextMeshProUGUI distanceScore;
    [SerializeField] TextMeshProUGUI topCoinScore;
    // GameOverDistanceText (TMP)

    private string SCENE_NAME;
    private string HOME_SCENE = "MainMenu";

    private GameController gameController;
    private ScoreSystem scoreSystem;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject coinContainer, distanceContainer;

    int totalCoin;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        scoreSystem = GameObject.FindObjectOfType<ScoreSystem>();
        totalCoin = scoreSystem.coinScore;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        GetScore(coinScore.name, distanceScore.name, topCoinScore.name);

        GameObject.FindGameObjectWithTag("AbilityUI").SetActive(false);

    }


    private void Update()
    {
        GameObject.Find("PauseButton").SetActive(false);

    }


    void GetScore(string _coinScore, string CurrentDistance, string overallCoinScore)
    {
        coinScore.text = GameObject.Find(_coinScore).GetComponent<TextMeshProUGUI>().text;
        distanceScore.text = GameObject.Find(CurrentDistance).GetComponent<TextMeshProUGUI>().text;
        topCoinScore.text = GameObject.Find(overallCoinScore).GetComponent<TextMeshProUGUI>().text;

        string coinString = Hanzo.ScoreParam.TOP_COINSCORE.ToString();
        topCoinScore.text = PlayerPrefs.GetInt(coinString).ToString();

        coinScore.text = scoreSystem.coinScore.ToString();

        GameObject DistanceComponent = GameObject.Find("DistanceComponent");
        GameObject CoinComponent = GameObject.Find("CoinComponent");
        DistanceComponent.SetActive(false);
        CoinComponent.SetActive(false);


    }

    // === GAME OVER BUTTON SYSTEMS === //

    public void Play()
    {
        SCENE_NAME = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SCENE_NAME);
    }

    public void Home()
    {
        SceneManager.LoadScene(HOME_SCENE);
    }

    public void Continue()
    {


        if (totalCoin >= 5)
        {
            Debug.Log("Continue is pressed");
            playerScript.isDead = false;
            playerScript.isRestored = true;

            playerScript.cameraSwitch.InverseTransition();

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

            foreach (var obstacle in obstacles)
            {
                obstacle.SetActive(false);
            }

            playerScript.skinnedMeshRenderer.enabled = true;
            playerScript.animator.SetBool(MotionParameter.Death.ToString(), false);
            playerScript.animator.SetBool(MotionParameter.Run.ToString(), true);
            coinContainer.SetActive(true);
            distanceContainer.SetActive(true);
            gameObject.SetActive(false);

            totalCoin -= 5;
            scoreSystem.coinScore = totalCoin;
            int TotalPlayerCoin = PlayerPrefs.GetInt(Hanzo.ScoreParam.TOP_COINSCORE.ToString());
            string coinString = Hanzo.ScoreParam.TOP_COINSCORE.ToString();
            TotalPlayerCoin = TotalPlayerCoin - totalCoin;
            PlayerPrefs.SetInt(coinString, TotalPlayerCoin);




        }


    }


}
