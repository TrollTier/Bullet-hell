using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class HealthSystem : ComponentSystem
    {
        struct Components
        {
            public Health health;
        }

        protected override void OnUpdate()
        {
            var entities = GetEntities<Components>();
            var died = new List<GameObject>();

            foreach (var e in entities)
            {
                if (e.health.hpCurrent <= 0)
                    died.Add(e.health.gameObject);
            }

            foreach (var obj in died)
                GameObject.Destroy(obj);
        }
    }
}
