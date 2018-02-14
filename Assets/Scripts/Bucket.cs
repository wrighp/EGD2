using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : WaterContainer {

	public static Bucket instance;

	void Awake(){
		maxWater = 5f;
		totalWater = 0;
		isDirty = false;
		isFillable = true;
		instance = this;	

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		isTouching = true;
		base.Update();

	}

}
