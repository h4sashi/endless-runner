using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

	public GameObject player;
	public Vector3 offset;

	// Use this for initialization
	void Start()
	{
		// player = GameObject.FindGameObjectWithTag("Player");
		
	}
	// Update is called once per frame
	void LateUpdate()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		transform.position = player.transform.position + offset;
	}
}
