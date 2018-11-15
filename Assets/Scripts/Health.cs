using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float hpMax;
    public float hpCurrent;

    private void Start()
    {
        hpCurrent = hpMax;
    }

    public void InflictDamage(float damage)
    {
        hpCurrent = Mathf.Max(hpCurrent - damage, 0);
    }
}
