using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
    float time = 0;
    float tossRate = 1;

    public Slider hpSlider;
    public Slider hungerSlider;

    public float hp;
    public int maxHP;
    public int hpRate;

    public float hunger;
    public int maxHunger;
    public int hungerRate;

    private bool hacks = false;

    public GameObject menu;
    public GameObject inventory;

    GameObject death;

    References r;


    // Use this for initialization
    void Start()
    {
        r = GetComponent<References>();
        menu = GameObject.FindGameObjectWithTag("Menu");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        death = GameObject.FindGameObjectWithTag("Death");

        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
        hp = maxHP;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
        hunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetButton("Cancel"))
        {
            r.craftingMenu.SetActive(false);
            r.craftingSubmenu.SetActive(false);
            r.inventory.SetActive(false);
        }
        if (Input.GetButton("Toss") && time > tossRate)
        {
            time = 0;
            if (Input.GetButton("Secondary" ))
            {
                r.toolsManagerScript.NextTool();
            }
            else
            {
                r.toolsManagerScript.ChangeTool("Toss");
            }
        }

        // HP mechanics
        if (hunger <= 0)
        {
            if (hp > 0 && hp <= maxHP)
            {
                hp -= Time.deltaTime * hpRate;
            }
            else if (hp > maxHP)
            {
                hp = maxHP;
            }
            else
            {
                hp = 0;
            }
        }

        // hunger mechanics
        if (hunger > 0 && hunger <= maxHunger)
        {
            hunger -= Time.deltaTime * hungerRate;
            UpdateVitals();
        }
        else if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
        else
        {
            hunger = 0;
        }
        UpdateVitals();
    }

    public void addHunger(int num)
    {
        hunger += num;
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    public void subtractHunger(int num)
    {
        hunger -= num; 
        if ( hunger < 0)
        {
            hunger = 0;
        }
    }

    public bool addHP(int num)
    {
        if (hp == maxHP)
        {
            return false;
        }
        hp += num;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        return true;
    }

    public void subtractHP(int num)
    {
        hp -= num;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    void UpdateVitals()
    {
        hpSlider.value = hp;
        hungerSlider.value = hunger;
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        death.SetActive(true);
    }
}
