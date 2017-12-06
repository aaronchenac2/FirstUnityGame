using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour {
    GameObject player;
    public GameObject[] items;
    public float[] rarity;

    int level;
    int durability;

    public int Durability
    {
        get
        {
            return durability;
        }
        set
        {
            durability = value;
        }
    }
    int toolDmg = 20; // 2


    // Use this for initialization
    void Start()
    {
        level = (int)(GetComponent<BoxCollider>().size.magnitude) / 3; 
        player = GameObject.FindGameObjectWithTag("Player");
        durability = level * 2; // 2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Pickaxe")
        {
            durability -= toolDmg;
            other.GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (durability <= 0)
        {
            gameObject.SetActive(false);
            dropItems();
        }
    }

    void dropItems()
    {
        for (int j = 0; j < items.Length; j++)
        {
            for (int i = 0; i < level; i++)
            {
                if (Random.value <= rarity[j])
                {
                    float newX = gameObject.transform.position.x + Random.value * 2;
                    float newZ = gameObject.transform.position.z + Random.value * 2;
                    float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                    Vector3 newPos = new Vector3(newX, newY, newZ);
                    GameObject work = Instantiate(items[j], newPos, new Quaternion(0, 0, 0, 0));
                    work.name = work.name.Replace("(Clone)", "").Trim();
                }
            }
        }
    }
}
