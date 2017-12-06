using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Float : MonoBehaviour
    {
        public bool mounted = false;
        GameObject player;
        GameObject boat;
        Transform front;
        Transform back;
        Transform seatParent;
        Transform seat;
        ThirdPersonUserControl tpuc;
        float time = 0;
        float lastUse = .25f;

        float boatSpeed = 20;
        float turnSpeed = 100;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            boat = GameObject.FindGameObjectWithTag("Boat");
            front = transform.Find("BoatFront");
            back = transform.Find("BoatBack");
            tpuc = player.GetComponent<ThirdPersonUserControl>();
            seatParent = transform.Find("boat");
            seat = seatParent.gameObject.transform.Find("BoatSeat");
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                float y = other.transform.position.y;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
            if (other.CompareTag("World"))
            {
                transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);

                float y1 = Terrain.activeTerrain.SampleHeight(transform.position);
                float y2 = Terrain.activeTerrain.SampleHeight(back.position);
                float y = y1 - y2;
                float newY = 4 * y / 6.5f;
                float angle = Mathf.Atan(y / 4) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, angle);
            }
            if (other.CompareTag("Player"))
            {
                if (Input.GetButton("Interact") && time > lastUse)
                {
                    time = 0;
                    mounted = !mounted;
                    tpuc.mounted = mounted;
                    if (!mounted)
                    {
                        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                    }
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                Destroy(gameObject);
                mounted = false;
                tpuc.mounted = false;
                player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
            }
        }

        private void LateUpdate()
        {
            time += Time.deltaTime;
            if (mounted)
            {
                tpuc.tpc.m_Animator.SetBool("OnGround", false);
                Vector3 newPos = seat.position - new Vector3(-.25f, .75f, 0);
                player.transform.position = newPos;
                Vector3 newRotation = boat.transform.eulerAngles;
                player.transform.eulerAngles = new Vector3(-newRotation.z, newRotation.y, newRotation.x) + new Vector3(0, 90, 0);
                MoveControls();
            }
        }

        void MoveControls()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(v * boatSpeed * Time.deltaTime, 0, h * -boatSpeed * Time.deltaTime));

            transform.Rotate(new Vector3(0, h * turnSpeed * Time.deltaTime, 0));
            if (Input.GetButton("TurnAround"))
            {
                transform.Translate(new Vector3(-boatSpeed * Time.deltaTime, 0, 0));
            }
        }
    }
}

