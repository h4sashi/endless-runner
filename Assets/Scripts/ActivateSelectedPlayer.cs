using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo;

public class ActivateSelectedPlayer : MonoBehaviour
{
    public GameObject[] players;
    private int index;

    private void Start()
    {

        index = PlayerPrefs.GetInt(Hanzo.SelectedPlayer.SELECTED_PLAYER.ToString());

        for (int i = 0; i < players.Length; i++)
        {
            if (i == index)
            {
                players[i].gameObject.SetActive(true);
            }
            else
            {
                players[i].gameObject.SetActive(false);
            }
        }
    }
}
