using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAuto : MonoBehaviour {
    GameObject player;
    References r;

    int handDmg = 20; //1
    float time = 0;
    float lastAttack = .25f;
    int selfHarm;

	void Start () {
        selfHarm = handDmg / 2;
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
	}

    private void OnTriggerStay(Collider other)
    {
        if (r.toolsManagerScript.HasChildren() == false)
        {
            if (other.CompareTag("Trees"))
            {
                if (Input.GetButton("Fire1") && time > lastAttack)
                {
                    time = 0;
                    CutTree ct = other.GetComponent<CutTree>();
                    ct.Durability -= handDmg;
                    r.vitals.subtractHP(selfHarm);
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
}
