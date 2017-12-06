using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour {
    public GameObject player;
    public GameObject menu;
    public GameObject inventory;
    public GameObject death;
    public GameObject craftingMenu;
    public GameObject craftingSubmenu;
    public GameObject world;
    public GameObject toolsManager;

    // Scripts:
    public SpawnStuff spawnStuff;
    public Vitals vitals;
    public ToolsManagerScript toolsManagerScript;
    public Inventory inventoryScript;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        menu = GameObject.FindGameObjectWithTag("Menu");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        death = GameObject.FindGameObjectWithTag("Death");
        craftingMenu = GameObject.FindGameObjectWithTag("CraftingMenu");
        craftingSubmenu = GameObject.FindGameObjectWithTag("CraftingSubmenu");
        world = GameObject.FindGameObjectWithTag("World");
        toolsManager = GameObject.FindGameObjectWithTag("ToolsManager");


        //Scripts:
        spawnStuff = world.GetComponent<SpawnStuff>();
        vitals = player.GetComponent<Vitals>();
        toolsManagerScript = toolsManager.GetComponent<ToolsManagerScript>();
        inventoryScript = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update () {
		if (Time.time < .1)
        {
            menu.SetActive(false);
            inventory.SetActive(false);
            death.SetActive(false);
            craftingMenu.SetActive(false);
            craftingSubmenu.SetActive(false);
        }
	}
}
