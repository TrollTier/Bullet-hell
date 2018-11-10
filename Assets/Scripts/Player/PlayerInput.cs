using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public Blaster[] blasters;
    public DeathRay[] rays;

    void Update () {
        if (Input.GetMouseButton(0))
        {
            foreach (var blaster in blasters)
            {
                blaster.Fire();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var ray in rays) { ray.Fire(); }
        }
    }
}
