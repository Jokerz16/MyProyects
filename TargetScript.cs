using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

    public Vector3 direction;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        direction = Input.mousePosition;
        direction = Camera.main.ScreenToWorldPoint(direction);
        transform.position = new Vector2(direction.x, direction.y);
	}
}
