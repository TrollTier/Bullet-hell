using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    [UpdateBefore(typeof(TankSystem))]
    class TankInputSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<TankInput> Inputs;
            public ComponentArray<Rigidbody2D> Bodies;
            public ComponentArray<Tank> Tanks;
            public ComponentArray<Transform> Transforms;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            var dir = GetDirection();
            var rotation = GetRotation();
                       
            for (int i = 0; i < data.Length; i++)
            {
                var body = data.Bodies[i];
                var transform = data.Transforms[i];
                var tank = data.Tanks[i];

                tank.Rotation = rotation;
                tank.Direction = dir;
            }
        }

        private static TankDirections GetDirection()
        {
            var inputY = Input.GetAxisRaw("Vertical");
            if (inputY <= -0.5f)
                return TankDirections.Backwards;
            else if (inputY >= 0.5f)
                return TankDirections.Forward;

            return TankDirections.Idle;
        }

        private static TankRotations GetRotation()
        {
            var inputX = Input.GetAxisRaw("Horizontal");

            if (inputX >= 0.5f)
                return TankRotations.Left;
            else if (inputX <= -0.5f)
                return TankRotations.Right;
            else
                return TankRotations.None;
        }
    }
}
