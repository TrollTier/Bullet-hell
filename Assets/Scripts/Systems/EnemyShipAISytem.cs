using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class EnemyShipAISytem : ComponentSystem
    {
        struct Components
        {
            public EnemyShipAI ai;
            public Transform transform;
            public Rigidbody2D body;
        }

        private const int rayMask = ~(1 << 10);
        protected override void OnUpdate()
        {
            var entities = GetEntities<Components>();
            if (entities.Length == 0) return;

            var playerInfo = Player.info;
            var playerPosNext = (Vector2)playerInfo.transform.position + (playerInfo.body.velocity * Time.deltaTime);

            var blastersToShoot = new List<Blaster>(100);
            foreach(var ai in entities)
            {
                UpdateAi(ai, playerPosNext, playerInfo.playerObject, blastersToShoot);
            }

            foreach (var blaster in blastersToShoot)
                blaster.Fire();
        }

        private static void UpdateAi(Components ai, Vector2 playerPosNext, GameObject player, List<Blaster> blastersToShoot)
        {
            var dir = playerPosNext - (Vector2)ai.transform.position;
            var distance = dir.magnitude;
            ai.transform.up = dir;

            foreach (var blaster in ai.ai.blasters)
            {
                if (blaster.reach >= distance && blaster.CanFire())
                {
                    var hit = Physics2D.BoxCast(blaster.gameObject.transform.position, Laser.size, 0f, dir, blaster.reach, rayMask);
                    if (hit.collider == null || hit.collider.gameObject == null || hit.collider.gameObject == player)
                        blastersToShoot.Add(blaster);
                }
            }

            if (dir.magnitude >= 1)
                ai.body.velocity = dir.normalized * ai.ai.speed;
            else
                ai.body.velocity = Vector2.zero;
        }
    }
}
