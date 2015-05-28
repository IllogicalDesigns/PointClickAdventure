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
		if (playerBehind) {
			tar = 0.5f;
			myRender.material.SetFloat("_Mode", 2);
			myRender.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			myRender.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			myRender.material.SetInt("_ZWrite", 0);
			myRender.material.DisableKeyword("_ALPHATEST_ON");
			myRender.material.EnableKeyword("_ALPHABLEND_ON");
			myRender.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			myRender.material.renderQueue = 3000;
		} else {
			tar = 1f;
			myRender.material.SetFloat("_Mode", 0);
			myRender.material.SetFloat("_Mode", 0);
			myRender.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			myRender.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			myRender.material.SetInt("_ZWrite", 1);
			myRender.material.DisableKeyword("_ALPHATEST_ON");
			myRender.material.EnableKeyword("_ALPHABLEND_ON");
			myRender.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			myRender.material.renderQueue = 3000;
		}
		if (aCurrent != tar) {
			aCurrent = Mathf.Lerp (aCurrent, tar, fadeTime);
			originalColour = myRender.material.color;
			myRender.material.color = new Color (originalColour.r, originalColour.g, originalColour.b, aCurrent);
		}
	}
}
