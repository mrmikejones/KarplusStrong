using UnityEngine;
using System.Collections;

public class CreditDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUILayout.BeginVertical();
		GUILayout.Space(20);

		GUILayout.BeginHorizontal();
		GUILayout.Space (20);
		
		GUILayout.BeginVertical();
		GUILayout.Label("Mike Jones 2013 hi.MikeJones@gmail.com");
		GUILayout.Label("www.mrmikejones.com");
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
}
