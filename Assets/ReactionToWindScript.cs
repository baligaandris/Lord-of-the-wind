using UnityEngine;
using System.Collections;

public class ReactionToWindScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetBlownAway(Vector3 wind) {
        gameObject.GetComponent<Rigidbody2D>().AddForce(wind, ForceMode2D.Impulse);

    }
}
