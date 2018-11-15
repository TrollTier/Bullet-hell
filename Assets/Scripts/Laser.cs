using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public static readonly Vector2 size = new Vector2(0.03425455f, 0.1914676f);

    public ParticleSystem hitParticle;
    public Vector3 direction;
    public LaserProperties properties;
    public float flewnDistance;

    private bool hasHit = false;
    public bool HasHit { get { return hasHit; } }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    
     public void Reset(LaserProperties properties, Vector3 direction)
    {
        this.properties = properties;
        this.direction = direction;

        hasHit = false;
        flewnDistance = 0f;

        renderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (hasHit || Equals(collision.transform.root.gameObject, properties.shotBy)) return;

        hitParticle.Play();

        var health = collision.GetComponent<Health>();
        if (health != null)
            health.InflictDamage(properties.damage);

        Die();
    }

    private new Renderer renderer;
    public void Die()
    {
        hasHit = true;
        renderer.enabled = false;
    }
}
