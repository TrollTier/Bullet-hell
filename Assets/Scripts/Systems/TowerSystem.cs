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
        struct Data
        {
            public readonly int Length;
            public ComponentArray<Transform> Transforms;
            public ComponentArray<Tower> Towers;
        }

        [Inject] private Data data;

        private const int rayMask = ~(1 << 10);
        protected override void OnUpdate()
        {
            var player = Player.info;
            var playerPosition = player.transform.position;
            var blastersToFire = new List<Blaster>();

            for (int i = 0; i < data.Length; i++)
            {
                var tower = data.Towers[i];
                var transform = data.Transforms[i];

                var dir = (Vector2)(playerPosition - transform.position);
                transform.up = dir;

                if (dir.magnitude <= tower.blaster.reach)
                {
                    var hit = Physics2D.BoxCast(tower.blaster.gameObject.transform.position, Laser.size, 0f, dir, tower.blaster.reach, rayMask);
                    if (hit.collider != null && hit.collider.gameObject != null && (hit.collider.gameObject == player.playerObject))
                        blastersToFire.Add(tower.blaster);
                }
            }
            
            foreach (var blaster in blastersToFire)
            {
                blaster.Fire();
            }
        }
    }
}
