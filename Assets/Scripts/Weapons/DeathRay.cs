using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeathRay : MonoBehaviour
    {
        public float damage = 2;
        public int coolDown = 20;
        public int duration = 120;
        public LineRenderer lineRenderer;
        public GameObject muzzleFlare;
        
        private static Vector3[] emptyPositions = new Vector3[] { Vector3.zero, Vector3.zero };
        private int framesLeft;
        private bool isShooting = false;
        private int framesUntilUsable = 0;
        private void Update()
        {
            if (isShooting)
            {
                framesLeft = Math.Max(0, framesLeft - 1);
                ShootRay();

                if (framesLeft == 0)
                {
                    lineRenderer.SetPositions(emptyPositions);
                    isShooting = false;
                    muzzleFlare.SetActive(false);
                    framesUntilUsable = coolDown;
                }
            }
            else if(framesUntilUsable > 0)
            {
                framesUntilUsable--;
            }
        }

        public void Fire()
        {
            if (!isShooting && framesUntilUsable == 0)
            {
                isShooting = true;
                muzzleFlare.SetActive(true);
                framesLeft = duration;

                ShootRay();
            }
        }

        private Vector3[] linePoints = new Vector3[2];
        private const int rayMask = ~(1 << 1);
        private void ShootRay()
        {
            linePoints[0] = transform.position;
            
            var hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up).normalized, Mathf.Infinity, rayMask);
            if (hit.collider != null)
            {
                linePoints[1] = hit.collider.transform.position;
                //hitFlare.transform.position = hit.collider.transform.position;
                //hitFlare.SetActive(true);

                var health = hit.collider.GetComponent<Health>();
                if (health != null)
                    health.InflictDamage(damage);
            }
            else
            {
                linePoints[1] = transform.TransformDirection(Vector2.up).normalized * 1000;
                //hitFlare.SetActive(false);
            }

            lineRenderer.SetPositions(linePoints);
        }
    }
}
