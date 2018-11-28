using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class ShipSpawner : MonoBehaviour
    {
        public GameObject shipPrefab;
        public GameObject spawnPoint; 
        public float spawnSeconds = 2;
        [HideInInspector]
        public float secondsToSpawn = 2;

        private void Start()
        {
            secondsToSpawn = spawnSeconds;
        }
    }
}
