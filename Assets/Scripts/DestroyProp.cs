using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 8);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
