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
    class BlasterSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<Blaster> Blasters;
        }

        [Inject]
        private Data data;

        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;

            for (int i = 0; i < data.Length; i++)
            {
                var blaster = data.Blasters[i];
                blaster.Cooldown();
            }
        }
    }
}
