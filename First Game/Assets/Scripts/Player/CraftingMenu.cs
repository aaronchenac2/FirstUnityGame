using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour {
    GameObject player;
    References r;

    bool active = false;

    float time = 0;
    float lastOpen = .25f;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
	}
	
	void Update () {
        time += Time.deltaTime;
        if (Input.GetButton("CraftingMenu") && time > lastOpen)
        {
            time = 0;
            active = !active;
            r.craftingMenu.SetActive(active);
        }
    }
}
