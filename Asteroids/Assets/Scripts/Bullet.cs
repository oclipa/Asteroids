using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages bullets
/// </summary>
public class Bullet : MonoBehaviour {

    // the radius of the collider (which approximates to the radius of the bullet)
    float colliderRadius;

    const float lifeTime = 1.0f;
    Timer timer;

	// Use this for initialization
	void Start () {
        // get the radius of the collider
        colliderRadius = GetComponent<CircleCollider2D>().radius;

        timer = GetComponent<Timer>();
        timer.Duration = lifeTime;
        timer.Run();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer.Finished)
        {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// Applies a force to the bullet
    /// </summary>
    /// <param name="direction">Direction.</param>
    public void ApplyForce(Vector2 direction)
    {
        const float force = 10f;

        GetComponent<Rigidbody2D>().AddForce(force * direction, ForceMode2D.Impulse);
    }


    /// <summary>
    /// When the asteroid goes off the edge of the screen, we want it to reappear 
    /// on the opposite side of the screen.
    /// </summary>
    private void OnBecameInvisible()
    {
        ScreenWrapper.ScreenWrap(transform, colliderRadius);
    }
}
