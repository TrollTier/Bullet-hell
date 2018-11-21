using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DestroyerAI : MonoBehaviour {
    public float speed;
    public float rotationSpeed = 0.2f;
    public DeathRay ray;
    [HideInInspector]
    public float targetAngle;
    [HideInInspector]
    public float rotationAngle = 0;
}
