using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour {
    GameObject world;
    GameObject player;
    GameObject canvas;
    GameObject menu;
    Text text;

    References r;
    Vitals vitals;
    SpawnStuff spawnStuff;
    Inventory inventory;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
        world = GameObject.FindWithTag("World");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        vitals = player.GetComponent<Vitals>();
        spawnStuff = world.GetComponent<SpawnStuff>();
        inventory = player.GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            menu = r.menu;
            text = menu.GetComponentInChildren<Text>();
            menu.SetActive(true);
            text.text = "Press F to pay respects.";
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetButton("Collect")) //Adds to inventory 
            {
                player.GetComponent<AudioSource>().Play();
                gameObject.SetActive(false);
                inventory.AddItem(name, gameObject.tag);
                Respawn();
            }
        }
        if (other.CompareTag("Boat"))
        {
            player.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
            inventory.AddItem(name, gameObject.tag);
            Respawn();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            menu.SetActive(false);
        }
    }

    void Respawn() 
    {
        Description();
        if (CompareTag("Consumable"))
        {
            spawnStuff.Respawn(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Description()
    {
        text.text = name + " has been added to your inventory!";
        Invoke("Undescribe", 1);
    }

    void Undescribe()
    {
        menu.SetActive(false);
    }
}
