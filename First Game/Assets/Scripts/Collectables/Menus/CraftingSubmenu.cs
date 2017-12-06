using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSubmenu : MonoBehaviour {
    public GameObject[] reqs;

    public Text title;
    GameObject player;
    Inventory inventory;


    // Use this for initialization
    void Start () {
        title = GetComponentInChildren<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
	}
	
	public void Setup(string title, string[] requirements, int[] numRequirements)
    {
        this.title.text = title;
        for (int j = 0; j < requirements.Length; j++)
        {
            Image work = reqs[j].GetComponentInChildren<Image>();
            work.sprite = inventory.GetSprite(requirements[j]);
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
            descript.text = requirements[j];
            count.text = "" + numRequirements[j];
            reqs[j].SetActive(true);
        }
    }

    public void Reset()
    {
        for (int j = 0; j < reqs.Length; j++)
        {
            Text[] texts = reqs[j].GetComponentsInChildren<Text>();
            Text descript = null;
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].name == "RequireDescript")
                {
                    descript = texts[i];
                }
            }
            reqs[j].SetActive(false);
            descript.text = "Required Material";
        }
    }
}
