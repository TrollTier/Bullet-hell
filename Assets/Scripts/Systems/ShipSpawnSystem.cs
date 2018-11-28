using Assets.Scripts.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class ShipSpawnSystem : ComponentSystem
    {
        struct Components
        {
            public ShipSpawner spawner;
        }

        protected override void OnUpdate()
        {
            var entities = GetEntities<Components>();
            var spawningThisFrame = new List<ShipSpawner>();

            foreach (var e in entities)
            {
                e.spawner.secondsToSpawn = Math.Max(0, e.spawner.secondsToSpawn - Time.deltaTime);

                if (Mathf.Approximately(0, e.spawner.secondsToSpawn))
                {
                    e.spawner.secondsToSpawn = e.spawner.spawnSeconds;
                    spawningThisFrame.Add(e.spawner);
                }
            }

            GameObject instantiated;
            foreach (var spawner in spawningThisFrame)
            {
                instantiated = GameObject.Instantiate(spawner.shipPrefab);
                instantiated.transform.position = spawner.spawnPoint.transform.position;
            }
        }
    }
}
