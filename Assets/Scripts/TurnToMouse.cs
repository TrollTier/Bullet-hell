using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class TurnToMouse : MonoBehaviour
    {
        private void Update()
        {
            var mouseWorld = Input.mousePosition;
            mouseWorld.z = 10;
            mouseWorld = Camera.main.ScreenToWorldPoint(mouseWorld);

            var direction = (Vector2)(mouseWorld - transform.position);
            transform.up = direction;
        }
    }
}
