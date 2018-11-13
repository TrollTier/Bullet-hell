using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public static readonly Vector2 size = new Vector2(0.03425455f, 0.1914676f);

    public ParticleSystem hitParticle;

    private Vector3 direction;
    private LaserProperties properties;


    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private bool hasHit = false;
    private void Update()
    {
        if (hasHit && !hitParticle.isPlaying)
        {
            gameObject.SetActive(false);
            hitParticle.Stop();
            hitParticle.Clear();
        }
    }

    private float flewnDistance;
    void FixedUpdate () {
        if (hasHit) return;

        var oldPos = transform.position;
        transform.position += (direction * properties.velocity * Time.deltaTime);
        
        flewnDistance += Vector2.Distance(oldPos, transform.position);
        if (flewnDistance >= properties.reach)
        {
            Die();
        }
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
    private void Die()
    {
        hasHit = true;
        renderer.enabled = false;
    }
}
