using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All timing related values are in milliseconds
public class Blaster : MonoBehaviour {

    public float velocity;
    public float reach;
    public float damage;
    public float coolDown = 0.032f;
    public ShootDirections shotDirection = ShootDirections.Up;
    public GameObject muzzle;

    private void Start()
    {
        properties = new LaserProperties
        {
            damage = damage,
            reach = reach,
            shotBy = transform.root.gameObject,
            shotDirection = shotDirection,
            velocity = velocity
        };
    }

    private float muzzleCoolDown = 0;
    private float timeToCoolDown = 0;
    void Update()
    {
        timeToCoolDown = System.Math.Max(timeToCoolDown - Time.deltaTime, 0);

        if (muzzleCoolDown > 0)
        {
            muzzleCoolDown = System.Math.Max(muzzleCoolDown - Time.deltaTime, 0);
            if (muzzleCoolDown == 0) muzzle.SetActive(false);
        }
    }

    private LaserProperties properties;
    public LaserProperties GetLaserProperties()
    {
        return properties;
    }


    public void Fire()
    {
        if (timeToCoolDown > 0)
            return;

        if (muzzle != null)
        {
            muzzle.SetActive(true);
            muzzleCoolDown = coolDown - 0.01f;
        }

        LaserFactory.CreateLaser(this);
        timeToCoolDown = coolDown;
    }

    public bool CanFire()
    {
        return (timeToCoolDown <= 0);
    }
}
