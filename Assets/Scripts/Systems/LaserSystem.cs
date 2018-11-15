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
        struct Components
        {
            public Transform transform;
            public Laser laser;
        }

        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;
            var entities = GetEntities<Components>();
            var lasersToDeactivate = new List<GameObject>();

            foreach (var e in entities)
            {
                if (e.laser.HasHit && !e.laser.hitParticle.isPlaying)
                {
                    lasersToDeactivate.Add(e.laser.gameObject);
                    e.laser.hitParticle.Stop();
                    e.laser.hitParticle.Clear();
                }
                else if (!e.laser.HasHit)
                {
                    var oldPos = e.transform.position;
                    e.transform.position += (e.laser.direction * e.laser.properties.velocity * delta);

                    e.laser.flewnDistance += Vector2.Distance(oldPos, e.transform.position);
                    if (e.laser.flewnDistance >= e.laser.properties.reach)
                    {
                        e.laser.Die();
                    }
                }
            }

            foreach (var obj in lasersToDeactivate)
                obj.SetActive(false);
        }
    }
}
