using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

	private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	public void ScreenLightOn()
	{
		anim.SetBool ("LightOn", true);
	}

	public void ScreenLightOff()
	{
		anim.SetBool ("LightOn", false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
