using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class RotatorSystem : ComponentSystem
    {
        struct Components
        {
            public Rotator rotator;
            public Transform transform;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            var entities = GetEntities<Components>();
            foreach (var e in entities)
            {
                var currentRotation = e.transform.rotation.eulerAngles.z;
                currentRotation = (currentRotation + e.rotator.rotationAngle) % 360;

                e.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            }
        }
    }
}
