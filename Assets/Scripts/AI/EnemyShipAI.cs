using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour {
    public Blaster[] blasters;
    public float speed; 

	void Start () {
        var playerObject = GameObject.FindGameObjectWithTag("Player");

        body = GetComponent<Rigidbody2D>();
        player = playerObject.transform;
        playerBody = playerObject.GetComponent<Rigidbody2D>();
	}

    private Rigidbody2D body;
    private Transform player;
    private Rigidbody2D playerBody;
    private const int rayMask = ~(1 << 1);
	void Update () {
        var playerPosNext = (Vector2)player.position + (playerBody.velocity * Time.deltaTime);
        var dir = playerPosNext - (Vector2)transform.position;
        var distance = dir.magnitude;
        transform.up = dir;

        foreach (var blaster in blasters)
        {
            if (blaster.reach >= distance && blaster.CanFire())
            {
                var hit = Physics2D.BoxCast(blaster.gameObject.transform.position, Laser.size, 0f, dir, blaster.reach, rayMask);
                if (hit.collider == null || hit.collider.gameObject == null || hit.collider.gameObject.transform == player)
                    blaster.Fire();
            }
        }

        direction = dir;
	}

    private Vector2 direction;
    private void FixedUpdate()
    {
        if (direction.magnitude >= 1)
            body.velocity = direction.normalized * speed;
        else
            body.velocity = Vector2.zero;
    }
}
