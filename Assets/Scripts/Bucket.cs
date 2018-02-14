using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : WaterContainer {

	public static Bucket instance;
    public Transform bucket;

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
        if(Mathf.Abs(bucket.rotation.eulerAngles.z) > 90f && bucket.GetComponent<Rigidbody2D>().freezeRotation == false)
        {
            totalWater = 0;
        }
	}

}
