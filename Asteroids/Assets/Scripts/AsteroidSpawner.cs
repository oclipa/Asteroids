using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid spawner.
/// </summary>
public class AsteroidSpawner : MonoBehaviour {

    [SerializeField]
    Asteroid prefabAsteroid;

    private float circleCollider2DRadius;

    Timer timer;

    Direction[] directions = { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
    Vector3[] locations;

    private bool isStarted;

	// Use this for initialization
	void Start () {
        // instantiate a temporary asteroid so that we can get the radius
        Asteroid tempAsteroid = Instantiate<Asteroid>(prefabAsteroid, new Vector3(ScreenUtils.ScreenRight + 100, ScreenUtils.ScreenTop + 100), Quaternion.identity);
        // store the radius
        circleCollider2DRadius = tempAsteroid.GetComponent<CircleCollider2D>().radius;
        // destroy the GameObject for the temporary asteroid
        DestroyImmediate(tempAsteroid.gameObject);

        locations = new [] {
            new Vector3(ScreenUtils.ScreenRight + circleCollider2DRadius, 0),
            new Vector3(ScreenUtils.ScreenLeft - circleCollider2DRadius, 0),
            new Vector3(0, ScreenUtils.ScreenTop + circleCollider2DRadius),
            new Vector3(0, ScreenUtils.ScreenBottom - circleCollider2DRadius)
        };

        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2.0f;
        timer.Run();
    }
	
	// Update is called once per frame
	void Update () {
        if (isStarted && timer.Finished)
        {
            ReleaseAsteroids();
            timer.Run();
        }
	}

    void ReleaseAsteroids()
    {
        int index = Random.Range(0, 4);
        Asteroid asteroid0 = Instantiate<Asteroid>(prefabAsteroid);
        asteroid0.GetComponent<Asteroid>().Initialize(directions[index], locations[index]);
    }

    public bool IsStarted
    {
        get { return this.isStarted;  }
        set { this.isStarted = value; }
    }
}
