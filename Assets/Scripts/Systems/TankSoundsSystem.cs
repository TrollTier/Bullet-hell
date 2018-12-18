using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class TankSoundsSystem : ComponentSystem
    {
        public struct Data
        {
            public readonly int Length;
            public ComponentArray<Tank> Tanks;
            public ComponentArray<TankSounds> Sounds;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            List<AudioSource> toActivate = new List<AudioSource>(data.Length);
            List<AudioSource> toDeactivate = new List<AudioSource>();

            for (int i = 0; i < data.Length; i++)
            {
                var tank = data.Tanks[i];
                var sounds = data.Sounds[i];

                var isMoving = (tank.Direction != TankDirections.Idle) || (tank.Rotation != TankRotations.None);

                if (!isMoving && sounds.Driving.isPlaying)
                {
                    toActivate.Add(sounds.Idle);
                    toDeactivate.Add(sounds.Driving);
                }
                else if (isMoving && sounds.Idle.isPlaying )
                {
                    toActivate.Add(sounds.Driving);
                    toDeactivate.Add(sounds.Idle);
                }
            }

            foreach (var sound in toDeactivate)
                sound.Stop();
            
            foreach (var sound in toActivate)
                sound.Play();
        }
    }
}
