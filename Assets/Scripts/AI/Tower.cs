using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public Blaster blaster;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    GameObject player;
    private const int rayMask = ~(1 << 1);
    void Update () {
        var dir = (Vector2)(player.transform.position - transform.position);
        transform.up = dir;

        if (dir.magnitude <= blaster.reach && blaster.CanFire())
        {
            var hit = Physics2D.BoxCast(blaster.gameObject.transform.position, Laser.size, 0f, dir, blaster.reach, rayMask);
            if (hit.collider != null && hit.collider.gameObject != null && string.Equals(hit.collider.gameObject.tag, "Player"))
                blaster.Fire();
        }
	}
}
