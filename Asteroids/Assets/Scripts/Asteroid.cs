using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{

    [SerializeField]
    Asteroid prefabAsteroid;

    // asteroid sprites
    [SerializeField]
    Sprite asteroid0;
    [SerializeField]
    Sprite asteroid1;
    [SerializeField]
    Sprite asteroid2;

    // the radius of the collider (which approximates to the radius of the asteroid)
    float colliderRadius;

    // HUD
    HUD hud;

    // initialize asteroid
    public void Initialize(Direction direction, Vector3 location)
    {
        // set initial location of asteroid
        transform.position = location;

        float randAngle = Random.Range(0f, 30f);
        float angleInDegrees = 0;

        // pick random angle
        switch (direction)
        {
            case Direction.Up:
                angleInDegrees = 75 + randAngle;
                break;
            case Direction.Down:
                angleInDegrees = 255 + randAngle;
                break;
            case Direction.Left:
                angleInDegrees = 165 + randAngle;
                break;
            case Direction.Right:
                angleInDegrees = 345 + randAngle;
                break;
            default:
                angleInDegrees = 0;
                break;
        }

        StartMoving(this, angleInDegrees);
    }

    // Use this for initialization
    void Start()
    {

        // get the radius of the collider
        colliderRadius = GetComponent<CircleCollider2D>().radius;

        // set asteroid sprite
        int asteroidNum = Random.Range(0, 3);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (asteroidNum == 0)
        {
            spriteRenderer.sprite = asteroid0;
        }
        else if (asteroidNum == 1)
        {
            spriteRenderer.sprite = asteroid1;
        }
        else if (asteroidNum == 2)
        {
            spriteRenderer.sprite = asteroid2;
        }

        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }


    /// <summary>
    /// When the asteroid goes off the edge of the screen, we want it to reappear 
    /// on the opposite side of the screen.
    /// </summary>
    private void OnBecameInvisible()
    {
        ScreenWrapper.ScreenWrap(transform, colliderRadius);
    }


    /// <summary>
    /// Handles collisions between asteroid and other objects
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);

            Vector3 localScale = transform.localScale;

            if (localScale.x >= 0.5)
            {
                localScale.x /= 2;
                localScale.y /= 2;
                transform.localScale = localScale;

                colliderRadius /= 2;

                createAsteroid(localScale);
                createAsteroid(localScale);
            }
            else // < 0.5
            {
                hud.UpdateScore();
            }

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void createAsteroid(Vector3 localScale)
    {
        Asteroid chunk = Instantiate<Asteroid>(prefabAsteroid, transform.position, Quaternion.identity);

        chunk.transform.localScale = localScale;

        StartMoving(chunk, Random.Range(0f, 360f));
    }

    public void StartMoving(Asteroid asteroid, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // apply impulse force to get game object moving
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;

        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // pick random force
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);

        // apply specified force in specified direction
        asteroid.GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}