using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.Utils;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Coin_Plain, Coins;
    public Transform StartPos, EndPos;
    [HideInInspector] public GameObject coinParent;

    // private bool hasSpawned = false;


    void Start()
    {
        Coin_Plain.GetComponent<Renderer>().enabled = false;
        Spawn();

    }

     void Spawn()
    {
        // Create a parent object for the coins
         coinParent = new GameObject("CoinParent");
         coinParent.tag = "CoinParent";
        coinParent.transform.position = Vector3.zero; // You can adjust the position as needed

        // Specify the number of coins you want to spawn
        int numberOfCoins = 10;

        // Calculate the spacing between coins
        float coinSpacing = (EndPos.position.z - StartPos.position.z) / numberOfCoins;
        float currentCoinPos = StartPos.position.z;

        for (int i = 0; i < numberOfCoins; i++)
        {
            // Instantiate a coin at the current position as a child of the coinParent
            GameObject coin = Instantiate(Coins, coinParent.transform);
            coin.transform.position = new Vector3(StartPos.position.x, StartPos.position.y + 0.8f, currentCoinPos);

            currentCoinPos += coinSpacing;
        }
        

        // Destroy the coinParent object after a certain time (e.g., 5 seconds)
        // Destroy(coinParent, 5f);
    }


}

