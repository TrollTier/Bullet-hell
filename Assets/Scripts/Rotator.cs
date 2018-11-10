using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationAngle;

    private void Start()
    {
        this.parentTransform = GetComponent<Transform>();
    }

    private Transform parentTransform;
	void Update () {
        var currentRotation = transform.rotation.eulerAngles.z;
        currentRotation = (currentRotation + rotationAngle) % 360;

        parentTransform.rotation = Quaternion.Euler(0, 0, currentRotation);
	}
}
