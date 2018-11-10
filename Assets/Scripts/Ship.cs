using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName="Moving objects/Ship", order=0)]
    public class Ship : ScriptableObject
    {
        public float speed;
    }
}
