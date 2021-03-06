using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterContainer : MonoBehaviour {

	public WaterIcon icon;
	public float maxWater = 10f;
	public float totalWater = 10f;
	public bool isDirty = false;
	public bool isFillable = false;
	public bool isDrainable = true;
	protected bool isTouching = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(icon != null){
			icon.waterDirty = isDirty;
			icon.waterRatio = totalWater / maxWater;
			icon.isTouching = isTouching;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Player"){
			return;
		}
		isTouching = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag != "Player"){
			return;
		}
		isTouching = false;
        Rigidbody2D phys = other.GetComponent<Bucket>().bucket.GetComponent<Rigidbody2D>();
        phys.freezeRotation = false;
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag != "Player"){
			return;
		}
		isTouching = true;
		Bucket otherWater = other.gameObject.GetComponent<Bucket>();
		if(Input.GetKeyDown(KeyCode.Space)){
			//If this source can be drained, empty it into the bucket
			if(isDrainable){
				EmptyInto(otherWater);
				if(otherWater.totalWater > 0){
					other.GetComponentInChildren<Animator>().SetBool("Lift", true);
                    Rigidbody2D phys = otherWater.bucket.GetComponent<Rigidbody2D>();
                    phys.freezeRotation = true;
                    otherWater.bucket.localRotation = Quaternion.identity;
				}
			}
			//If this source can be filled (house), take water from the bucket
			if(isFillable){
				otherWater.EmptyInto(this);
				if(otherWater.totalWater <= 0){
					other.GetComponentInChildren<Animator>().SetBool("Lift", false);
				}
                if(name == "House" && totalWater >= maxWater)
                {
                    //End day
                    foreach(WaterContainer wc in GameObject.FindObjectsOfType<WaterContainer>()){
                        if (wc.name == "House") continue;
                        wc.maxWater = wc.maxWater / 2;
                        wc.totalWater = wc.maxWater;
                    }
                    FadeSystem.i.PerformMultiFadeCall();
                    WaterManager.i.CalculateDailyScore(totalWater);
                }
			}
		}
	}

	/// <summary>
	/// Fill the specified amount and return the overflow amount;
	/// </summary>
	/// <param name="amount">Amount of water to add.</param>
	/// <param name="dirty">If set to <c>true</c> water added is dirty.</param>
	public float Fill(float amount, bool dirty){
		if(!isFillable){
			//Can't fill
			return amount;
		}

		//Sets water to dirty if it isn't already dirty
		isDirty = !isDirty ? dirty : isDirty;

		totalWater += amount;
		float remainder = totalWater - maxWater;
		remainder = remainder > 0 ? remainder : 0;
		totalWater = Mathf.Min(totalWater,maxWater);
		return remainder;
	}

	public void EmptyInto(WaterContainer container){
		if(totalWater <= 0){
			return;
		}
		float overflow = container.Fill(totalWater, isDirty);
		//If any water was actually added
		if(overflow < totalWater){
			SoundManager.GetSound("run_water_1",pitchShiftRange: .15f).Play();
		}

		totalWater = overflow;

		if(totalWater <= 0){
			isDirty = false;
		}
	}
}
