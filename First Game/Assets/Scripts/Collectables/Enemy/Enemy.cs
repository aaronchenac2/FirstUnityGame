using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameObject player;
    Vitals vitals;

    int dmg = 20;

    float time = 0;
    float lastOpen = .25f;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        vitals = player.GetComponent<Vitals>();
	}

   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vitals.subtractHP(dmg);
        }
    }
}
