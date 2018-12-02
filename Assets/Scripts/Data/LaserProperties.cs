using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ShootDirections
{
    Up,
    Down
}

public class ShotInfo
{
    public ShootDirections shotDirection;
    public GameObject shotBy;
}

public class LaserProperties
{
    public float velocity;
    public float reach;
    public float damage;
}
