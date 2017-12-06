using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpright : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, 0);
	}
}
