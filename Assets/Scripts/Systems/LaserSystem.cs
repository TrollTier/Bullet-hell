using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class LaserSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<Transform> Transforms;
            public ComponentArray<Laser> Lasers;
            public ComponentArray<HitInfo> Hits;
        }

        [Inject]
        private Data data;

        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;
            var lasersToDeactivate = new List<GameObject>();

            for(int i = 0; i < data.Length; i++)
            {
                var laser = data.Lasers[i];
                var transform = data.Transforms[i];
                var hit = data.Hits[i];

                if (!hit.HasHit)
                {
                    var oldPos = transform.position;
                    transform.position += (laser.direction * laser.properties.velocity * delta);

                    laser.flewnDistance += Vector2.Distance(oldPos, transform.position);
                    if (laser.flewnDistance >= laser.properties.reach)
                    {
                        laser.Die();
                    }
                }
                else if (!hit.Particles.isPlaying)
                {
                    hit.Particles.Stop();
                    hit.Particles.Clear();
                    lasersToDeactivate.Add(laser.gameObject);
                }
            }

            foreach (var obj in lasersToDeactivate)
                obj.SetActive(false);
        }
    }
}
