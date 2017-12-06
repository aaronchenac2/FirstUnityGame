using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotClicked : MonoBehaviour {
    References r;
    GameObject player;
    Inventory inventory;
    Vitals vitals;

    int hpValue = 10;
    int appleValue = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
        inventory = player.GetComponent<Inventory>();
        vitals = player.GetComponent<Vitals>();
    }

    
    public void TaskOnClick()
    {
        if (GetComponentInChildren<Image>().sprite != null)
        {
            int slotNumber = Int32.Parse(gameObject.name.Substring(4));
            String itemName = gameObject.GetComponentInChildren<Text>().text; // POSSIBLE FUTURE PROBLEM
            string itemTag;
            if ((itemTag = inventory.UseItem(slotNumber)) == "CraftingMaterial")
            {
                DenyAction(itemName);
            }
            Debug.Log(itemTag);
            if (itemTag == "Tool")
            {
                r.toolsManagerScript.ChangeTool(itemName);
            }
            Action(itemName);
        }
    }

    void Action(String itemName)
    {
        if (itemName == "Apple")
        {
            vitals.addHunger(appleValue);
        }
        else if (itemName == "HP Pack")
        {
            if (!vitals.addHP(hpValue))
            {
                DenyAction(itemName);
            }
        }
        else if (itemName == "Boat")
        {
            Instantiate(r.spawnStuff.GetGO("BoatBottom"), player.transform.position + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    void DenyAction(String itemName)
    {
        inventory.AddItem(itemName, "istill<3vh");
    }
}
