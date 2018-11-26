using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        public static PlayerInfo info;

        void Start()
        {
            info = new PlayerInfo
            {
                body = GetComponent<Rigidbody2D>(),
                transform = transform, 
                playerObject = gameObject
            };
        }
    }

    struct PlayerInfo
    {
        public Transform transform;
        public Rigidbody2D body;
        public GameObject playerObject;
    }
}
