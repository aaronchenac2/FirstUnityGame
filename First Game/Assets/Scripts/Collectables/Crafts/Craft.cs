using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Craft : MonoBehaviour {
    GameObject player;
    GameObject menu;
    Inventory inventory;
    CraftingSubmenu sub;
    public GameObject[] reqs;
    References r;


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        menu = GameObject.FindGameObjectWithTag("Menu");
        inventory = player.GetComponent<Inventory>();
        sub = GetComponentInParent<CraftingSubmenu>();
        reqs = sub.reqs;
        r = player.GetComponent<References>();
	}
	
    public void TaskOnClick()
    {
        bool transaction = true;
        String errorMessage = "";
        for(int j = 0; j < reqs.Length; j++)
        {
            Text[] texts = reqs[j].GetComponentsInChildren<Text>();
            Text descript = null;
            Text count = null;
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].name == "RequireDescript")
                {
                    descript = texts[i];
                }
                else if (texts[i].name == "Count")
                {
                    count = texts[i];
                }
            }

            if (descript.text != "Required Material")
            {
                Text source = inventory.getCount(descript.text);

                if(source == null)
                {
                    errorMessage += "You do not have any " + descript.text + "s\n";
                    transaction = false;
                }
                else
                {
                    int has = Int32.Parse(source.text);
                    int needs = Int32.Parse(count.text);
                    if (has < needs)
                    {
                        if (needs - has == 1)
                        {
                            errorMessage += "You need " + (needs - has) + " more " + descript.text + "\n";
                        }
                        else
                        {
                            errorMessage += "You need " + (needs - has) + " more " + descript.text + "s\n";
                        }
                        transaction = false;
                    }
                }
            }
        }
        if (transaction)
        {
            inventory.AddItem(r.craftingSubmenu.GetComponentInChildren<Text>().text, "i<3vhstill");
            menu.GetComponentInChildren<Text>().text = "You obtained " + r.craftingSubmenu.GetComponentInChildren<Text>().text + "!!";
            menu.SetActive(true);
            Invoke("Undescribe", 3);

            // reduce counts
            for (int j = 0; j < reqs.Length; j++)
            {
                Text[] texts = reqs[j].GetComponentsInChildren<Text>();
                Text descript = null;
                Text count = null;
                for (int i = 0; i < texts.Length; i++)
                {
                    if (texts[i].name == "RequireDescript")
                    {
                        descript = texts[i];
                    }
                    else if (texts[i].name == "Count")
                    {
                        count = texts[i];
                    }
                }

                if (descript.text != "Required Material")
                {
                    Text source = inventory.getCount(descript.text);
                    int has = Int32.Parse(source.text);
                    int needs = Int32.Parse(count.text);

                    if (has - needs > 0)
                    {
                        source.text = "" + (has - needs);
                    }
                    else
                    {
                        source.text = "" + 0;
                        Debug.Log("Slot number: " + inventory.GetSlotInt(descript.text));
                        Debug.Log(descript);
                        inventory.ClearSlot(inventory.GetSlotInt(descript.text), inventory.getDescript(descript.text));
                    }
                }
            }
        }
        else
        {
            menu.GetComponentInChildren<Text>().text = errorMessage;
            menu.SetActive(true);
            Invoke("Undescribe", 3);
        }
    }

    void Undescribe()
    {
        menu.SetActive(false);
    }
}
