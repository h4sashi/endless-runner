using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;

    public void GenerateObstacles()
    {
        foreach (GameObject obs in obstacles)
        {
            obs.GetComponent<WallSpawner>().GenerateWall();
        }
    }
    // public void DisableObstacles()
    // {
    //     Transform parent = transform;
    //      Transform obstacleContainer = FindChildByTag(parent, "ObstacleContainer");
    //      obstacleContainer.gameObject.SetActive(false);
    // }
    
    // private Transform FindChildByTag(Transform parent, string tag)
    // {
    //     foreach (Transform child in parent)
    //     {
    //         if (child.CompareTag(tag))
    //         {
    //             return child; // Found the child with the tag
    //         }
            
          
    //     }

    //     return null; // Child with the tag not found in this branch
    // }
}
