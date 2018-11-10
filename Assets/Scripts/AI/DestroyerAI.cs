using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAI : MonoBehaviour {
    public float speed;
    public float rotationSpeed = 0.2f;

	void Start () {
        var playerObject = GameObject.FindGameObjectWithTag("Player");

        body = GetComponent<Rigidbody2D>();
        player = playerObject.transform;
	}

    private Transform player;
    void Update () {
        if (!Mathf.Approximately(rotationAngle, 0)) return;

        var movingLeft = transform.rotation.z > 0f;
        if (movingLeft && player.position.x > transform.position.x)
        {
            targetAngle = 270;
            rotationAngle = rotationSpeed;
        }
        else if (!movingLeft && player.position.x < transform.position.x)
        {
            targetAngle = 90f;
            rotationAngle = -rotationSpeed;
        }
	}

    private Rigidbody2D body;
    private float targetAngle;
    private float rotationAngle = 0;
    private void FixedUpdate()
    {
        body.velocity = transform.up * speed;

        if (!Mathf.Approximately(rotationAngle, 0) && !Mathf.Approximately(transform.rotation.eulerAngles.z, targetAngle))
        {
            var euler = transform.rotation.eulerAngles.z;
            var newZ = (rotationAngle > 0 ? Mathf.Min(targetAngle, euler + rotationAngle) : Mathf.Max(targetAngle, euler + rotationAngle));
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);
        }
        else
        {
            rotationAngle = 0f;
        }
    }
}
