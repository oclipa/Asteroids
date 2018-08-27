using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    int score;

    float elapsedTime = 0;

    bool runTimer = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (runTimer)
        {
            elapsedTime += Time.deltaTime;
            scoreText.text = ((int)elapsedTime).ToString() + " secs : " + score.ToString() + " asteroids destroyed";
        }
	}

    public void UpdateScore()
    {
        score++;
    }

    public int Score
    {
        get { return score; }
    }

    public void Stop()
    {
        runTimer = false;
    }
}
