using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectedController : MonoBehaviour {

    public void levelOne()
    {
        Application.LoadLevel("GamePlay");
    }

    public void backToHome()
    {
        Application.LoadLevel("MainMenu");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
