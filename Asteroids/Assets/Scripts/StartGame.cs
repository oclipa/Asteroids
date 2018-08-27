using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    [SerializeField]
    Ship prefabShip;
    [SerializeField]
    HUD prefabHUD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] asteriods = GameObject.FindGameObjectsWithTag("Asteroid");
            for (int i = 0; i < asteriods.Length; i++)
            {
                Destroy(asteriods[i]);
            }

            GameObject hud = GameObject.FindGameObjectWithTag("HUD");
            if (hud != null)
                Destroy(hud);

            Instantiate(prefabShip);
            Instantiate(prefabHUD);

            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera.GetComponent<AsteroidSpawner>().IsStarted = true;

            Destroy(gameObject);
        }
	}

    public void DisplayRestartMessage()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<AsteroidSpawner>().IsStarted = false;

        Transform promptTransform = this.transform.Find("Prompt");
        Text prompt = promptTransform.GetComponent<Text>();
        prompt.text = "You died!\nPress Spacebar to restart game";
    }
}
