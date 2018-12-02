using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TankDirections
    {
        Idle,
        Forward,
        Backwards
    }

    public enum TankRotations
    {
        None, 
        Left, 
        Right
    }

    class Tank : MonoBehaviour
    {
        [HideInInspector]
        public TankDirections Direction;

        [HideInInspector]
        public TankRotations Rotation;
    }
}
