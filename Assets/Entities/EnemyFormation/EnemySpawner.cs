﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5;

	private bool movingRight = true;
	private float xmax;
	private float xmin;

	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		foreach( Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	void Update () {
		if (movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOrFormation = transform.position.x + (0.5f*width);
		float leftEdgeOrFormation = transform.position.x - (0.5f*width);
		if (leftEdgeOrFormation < xmin) {
			movingRight = true;
		} else if(rightEdgeOrFormation > xmax) {
			movingRight = false;
		}
	}
}
