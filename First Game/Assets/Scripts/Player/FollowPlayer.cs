using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject player;
        ThirdPersonUserControl tpuc;

    float distance;
        Vector3 displacement;
        Vector3 startDisplacement;
        float initFOV;
        int zoomSpeed = 2;

        Ray toPlayer;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag(("Player"));
            tpuc = player.GetComponent<ThirdPersonUserControl>();
            distance = 10;
            startDisplacement = transform.position - player.transform.position;
            initFOV = this.GetComponent<Camera>().fieldOfView;
            toPlayer = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 + 50, 0));
        }


        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("World"))
            {
                float newY = Terrain.activeTerrain.SampleHeight(transform.position) + 2f;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastHit hit;
            toPlayer = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 + 50, 0));
            if (Physics.Raycast(toPlayer, out hit, displacement.magnitude))
            {
                if (hit.collider.tag == "Trees")
                {
                    MeshRenderer mr = hit.collider.GetComponent<MeshRenderer>();
                    if (mr != null)
                    {
                        mr.enabled = false;
                        StartCoroutine(Foo(mr, 3));
                    }

                    /*
                    SetTransparent(hit.collider.gameObject);
                    StartCoroutine(Foo(hit.collider.gameObject, 2));
                    */
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.GetComponent<Camera>().fieldOfView > 0)
            {
                this.GetComponent<Camera>().fieldOfView -= zoomSpeed;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.GetComponent<Camera>().fieldOfView < initFOV * 1.5)
            {
                this.GetComponent<Camera>().fieldOfView += zoomSpeed;
            }
            float angle = player.transform.rotation.eulerAngles.y * -1 + 90;
            displacement = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * -distance, startDisplacement.y, Mathf.Sin(Mathf.Deg2Rad * angle) * -distance);
            if (!tpuc.mounted)
            {
                transform.position = player.transform.position + displacement;
            }
            if (Terrain.activeTerrain.SampleHeight(transform.position) > transform.position.y)
            {
                float newY = Terrain.activeTerrain.SampleHeight(transform.position) + 2f;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            transform.LookAt(player.transform);
        }


        IEnumerator Foo(MeshRenderer mr, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (mr != null)
            {
                mr.enabled = true;
            }
        }


        /*
        IEnumerator Foo(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            RevertTransparent(go);
        }
        */



        private void SetTransparent(GameObject g)
        {
            for (int i = 0; i < g.GetComponent<Renderer>().materials.Length; i++)
            {
                g.GetComponent<Renderer>().materials[i].shader = Shader.Find("Transparent/Diffuse");
                g.GetComponent<Renderer>().materials[i].SetColor("_Color", new Color(1, 1, 1, 0.1f));
            }
            for (int i = 0; i < g.transform.childCount; i++)
                SetTransparent(g.transform.GetChild(i).gameObject);
        }

        private void RevertTransparent(GameObject g)
        {
            for (int i = 0; i < g.GetComponent<Renderer>().materials.Length; i++)
            {
                if (g.GetComponent<Renderer>().materials[i].name == "Optimized Bark Material (Instance)")
                    g.GetComponent<Renderer>().materials[i].shader =
                                   Shader.Find("Nature/Tree Creator Bark");
                else if (g.GetComponent<Renderer>().materials[i].name ==
                                   "Optimized Leaf Material (Instance)")
                    g.GetComponent<Renderer>().materials[i].shader =
                                   Shader.Find("Nature/Tree Creator Leaves Fast");
                else
                    g.GetComponent<Renderer>().materials[i].shader = Shader.Find("Default/Diffuse");
                g.GetComponent<Renderer>().materials[i].SetColor("_Color", new Color(1, 1, 1));
            }
            for (int i = 0; i < g.transform.childCount; i++)
                SetTransparent(g.transform.GetChild(i).gameObject);
        }

    }
}
