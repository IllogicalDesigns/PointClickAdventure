using UnityEngine;
using System.Collections;

public class FadeObject : MonoBehaviour
{
	Renderer myRender;
	public GameObject myObject;
	public float fadeTime = 0.05f;
	float aCurrent = 1f;
	float tar = 1f;
	Color originalColour;
	bool playerBehind = false;
	public bool canStealthBehind = true;

	// Use this for initialization
	void Start ()
	{
		myRender = myObject.GetComponent<Renderer> ();
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			if (canStealthBehind) {
				PlayerMoveTEst tempMove = col.gameObject.GetComponent<PlayerMoveTEst> ();
				tempMove.behindStealthableObj = true;
			}
			playerBehind = true;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			if (canStealthBehind) {
				PlayerMoveTEst tempMove = col.gameObject.GetComponent<PlayerMoveTEst> ();
				tempMove.behindStealthableObj = false;
			}
			playerBehind = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerBehind) 
			tar = 0.1f;
		else
			tar = 1f;
		aCurrent = Mathf.Lerp (aCurrent, tar, fadeTime);
		originalColour = myRender.material.color;
		myRender.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, aCurrent);
	}
}
