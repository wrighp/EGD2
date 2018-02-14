using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class Balance : MonoBehaviour {
    private HingeJoint2D joint;
    public float tiltSpeed = 1.5f;
    public float spillThreshold = 15;
    public float spillSpeed = 1f;

    public float hingeMin = -25;
    public float hingeStart = 0;
    public float hingeMax = 25;

    private float  sumDirection = 0;

    void Start() {
        joint = GetComponent<HingeJoint2D>();
        JointAngleLimits2D tmp = new JointAngleLimits2D();

        tmp.max = hingeStart;
        tmp.min = hingeStart;

        joint.limits = tmp;
    }

    void Update() {
        if(Input.GetKey(KeyCode.A)){
            sumDirection -= Time.deltaTime / 2;
        } else if (Input.GetKey(KeyCode.D)) {
            sumDirection += Time.deltaTime / 2;
        }
        sumDirection = Mathf.Clamp(sumDirection, -3, 3);
        float currentAngle = joint.jointAngle;
        JointAngleLimits2D tmp = new JointAngleLimits2D();
        float newAngle = 0;
        if (sumDirection < 0) {
            newAngle = currentAngle - Mathf.Pow(tiltSpeed, sumDirection);
        } else if (sumDirection > 0) {
            newAngle = currentAngle + Mathf.Pow(tiltSpeed, sumDirection);
        }
        newAngle = Mathf.Clamp(newAngle, hingeMin, hingeMax);

        tmp.max = newAngle;
        tmp.min = newAngle;

        joint.limits = tmp;

        if(Mathf.Abs(newAngle) > spillThreshold) {
            //Use spill speed to spill water here
        }
    }
}
