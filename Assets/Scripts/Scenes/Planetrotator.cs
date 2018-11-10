using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class Planetrotator : MonoBehaviour
    {
        public float rotationY = 0.5f;
        
        void Update()
        {
            var currentRotation = transform.rotation.eulerAngles.y;
            currentRotation = (currentRotation + rotationY) % 360;

            transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }
}
