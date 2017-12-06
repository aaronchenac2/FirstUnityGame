using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    GameObject world;

	// Use this for initialization
	void Start () {
        world = GameObject.FindGameObjectWithTag("World");
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("World"))
        {
            gameObject.SetActive(false);
        }
    }
}
