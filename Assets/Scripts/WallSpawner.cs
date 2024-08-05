using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallSpawner : MonoBehaviour
{
    public GameObject[] walls;
    private int odd;

    private void Start()
    {

        GetComponent<Renderer>().enabled = false;

        GenerateWall();
    }

    public void GenerateWall()
    {
        if (SceneManager.GetActiveScene().name == "Eden")
        {
            GameObject obstacleContainer = GameObject.Find("ObstacleContainer");

            if (obstacleContainer == null)
            {
                obstacleContainer = new GameObject("ObstacleContainer");
                obstacleContainer.tag = "ObstacleContainer";
            }


            odd = Random.Range(0, walls.Length);

            Vector3 spawnPos = transform.position;
            spawnPos.y += (odd == 0) ? 0f : 0f;
            Quaternion rotation = (odd == 1) ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 0, 0);

            GameObject wall = Instantiate(walls[odd], spawnPos, rotation);
            wall.transform.SetParent(obstacleContainer.transform);

            obstacleContainer.transform.SetParent(transform.parent);
        }
        else if (SceneManager.GetActiveScene().name == "Desert")
        {
            GameObject obstacleContainer = GameObject.Find("ObstacleContainer");

            if (obstacleContainer == null)
            {
                obstacleContainer = new GameObject("ObstacleContainer");
                obstacleContainer.tag = "ObstacleContainer";
            }


            odd = Random.Range(0, walls.Length);

            Vector3 spawnPos = transform.position;
            spawnPos.y += (odd == 0) ? 0f : 0f;
            Quaternion rotation = (odd == 1) ? Quaternion.Euler(Vector3.up * 180f) : Quaternion.Euler(-90f,90f, 0f);

            GameObject wall = Instantiate(walls[odd], spawnPos, rotation);
            wall.transform.SetParent(obstacleContainer.transform);

            obstacleContainer.transform.SetParent(transform.parent);
        }


    }
}
