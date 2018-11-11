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
        public Light muzzleFlare;
        public GameObject hitEffect;


        private void Start()
        {
            muzzleRange = muzzleFlare.range;
            muzzleDecline = (muzzleRange / coolDown);

            hitParticle = hitEffect.GetComponent<ParticleSystem>();
        }

        private float muzzleRange = 0.1f;
        public void Fire()
        {
            if (!isShooting && framesUntilUsable == 0)
            {
                isShooting = true;
                muzzleFlare.enabled = true;

                framesLeft = duration;
            }
        }

        private static Vector3[] emptyPositions = new Vector3[] { Vector3.zero, Vector3.zero };
        private int framesLeft;
        private bool isShooting = false;
        private int framesUntilUsable = 0;

        private float muzzleDecline = 0f;
        private void Update()
        {
            if (isShooting)
            {
                WarmupOrShootRay();
            }
            else if (!Mathf.Approximately(0f, muzzleFlare.range))   //cool down laser
            {
                muzzleFlare.range -= muzzleDecline;

                if (Mathf.Approximately(0f, muzzleFlare.range))
                    muzzleFlare.enabled = false;
            }
            else if(framesUntilUsable > 0)
            {
                framesUntilUsable--;
            }
        }

        private void WarmupOrShootRay()
        {
            if (!Mathf.Approximately(muzzleRange, muzzleFlare.range))   //Warm up laser
            {
                muzzleFlare.range += muzzleDecline;
            }
            else
            {
                framesLeft = Math.Max(0, framesLeft - 1);
                SendRay();

                if (framesLeft == 0)
                {
                    lineRenderer.SetPositions(emptyPositions);
                    isShooting = false;
                    framesUntilUsable = coolDown;
                    hitParticle.Stop();
                }
            }
        }

        private Vector3[] linePoints = new Vector3[2];
        private const int rayMask = ~(1 << 1);
        private ParticleSystem hitParticle;
        private void SendRay()
        {
            linePoints[0] = transform.position;
            
            var hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up).normalized, Mathf.Infinity, rayMask);
            if (hit.collider != null)
            {
                linePoints[1] = hit.point;
                hitEffect.transform.position = hit.point;

                if (!hitParticle.isPlaying)
                    hitParticle.Play();

                var health = hit.collider.GetComponent<Health>();
                if (health != null)
                    health.InflictDamage(damage);
            }
            else
            {
                linePoints[1] = transform.TransformDirection(Vector2.up).normalized * 1000;
                hitParticle.Stop();
            }

            lineRenderer.SetPositions(linePoints);
        }
    }
}
