using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Restricts gameobject to certain zone
/// </summary>

public class AreaRestrict : MonoBehaviour {
	
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	//public Transform origin;
	public Transform target;

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}

	void Update(){

	}

	// Update is called once per frame
	void LateUpdate () {
		Transform camTransform = cam.transform;
		float orthoSizeX = cam.orthographicSize * cam.aspect;
		float orthoSizeY = cam.orthographicSize;

		float xPos = cam.transform.position.x;
		float yPos = cam.transform.position.y;

		float leftView = xPos  - orthoSizeX;
		float rightView = xPos + orthoSizeX;
		float downView = yPos - orthoSizeY;
		float upView = yPos  + orthoSizeY;

		Vector3 newPos = transform.position;

		if(leftView < minX){
			newPos.x = minX + orthoSizeX;
		}
		else if(rightView > maxX){
			newPos.x = maxX - orthoSizeX;
		}

		if(downView < minY){
			newPos.y = minY + orthoSizeY;
		}
		else if(upView > maxY){
			newPos.y = maxY - orthoSizeY;
		}
		transform.position = newPos;
	}

	void OnDrawGizmosSelected()
	{
		if(cam == null){
			return;
		}
		//Draw guide box
		Gizmos.color = Color.red;


		float halfX = (maxY - minY) * .5f;
		float halfY = (maxX - minX) * .5f;

		float shiftX = (maxX + minX) * .5f;
		float shiftY = (maxY + minY) * .5f;

		Vector3 xVec = Vector3.down * (halfX - shiftY);
		Vector3 xVec2 = Vector3.up * (halfX + shiftY);
		Vector3 yVec = Vector3.left * (halfY - shiftX);
		Vector3 yVec2 = Vector3.right * (halfY + shiftX);

		Vector3 vec = Vector3.right * minX;
		Gizmos.DrawLine(vec + xVec, vec + xVec2);

		Vector3 vec2 = Vector3.right * maxX;
		Gizmos.DrawLine(vec2 + xVec, vec2 + xVec2);

		Vector3 vec3 = Vector3.up * minY;
		Gizmos.DrawLine(vec3 + yVec, vec3 + yVec2);

		Vector3 vec4 = Vector3.up * maxY;
		Gizmos.DrawLine(vec4 + yVec, vec4 + yVec2);
	}

}
