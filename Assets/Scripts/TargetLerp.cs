using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLerp : MonoBehaviour {
	public Transform target;
	public float period = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}
	void LateUpdate(){
		if(target == null){
			return;
		}
		Vector3 newPos = transform.position;
		newPos = Vector3.Slerp(newPos,target.position,Time.deltaTime / period);
		newPos.z = transform.position.z;
		transform.position = newPos;
	}
}
