using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.AudioSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignContainer : MonoBehaviour
{
    private int levelIndex = 0;
    public GameObject[] levels;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(levels[0].gameObject.name);
        Debug.Log(levels[1].gameObject.name);
    }


    public void Next()
    {
        AudioManager.Instance.PlaySFX("Swoosh");
        levelIndex = (levelIndex + 1) % levels.Length;
        SetActiveLevel();

    }
    public void Previous()
    {
        AudioManager.Instance.PlaySFX("Swoosh");
        levelIndex = (levelIndex - 1 + levels.Length) % levels.Length;
        SetActiveLevel();

    }

    public void SelectLevel()
    {
        Debug.Log(levels[0].gameObject.name);
        Debug.Log(levels[1].gameObject.name);
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].activeSelf && levels[i].name == levels[0].gameObject.name)
            {
                SceneManager.LoadScene("Eden");
            }
            else if (levels[i].activeSelf && levels[i].name == levels[1].gameObject.name)
            {
                SceneManager.LoadScene("Desert");
            }
        }
    }

    private void SetActiveLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == levelIndex);
        }
    }
}
