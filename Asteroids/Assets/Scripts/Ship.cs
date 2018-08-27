using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ship
/// </summary>
public class Ship : MonoBehaviour {

    [SerializeField]
    Bullet prefabBullet;
    [SerializeField]
    StartGame prefabStartGame;

    Rigidbody2D shipRigidBody2d;

    // the direction of the force applied to the ship
    Vector2 thrustDirection = new Vector2(1, 0);

    // the amount of force applied to the ship by the user
    const float thrustForce = 5.0f;

    // the radius of the collider (which approximates to the radius of the ship)
    float shipColliderRadius;

    // the speed with which the ship rotates
    const float RotateDegreesPerSecond = 90;

	// Use this for initialization
	void Start () {
        shipRigidBody2d = GetComponent<Rigidbody2D>();
        shipColliderRadius = GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {
        float rotation = Input.GetAxis("Rotate");
        if (rotation != 0) // if we have rotation specified by the user
        {
            // get the amount of rotation amount
            float rotationAmountInDegrees = RotateDegreesPerSecond * Time.deltaTime;

            // if rotation direction is negative, change the amount accordingly
            if (rotation > 0)
                rotationAmountInDegrees *= -1;

            // apply the new rotation
            transform.Rotate(Vector3.forward, rotationAmountInDegrees);

            // update the thrustDirection to be along the new ship direction
            float currentZRotationInDegrees = transform.eulerAngles.z;
            float currentZRotationInRadians = currentZRotationInDegrees * Mathf.Deg2Rad;
            thrustDirection= new Vector2(Mathf.Cos(currentZRotationInRadians), Mathf.Sin(currentZRotationInRadians));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.Play(AudioClipName.PlayerShot);

            Bullet bullet = Instantiate<Bullet>(prefabBullet, transform.position, Quaternion.identity);
            bullet.ApplyForce(thrustDirection);
        }
	}

    /// <summary>
    /// Applies a force to the ship's Rigidbody2D every fixed framerate frame
    /// </summary>
    private void FixedUpdate()
    {
        // get the thrust indicated by the user
        float thrust = Input.GetAxis("Thrust");
        if (thrust > 0) // if we have thrust
        {
            // apply the thrust to the ship
            shipRigidBody2d.AddForce(thrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// When the ship goes off the edge of the screen, we want it to reappear 
    /// on the opposite side of the screen.
    /// </summary>
    private void OnBecameInvisible()
    {
        ScreenWrapper.ScreenWrap(transform, shipColliderRadius);
    }

    /// <summary>
    /// Handles collisions between ship and other objects
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Asteroid"))
        {
            // Reduces the transparancy of the ship with each hit
            //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            //Color color = spriteRenderer.color;
            //color.a -= 0.2f;
            //spriteRenderer.color = color;

            AudioManager.Play(AudioClipName.PlayerDeath);

            HUD hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

            hud.Stop();

            Destroy(this.gameObject);

            StartGame startGame = Instantiate<StartGame>(prefabStartGame);
            startGame.DisplayRestartMessage();
        }
    }
}
