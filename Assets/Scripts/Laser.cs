using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public static readonly Vector2 size = new Vector2(0.03425455f, 0.1914676f);

    public Vector3 direction;
    public LaserProperties properties;
    public float flewnDistance;
    public HitInfo hitInfo;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        hitInfo = GetComponent<HitInfo>();
    }

    
     public void Reset(LaserProperties properties, Vector3 direction)
    {
        this.properties = properties;
        this.direction = direction;

        hitInfo.HasHit = false;
        flewnDistance = 0f;

        renderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (hitInfo.HasHit || Equals(collision.transform.root.gameObject, properties.shotBy)) return;

        hitInfo.Particles.Play();

        var health = collision.GetComponent<Health>();
        if (health != null)
            health.InflictDamage(properties.damage);

        Die();
    }

    private new Renderer renderer;
    public void Die()
    {
        hitInfo.HasHit= true;
        renderer.enabled = false;
    }
}
