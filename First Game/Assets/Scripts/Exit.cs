using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    public GameObject parent;

    public void TaskOnClick()
    {
        parent.SetActive(false);
    }
}
