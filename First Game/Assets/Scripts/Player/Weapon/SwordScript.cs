using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
		if (Input.GetButton("Fire1"))
        {
            anim.SetTrigger("BasicAttack");
        }
	}
}
