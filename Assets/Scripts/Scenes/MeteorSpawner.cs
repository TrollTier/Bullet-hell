using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scenescripts
{
    public class MeteorSpawner : MonoBehaviour
    {
        public Camera viewport;
        public GameObject meteorPrefab;
        public Transform playerPosition;
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
                var obj = GameObject.Instantiate(meteorPrefab);

                var x = Screen.width - 32;
                var y = UnityEngine.Random.Range(0, Screen.height - 32);
                var velocity = UnityEngine.Random.Range(0.02f, 1.0f);
                obj.transform.position = viewport.ScreenToWorldPoint (new Vector3(x, y, 10));

                var dir = (playerPosition.position - obj.transform.position);
                dir.Normalize();
                dir *= velocity;
                obj.GetComponent<Rigidbody2D>().velocity = dir;
                obj.GetComponent<Rotator>().rotationAngle = velocity;
            }
        }
    }
}
