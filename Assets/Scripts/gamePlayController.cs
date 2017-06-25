using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayController : MonoBehaviour {

	private GameController gc;

    public void backToLevel()
    {
        Application.LoadLevel("LevelSelected");
    }

	public void winGame ()
	{
		print ("S");
		gc.triggerAutoPlay ();
	}

    // Use this for initialization
    void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		if (gc == null) throw new MissingComponentException("Game Controller not found!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
