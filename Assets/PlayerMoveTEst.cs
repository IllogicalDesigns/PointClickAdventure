using UnityEngine;
using System.Collections;

public class PlayerMoveTEst : MonoBehaviour {
	public NavMeshAgent agent;
	public bool behindStealthableObj = false;		//I am Behind something

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
			if (Physics.Raycast (ray, out hit)) {
				Transform objectHit = hit.transform;
				Debug.DrawLine (Camera.main.transform.position, hit.point, Color.red);
				NavMeshHit navHit;
				NavMesh.SamplePosition(hit.point, out navHit, 20f, NavMesh.AllAreas);
				agent.SetDestination(navHit.position);
			}
		}
	}
}
