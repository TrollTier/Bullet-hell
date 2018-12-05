using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class DestroyerAISystem : ComponentSystem
    {
        struct Components
        {
            public readonly int Length;
            public ComponentArray<DestroyerAI> AIs;
            public ComponentArray<Rigidbody2D> Bodies;
            public ComponentArray<SpriteRenderer> Renderers;
            public ComponentArray<Transform> Transforms;
        }

        [Inject] private Components data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            var player = Player.info.transform;
            for (int i = 0; i < data.Length; i++)
            {
                var ai = data.AIs[i];
                var body = data.Bodies[i];
                var renderer = data.Renderers[i];
                var transform = data.Transforms[i];

                if (ai.ray.CanFire() && renderer.isVisible)
                    ai.ray.Fire();

                UpdateDestroyerPositioning(ai, transform, player);
                UpdateDestroyerVelocity(ai, transform, body);
            }
        }

        private static void UpdateDestroyerPositioning(DestroyerAI ai, Transform transform, Transform player)
        {
            if (!Mathf.Approximately(ai.rotationAngle, 0)) return;

            var movingLeft = transform.rotation.z > 0f;
            if (movingLeft && player.position.x > transform.position.x)
            {
                ai.targetAngle = 270;
                ai.rotationAngle = ai.rotationSpeed;
            }
            else if (!movingLeft && player.position.x < transform.position.x)
            {
                ai.targetAngle = 90f;
                ai.rotationAngle = -ai.rotationSpeed;
            }
        }

        private static void UpdateDestroyerVelocity(DestroyerAI ai, Transform transform, Rigidbody2D body)
        {
            body.velocity = transform.up * ai.speed;

            if (!Mathf.Approximately(ai.rotationSpeed, 0) && !Mathf.Approximately(transform.rotation.eulerAngles.z, ai.targetAngle))
            {
                var euler = transform.rotation.eulerAngles.z;
                var newZ = (ai.rotationAngle > 0 ? 
                    Mathf.Min(ai.targetAngle, euler + ai.rotationAngle) : 
                    Mathf.Max(ai.targetAngle, euler + ai.rotationAngle));
                transform.rotation = Quaternion.Euler(0f, 0f, newZ);
            }
            else
            {
                ai.rotationAngle = 0f;
            }
        }
    }
}
