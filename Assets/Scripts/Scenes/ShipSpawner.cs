using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class ShipSpawner : MonoBehaviour
    {
        public Camera viewport;
        public GameObject shipPrefab;
        public GameObject spawnPoint; 
        public int spawnFrames = 120;

        private void Start()
        {
            framesUntilSpawn = spawnFrames;
        }

        private int framesUntilSpawn = 120;
        private void Update()
        {
            framesUntilSpawn--;

            if (framesUntilSpawn == 0)
            {
                framesUntilSpawn = spawnFrames;
                var obj = GameObject.Instantiate(shipPrefab);

                obj.transform.position = spawnPoint.transform.position;
            }
        }
    }
}
