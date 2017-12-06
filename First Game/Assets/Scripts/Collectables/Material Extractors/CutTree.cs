using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTree : MonoBehaviour {
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
	void Start () {
        level = (int)(GetComponent<CapsuleCollider>().height) * 3; // no * 3
        player = GameObject.FindGameObjectWithTag("Player");
        durability = level * 2; // 2
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Axe")
        {
            durability -= toolDmg;
            other.GetComponent<AudioSource>().Play();
            Debug.Log(durability);
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
                    float newY = gameObject.transform.position.y + Random.value * level;
                    float newZ = gameObject.transform.position.z + Random.value * 2;
                    Vector3 newPos = new Vector3(newX, newY, newZ);
                    GameObject work = Instantiate(items[j], newPos, new Quaternion(0, 0, 0, 0));
                    work.name = work.name.Replace("(Clone)", "").Trim();
                }
            }
        }
    }
}
