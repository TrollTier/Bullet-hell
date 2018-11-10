using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float hpMax;
    private float hpCurrent;

    private void Start()
    {
        hpCurrent = hpMax;
    }

    public void InflictDamage(float damage)
    {
        hpCurrent = Mathf.Max(hpCurrent - damage, 0);

        if (hpCurrent == 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetCurrentHp()
    {
        return hpCurrent;
    }
}
