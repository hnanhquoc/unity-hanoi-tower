using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayController : MonoBehaviour {

	private GameController game;

    public void backToLevel()
    {
        Application.LoadLevel("LevelSelected");
    }

	public void winGame ()
	{
		print ("S");
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
