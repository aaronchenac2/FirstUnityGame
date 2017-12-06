using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CraftsTags : MonoBehaviour
{
    GameObject player;
    References r;
    GameObject[] craftCategories;
    Vector3[] pos;
    GameObject[] allItems;
    int numItems = 0;

    public bool multiPage = false;
    public int pageNum = 0;
    int lastPage = 0;

    Dropdown dd;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
        craftCategories = GameObject.FindGameObjectsWithTag("CraftingTag");
        dd = gameObject.GetComponent<Dropdown>();

        // Updates dropdown text
        for (int j = 0; j < craftCategories.Length; j++)
        {
            dd.options[j + 1].text = craftCategories[j].name;
        }

        // Extracts slot positions
        GameObject[] positions = GameObject.FindGameObjectsWithTag("CraftingSlot");
        pos = new Vector3[positions.Length];
        for (int j = 0; j < positions.Length; j++)
        {
            int index = Int32.Parse(positions[j].name.Substring(3));
            pos[index] = positions[j].GetComponent<RectTransform>().position;
        }

        // Gets total number of items
        for (int j = 0; j < craftCategories.Length; j++)
        {
            GameObject[] items = craftCategories[j].GetComponent<CraftArray>().items;
            numItems += items.Length;
        }

        // Inputs the extracted items into the defined array
        int indexItems = 0;
        allItems = new GameObject[numItems];
        for (int j = 0; j < craftCategories.Length; j++)
        {
            GameObject[] items = craftCategories[j].GetComponent<CraftArray>().items;
            for (int i = 0; i < items.Length; i++)
            {
                allItems[indexItems] = items[i];
                indexItems++;
            }
        }

        ClearMenu();
        SetUpMenu(-1);
    }

    public void OnValueChange()
    {
        ClearMenu();
        SetUpMenu(dd.value - 1);
    }

    void SetUpMenu(int index)
    {
        GameObject[] items;
        if (index < 0)
        {
            items = allItems;
        }
        else
        {
            items = craftCategories[index].GetComponent<CraftArray>().items;
        }
        if (items.Length / 5 >= 1)
        {
            multiPage = true;
            lastPage = items.Length / 5;
            Debug.Log(lastPage);

        }
        for (int j = 0; j < items.Length && j < 5; j++)
        {
            items[j].GetComponent<RectTransform>().position = pos[j];
        }
    }

    public void NextPage()
    {
        ClearMenu();
        if (pageNum < lastPage)
        {
            pageNum++;
        }
        GameObject[] items;
        if (dd.value - 1 < 0)
        {
            items = allItems;
        }
        else
        {
            items = craftCategories[dd.value - 1].GetComponent<CraftArray>().items;
        }

        for (int j = pageNum * 5; j < items.Length; j++)
        {
            Debug.Log("Index: " + pageNum * 5);
            Debug.Log("Item Length: " + items.Length);
            items[j].GetComponent<RectTransform>().position = pos[j % 5];
        }
    }

    public void LastPage()
    {
        ClearMenu();
        if (pageNum > 0)
        {
            pageNum--;
        }
        GameObject[] items;
        if (dd.value - 1 < 0)
        {
            items = allItems;
        }
        else
        {
            items = craftCategories[dd.value - 1].GetComponent<CraftArray>().items;
        }

        int initj = pageNum * 5;
        for (int j = pageNum * 5; j < items.Length && j < initj + 5; j++)
        {
            Debug.Log("Index: " + pageNum * 5);
            Debug.Log("Item Length: " + items.Length);
            items[j].GetComponent<RectTransform>().position = pos[j % 5];
        }
    }

    void ClearMenu()
    {
        for (int j = 0; j < craftCategories.Length; j++)
        {
            GameObject[] items = craftCategories[j].GetComponent<CraftArray>().items;
            for (int i = 0; i < items.Length; i++)
            {
                items[i].GetComponent<RectTransform>().position = new Vector3(7000, 7000, 7000);
            }
        }
    }
}


