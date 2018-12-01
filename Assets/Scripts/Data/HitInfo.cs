using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class HitInfo : MonoBehaviour
    {
        [HideInInspector]
        public bool HasHit;
        public ParticleSystem Particles;
    }
}
