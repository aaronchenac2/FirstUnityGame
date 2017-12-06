using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafts : MonoBehaviour
{
    GameObject player;
    GameObject sub;
    CraftingSubmenu subScript;

    public string[] requirements;
    public int[] numRequirements;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sub = GameObject.FindGameObjectWithTag("CraftingSubmenu");
        subScript = sub.GetComponent<CraftingSubmenu>();
    }

    public void TaskOnClick()
    {
        subScript.Reset();
        subScript.Setup(name, requirements, numRequirements);
        sub.SetActive(true);
    }
}
