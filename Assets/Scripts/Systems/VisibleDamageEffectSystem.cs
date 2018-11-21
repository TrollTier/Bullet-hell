using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(LaserSystem))]
    class VisibleDamageEffectSystem : ComponentSystem
    {
        struct Components
        {
            public Health health;
            public VisibleDamageEffect effect;
        }

        protected override void OnUpdate()
        {
            var entities = GetEntities<Components>();
            
            foreach (var e in entities)
            {
                var percent = (e.health.hpCurrent / e.health.hpMax) * 100;
                for (int i = 0; i < e.effect.healthThresholdPercent.Length; i++)
                {
                    if (e.effect.healthThresholdPercent[i] >= percent && !e.effect.damageEffect[i].isPlaying)
                    {
                        e.effect.damageEffect[i].Play();
                    }
                }
            }
        }
    }
}
