using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour {

    float waterScore = 1f;
    float currentWaterScore = 1f;
    float previousWaterScore = 1f;
    float lossAmount = 0f;
    float lossAmountPrevious = 0f;

    public float maxPeopleAlive = 5;
    public int healthyPeople = 5;
    public int sickPeople = 0;
    public int deadPeople = 0;

	// Use this for initialization
	void Start () {
        waterScore = maxPeopleAlive;
        currentWaterScore = maxPeopleAlive;
        previousWaterScore = maxPeopleAlive;
	}

    public void CalculateDailyScore(float waterAmount) {
        previousWaterScore = currentWaterScore;
        currentWaterScore = waterAmount / maxPeopleAlive;

        lossAmountPrevious = lossAmount;
        lossAmount = previousWaterScore - currentWaterScore;
        if (lossAmountPrevious > 0 && lossAmount > 0) {
            waterScore -= Mathf.Min(lossAmountPrevious, lossAmount);
        }

        if(waterScore % 1 != 0) {
            sickPeople = 1;
        } else {
            sickPeople = 0;
        }

        healthyPeople = (int)Mathf.Floor(waterScore);
        deadPeople = ((int)maxPeopleAlive - healthyPeople - sickPeople);
    }
}
