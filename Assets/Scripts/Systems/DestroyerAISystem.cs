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
            public DestroyerAI ai;
            public Rigidbody2D body;
            public SpriteRenderer renderer;
            public Transform transform;
        }

        protected override void OnUpdate()
        {
            var entities = GetEntities<Components>();
            var player = GameObject.FindGameObjectWithTag("Player").transform;

            foreach (var destroyer in entities)
            {
                if (destroyer.ai.ray.CanFire() && destroyer.renderer.isVisible)
                    destroyer.ai.ray.Fire();

                UpdateDestroyerPositioning(destroyer, player);
                UpdateDestroyerVelocity(destroyer);
            }
        }

        private static void UpdateDestroyerPositioning(Components destroyer, Transform player)
        {
            if (!Mathf.Approximately(destroyer.ai.rotationAngle, 0)) return;

            var movingLeft = destroyer.transform.rotation.z > 0f;
            if (movingLeft && player.position.x > destroyer.transform.position.x)
            {
                destroyer.ai.targetAngle = 270;
                destroyer.ai.rotationAngle = destroyer.ai.rotationSpeed;
            }
            else if (!movingLeft && player.position.x < destroyer.transform.position.x)
            {
                destroyer.ai.targetAngle = 90f;
                destroyer.ai.rotationAngle = -destroyer.ai.rotationSpeed;
            }
        }

        private static void UpdateDestroyerVelocity(Components destroyer)
        {
            destroyer.body.velocity = destroyer.transform.up * destroyer.ai.speed;

            if (!Mathf.Approximately(destroyer.ai.rotationSpeed, 0) && !Mathf.Approximately(destroyer.transform.rotation.eulerAngles.z, destroyer.ai.targetAngle))
            {
                var euler = destroyer.transform.rotation.eulerAngles.z;
                var newZ = (destroyer.ai.rotationAngle > 0 ? 
                    Mathf.Min(destroyer.ai.targetAngle, euler + destroyer.ai.rotationAngle) : 
                    Mathf.Max(destroyer.ai.targetAngle, euler + destroyer.ai.rotationAngle));
                destroyer.transform.rotation = Quaternion.Euler(0f, 0f, newZ);
            }
            else
            {
                destroyer.ai.rotationAngle = 0f;
            }
        }
    }
}
