using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterManager : MonoBehaviour {

    public static WaterManager i;
	public List<GameObject> graves;

    float waterScore = 1f;
    float currentWaterScore = 1f;
    float previousWaterScore = 1f;
    float lossAmount = 0f;
    float lossAmountPrevious = 0f;

    public float maxPeopleAlive = 5;
    public int healthyPeople = 5;
    public int sickPeople = 0;
    public int deadPeople = 0;

    int[] healthStatus;

	// Use this for initialization
	void Start () {
        i = this;
        waterScore = maxPeopleAlive;
        currentWaterScore = maxPeopleAlive;
        previousWaterScore = maxPeopleAlive;

        healthStatus = new int[] { 2,2,2,2,2 };

        CalculateDailyScore(maxPeopleAlive);
	}

    public void CalculateDailyScore(float waterAmount) {
        previousWaterScore = currentWaterScore;
        currentWaterScore = waterAmount;

        lossAmountPrevious = lossAmount;
        lossAmount = previousWaterScore - currentWaterScore;
        if (lossAmountPrevious > 0 && lossAmount > 0) {
            waterScore -= Mathf.Min(lossAmountPrevious, lossAmount);
        }

        for(int i = 0; i < 5; ++i) {
            if (currentWaterScore < i) {
                healthStatus[i] -= 1; //Set sick or dead
            } else if (i <= currentWaterScore && healthStatus[i] != 0) {
                healthStatus[i] = 2; //Set healthy again
            }
        }

        int j = 0;
        int deadPeople = 0;
        foreach(Image i in GameObject.Find("FamilyHolder").GetComponentsInChildren<Image>()) {
            if (healthStatus[j] == 0) {
                i.color = new Color(0, 0, 0);
                deadPeople++;
            } else if (healthStatus[j] == 1) {
                i.color = new Color(1, 0, 0);
            } else {
                i.color = new Color(1, 1, 1);
            }
            j++;
        }

		for(int i = 0; i < graves.Count; i++){
			graves[i].SetActive(i < deadPeople);
		}
    }
}
