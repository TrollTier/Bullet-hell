using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public static readonly Vector2 size = new Vector2(0.03425455f, 0.1914676f);

    private Vector3 direction;
    private LaserProperties properties;

    private float flewnDistance;
    void FixedUpdate () {
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
        
        flewnDistance = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Equals(collision.transform.root.gameObject, properties.shotBy)) return;

        var health = collision.GetComponent<Health>();
        if (health != null)
            health.InflictDamage(properties.damage);

        Die();
    }

    private void Die()
    {
        LaserFactory.AddDeadLaser(gameObject);
    }
}
