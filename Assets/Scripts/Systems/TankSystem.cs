using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class TankSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<Rigidbody2D> Bodies;
            public ComponentArray<Tank> Tanks;
            public ComponentArray<TankData> TankInformation;
            public ComponentArray<Transform> Transforms;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            for (int i = 0; i < data.Length; i++)
            {
                var body = data.Bodies[i];
                var tank = data.Tanks[i];
                var info = data.TankInformation[i];
                var transform = data.Transforms[i];

                RotateTank(tank, info, transform);
                body.velocity = GetVelocity(tank, info, transform);
            }
        }

        private static void RotateTank(Tank tank, TankData info, Transform transform)
        {
            switch (tank.Rotation)
            {
                case TankRotations.Left:
                    transform.Rotate(new Vector3(0, 0, -info.RotationSpeed));
                    break;

                case TankRotations.Right:
                    transform.Rotate(new Vector3(0, 0, info.RotationSpeed));
                    break;
            }
        }

        private static Vector2 GetVelocity(Tank tank, TankData info, Transform transform)
        {
            switch(tank.Direction)
            {
                case TankDirections.Backwards: return transform.up.normalized * (-info.Speed);
                case TankDirections.Forward: return transform.up.normalized * (info.Speed);
                case TankDirections.Idle: return Vector2.zero;
            }

            return Vector2.zero;
        }
    }
}
