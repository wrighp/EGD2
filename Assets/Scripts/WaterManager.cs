using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterManager : MonoBehaviour {

    public static WaterManager i;

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
        i = this;
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

        int deadColor = deadPeople;
        int sickColor = sickPeople;
        foreach(Image i in GameObject.Find("FamilyHolder").GetComponentsInChildren<Image>()) {
            if (deadColor > 0) {
                i.color = new Color(0, 0, 0);
                deadColor--;
            } else if (sickColor > 0) {
                i.color = new Color(1, 0, 0);
                sickColor--;
            } else {
                i.color = new Color(1, 1, 1);
            }
        }
    }
}
