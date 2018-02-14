using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIcon : MonoBehaviour {

	public Transform parentWater;
	public Transform EmptyTarget;
	public Transform FullTarget;

	public GameObject waterSprite;
	public GameObject dirtyWaterSprite;

	public bool isTouching = false;
	public float waterRatio = 0;
	public bool waterDirty = false;
	float waterlevel = 0f;
	Vector3 targetScale;
	// Use this for initialization
	void Start () {
		targetScale = transform.localScale;
		waterlevel = waterRatio;
	}
	
	// Update is called once per frame
	void Update () {
		waterSprite.SetActive(!waterDirty);
		dirtyWaterSprite.SetActive(waterDirty);

		Vector3 target = Vector3.Lerp(EmptyTarget.position,FullTarget.position, waterRatio);
		parentWater.transform.position = Vector3.Slerp(parentWater.position,target,Time.deltaTime * 2f);

		Vector3 sizeTarget = Vector3.zero;
		if(isTouching){
			sizeTarget = targetScale;
		}
		transform.localScale = Vector3.Slerp(transform.localScale, sizeTarget, Time.deltaTime * 5f);

	
	}
}
