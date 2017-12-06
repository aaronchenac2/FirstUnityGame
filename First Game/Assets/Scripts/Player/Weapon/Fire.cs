using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;

    private float timer = 0;
    public float timeBetweenBullets = .5f;

    Animator anim;
    float bulletSpeed = 2500;
    GameObject firePointObject;
    Vector3 firePoint;
    float width;
    float height;
    Vector2 center;
    float cX;
    float cY;

    Rigidbody rbody;


    void Start()
    {
        anim = GetComponent<Animator>();
        firePointObject = GameObject.FindGameObjectWithTag("WandChild");
        firePoint = firePointObject.transform.position;
        width = Screen.width;
        height = Screen.height;
        center = new Vector2(width / 2, height / 2);
        cX = center.x;
        cY = center.y;

        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire2") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            anim.SetTrigger("BasicAttack");
            //Debug.Log(anim.runtimeAnimatorController.animationClips.Length);
            timer = 0;
            Invoke("Fired", .5f);
        }
    }

    void Fired()
    {
        firePoint = firePointObject.transform.position;
        GameObject b = Instantiate(bullet, firePoint, transform.rotation) as GameObject;
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.x * bulletSpeed, -1f, transform.forward.z * bulletSpeed);
        Destroy(b, 10);
    }
}
