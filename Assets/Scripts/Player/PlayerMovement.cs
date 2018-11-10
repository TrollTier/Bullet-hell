using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D body;
    private new Transform transform;

    public float speed;

    // Use this for initialization
    void Start () {
        this.transform = this.GetComponent<Transform>();
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private float currentValueX;
    private float currentValueY;
    void FixedUpdate ()
    {
        SetVelocity();
        SetDirection();

    }

    private void SetVelocity()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");
        if (inputX != currentValueX || inputY != currentValueY)
        {
            var velocities = GetVelocities(inputX, inputY);
            currentValueX = inputX;
            currentValueY = inputY;

            body.velocity = velocities;
        }
    }

    Vector2 GetVelocities(float inputX, float inputY)
    {
        float speed = this.speed;
        Vector2 velocityX = new Vector2();
        Vector2 velocityY = new Vector2();

        if (inputX <= -0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            velocityX.x = -1;
        }
        else if (inputX >= 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            velocityX.x = 1;
        }

        if (inputY <= -0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            velocityY.y = -1;
        }
        else if (inputY >= 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            velocityY.y = 1;
        }

        return (velocityX + velocityY).normalized * speed;
    }


    private void SetDirection()
    {
        var mouseWorld = Input.mousePosition;
        mouseWorld.z = 10;
        mouseWorld = Camera.main.ScreenToWorldPoint(mouseWorld);

        var direction = (Vector2)(mouseWorld - transform.position);
        transform.up = direction;
    }
}
