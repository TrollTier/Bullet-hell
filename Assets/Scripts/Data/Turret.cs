using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Assets.Scripts
{
    class Turret : MonoBehaviour
    {
        public VisualEffect muzzleFire;

        public float cooldown = 0.2f;
        private float timeToCoolDown = 0; 
        private void Update()
        {
            timeToCoolDown = Math.Max(0f, timeToCoolDown - Time.deltaTime);
            if (!Mathf.Approximately(0f, timeToCoolDown))
                return;


            if (Input.GetButton("Fire1"))
            {
                if (!muzzleFire.isActiveAndEnabled)
                    muzzleFire.enabled = true;
                else
                    muzzleFire.Play();

                timeToCoolDown = cooldown;
            }
        }
    }
}
