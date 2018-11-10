using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTower : MonoBehaviour {

    public Blaster[] blasters;
	
	// Update is called once per frame
	void Update () {
		foreach (var blaster in blasters)
        {
            blaster.Fire();
        }
	}
}
