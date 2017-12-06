using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Sprite[] sprites;
    string[] spritesCatalog;

    public static int numObjects;

    GameObject inventory;

    static int numSlots = 8;
    bool visible = false;
    float time = 0;
    float lastOpen = .25f;
    public GameObject[] slots;
    String[] Catalog;
    static int historySlots = 2;
    String[,] history;


    // Use this for initialization
    void Start()
    {
        numObjects = sprites.Length;
        spritesCatalog = new string[numObjects];

        slots = new GameObject[numSlots];
        Catalog = new String[numSlots];
        history = new String[numObjects, historySlots];

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        GameObject[] slotsUnorganized = GameObject.FindGameObjectsWithTag("Slot");
        for (int j = 0; j < numSlots; j++)
        {
            int trueValue = Int32.Parse(slotsUnorganized[j].name.Substring(4));
            slots[trueValue] = slotsUnorganized[j];
        }
        for (int j = 0; j < numObjects; j++)
        {
            history[j, 0] = "Empty";
            history[j, 1] = "Empty";

            spritesCatalog[j] = sprites[j].name;
        }
        for (int j = 0; j < 99; j++)
        {
            AddItem("Rock", "CraftingMaterial");
            AddItem("Diamond", "CraftingMaterial");
            AddItem("Log", "CraftingMaterial");
            AddItem("Demon Head", "CraftingMaterial");
            AddItem("Mallet", "CraftingMaterial");
        }
    }

    // Opens/closes inventory
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetButton("Inventory") && time > lastOpen)
        {
            time = 0;
            if (!visible)
            {
                inventory.SetActive(true);
                visible = true;
            }
            else
            {
                inventory.SetActive(false);
                visible = false;
            }
        }
    }

    // Input: name and tag of item
    // Ouput: tag of item
    public String AddItem(String name, String tag)
    {
        if (name == "Pickaxe" || name == "Axe" || name == "Sword")
        {
            tag = "Tool";
        }
        if (name == "Mallet")
        {
            tag = "CraftingMaterial";
        }
        updateHistory(name, tag);
        int slotNum = GetSlot(name);
        Text[] texts = slots[slotNum].GetComponentsInChildren<Text>();
        //Debug.Log(texts[0]);
        //Debug.Log(texts[1]);
        Text description;
        Text count;
        if (texts[0].name == "SlotDescript1")
        {
            description = texts[0];
            count = texts[1];
        }
        else
        {
            description = texts[1];
            count = texts[0];
        }

        description.text = name;
        count.text = "" + (Int32.Parse(count.text) + 1);
        return GetTag(name);

    }

    // Input: number slot
    // Outpu: tag
    public String UseItem(int num)
    {
        Text[] texts = slots[num].GetComponentsInChildren<Text>();
        //Debug.Log(texts[0]);
        //Debug.Log(texts[1]);
        Text description;
        Text count;
        if (texts[0].name == "SlotDescript1")
        {
            description = texts[0];
            count = texts[1];
        }
        else
        {
            description = texts[1];
            count = texts[0];
        }
        description.text = Catalog[num];
        string oneItemer = Catalog[num]; // DEBUGGED WHEN 1 CRAFTING MATERIAL LEFT

        count.text = "" + (Int32.Parse(count.text) - 1);
        int newCount = Int32.Parse(count.text);
        if (newCount == 0)
        {
            ClearSlot(num, description);
        }
        return GetTag(oneItemer);
    }


    /* Input: name of item
     * Output: slot number
     * Actions:
     * 1. Checks if equals
     * 2. inserts in null
    */
    int GetSlot(String name)
    {
        int answer = 0;
        for (int j = 0; j < numSlots; j++)
        {
            if (Catalog[j] == name)
            {
                return j;
            }
        }
        for (int j = 0; j < numSlots; j++)
        {
            if (Catalog[j] == null)
            {
                Catalog[j] = name;
                answer = j;
                AddSlot(name, answer);
                break;
            }
        }
        return answer;
    }

    GameObject GetSlotv2(String name)
    {
        for (int j = 0; j < numSlots; j++)
        {
            if (Catalog[j] == name)
            {
                return slots[j];
            }
        }
        return null;
    }

    public int GetSlotInt(String name)
    {
        for (int j = 0; j < numSlots; j++)
        {
            Debug.Log("Catalog: " + Catalog[j]);
            if (Catalog[j] == name)
            {
                return j;
            }
        }
        return -1;
    }

    // updates the sprite on slot
    // names = tag
    void AddSlot(String name, int index)
    {
        slots[index].GetComponentInChildren<Image>().sprite = GetSprite(name);
    }

    public Sprite GetSprite(String name)
    {
        for (int j = 0; j < numObjects; j++)
        {
            if (spritesCatalog[j] == name)
            {
                return sprites[j];
            }
        }
        return null;
    }

    // Clears slot
    public void ClearSlot(int num, Text description)
    {
        slots[num].GetComponentInChildren<Image>().sprite = null;
        description.text = "Empty";
        Catalog[num] = null;
    }

    void updateHistory(String name, String tag)
    {
        for (int j = 0; j < numObjects; j++)
        {
            if (name == history[j, 0])
            {
                return;
            }
        }
        addHistory(name, tag);
    }
    void addHistory(String name, String tag)
    {
        for (int j = 0; j < numObjects; j++)
        {
            if (history[j, 0] == "Empty")
            {
                history[j, 0] = name;
                history[j, 1] = tag;
                return;
            }
        }
    }
    String GetTag(String name)
    {
        for (int j = 0; j < numObjects; j++)
        {
            if (name == history[j, 0])
            {
                return history[j, 1];
            }
        }
        return "NAMENOTFOUND";
    }

    public String getName(String name)
    {
        return GetSlotv2(name).name;
    }
    public Text getCount(String name)
    {
        GameObject slot = GetSlotv2(name);
        if (slot == null)
        {
            return null;
        }
        Text[] texts = slot.GetComponentsInChildren<Text>();
        Text count = null;
        for (int j = 0; j < texts.Length; j++)
        {
            if (texts[j].name == "Count")
            {
                count = texts[j];
            }
        }
        return count;
    }
    public Text getDescript(String name)
    {
        GameObject slot = GetSlotv2(name);
        Text[] texts = slot.GetComponentsInChildren<Text>();
        Text descript = null;
        for (int j = 0; j < texts.Length; j++)
        {
            if (texts[j].name == "SlotDescript1")
            {
                descript = texts[j];
            }
        }
        return descript;
    }
}
