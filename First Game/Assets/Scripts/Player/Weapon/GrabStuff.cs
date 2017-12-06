using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStuff : MonoBehaviour {
    public GameObject stuff;

    // Update is called once per frame
    void Update()
    {
        stuff.transform.position = transform.position;
    }
}
