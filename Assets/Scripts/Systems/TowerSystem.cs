using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class TowerSystem : ComponentSystem
    {
        struct Components
        {
            public Transform transform;
            public Tower tower;
        }

        private const int rayMask = ~(1 << 1);
        protected override void OnUpdate()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var entities = GetEntities<Components>();
            var blastersToFire = new List<Blaster>();

            foreach (var e in entities)
            {
                var dir = (Vector2)(player.transform.position - e.transform.position);
                e.transform.up = dir;

                if (dir.magnitude <= e.tower.blaster.reach && e.tower.blaster.CanFire())
                {
                    var hit = Physics2D.BoxCast(e.tower.blaster.gameObject.transform.position, Laser.size, 0f, dir, e.tower.blaster.reach, rayMask);
                    if (hit.collider != null && hit.collider.gameObject != null && (hit.collider.gameObject == player))
                        blastersToFire.Add(e.tower.blaster);
                }
            }

            foreach (var blaster in blastersToFire)
                blaster.Fire();
        }
    }
}
