using UnityEngine;
using System.Collections;

public class Synth : MonoBehaviour {
	public int voiceindex = 0;
	public KarplusStrongGenerator[] generators;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Trigger(float velocity, Vector3 position, float direction)
	{
		float strength = Mathf.Clamp(velocity / 40.0f, 0.0f, 1.0f);
		float upperfreq = 250.0f + ((1.0f - strength) * (800.0f - 250.0f));
		
		float frequency = UnityEngine.Random.Range(80.0f, upperfreq);
		
		Vector3 distanceVec = Camera.mainCamera.transform.position - position;
		float distance = distanceVec.magnitude;
		float pickPos = Mathf.Clamp01(distance + 3.0f / 18.0f);
		
		generators[voiceindex].Trigger(frequency, strength, direction, pickPos);
		
		voiceindex++;
		if(voiceindex >= generators.Length) voiceindex = 0;
	}
}
