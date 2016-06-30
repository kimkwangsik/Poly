﻿using UnityEngine;
using System.Collections;

public class csSkillAction : MonoBehaviour {
	public GameObject pointsprite;

	GameObject player;

	Vector3 playerPos;
	Vector3 StartplayerPos;
	Vector3 moveplayerPos;

	bool startSkill = false;


	public float timestoplimit = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerPos = player.transform.position + new Vector3(-10.0f, 0.0f, -5.0f);
		moveplayerPos = playerPos;
		for (int x = 0; x < 20; x++) {
			for (int z = 0; z < 10; z++) {
				GameObject goTemp =  Instantiate (pointsprite, moveplayerPos,
					Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform;
				moveplayerPos += Vector3.forward * 1.0f;
			}
			playerPos += Vector3.right * 1.0f;
			moveplayerPos = playerPos;

		}

		transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		startSkill = true;
	}
	
	// Update is called once per frame
	void Update () {
		//if (startSkill) {
		//	timestoplimit += Time.unscaledDeltaTime;

		//	if (timestoplimit > 5) {
		//		Destroy (gameObject);
		//	}
		//}
	}
}
