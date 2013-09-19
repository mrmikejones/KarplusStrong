using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {
	public Synth synth;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
		synth.Trigger(collision.relativeVelocity.magnitude, collision.transform.localPosition, gameObject.rigidbody.velocity.y);
    }
}
