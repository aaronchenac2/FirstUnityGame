using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManagerScript : MonoBehaviour {
    GameObject player;
    References r;
    GameObject[] children;
    string[] childrenNames;
    bool[] childrenStatus;
    bool[] childrenUnlocked;
    int numChildren;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r = player.GetComponent<References>();
        Animator[] childrenTransforms = transform.GetComponentsInChildren<Animator>();
        numChildren = childrenTransforms.Length;
        children = new GameObject[childrenTransforms.Length];
        childrenNames = new string[childrenTransforms.Length];
        childrenStatus = new bool[childrenTransforms.Length];
        childrenUnlocked = new bool[childrenTransforms.Length];
        for (int j = 0; j < childrenTransforms.Length; j++)
        {
            children[j] = childrenTransforms[j].gameObject;
            childrenNames[j] = childrenTransforms[j].name;
            childrenStatus[j] = false;
        }
        // Deactivates children
        for (int j = 0; j < childrenTransforms.Length; j++)
        {
            children[j].SetActive(false);
        }
    }

    public bool HasChildren()
    {
        for (int j = 0; j < numChildren; j++)
        {
            if (childrenStatus[j] == true)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeTool(string toolName)
    {
        for (int j = 0; j < numChildren; j++)
        {
            if (childrenNames[j] == toolName)
            {
                children[j].SetActive(true);
                childrenStatus[j] = true;
                childrenUnlocked[j] = true;
            }
            else
            {
                children[j].SetActive(false);
                childrenStatus[j] = false;
            }
        }
    }

    public void NextTool()
    {
        bool noneUnlocked = true;
        for  (int j = 0; j < numChildren; j++)
        {
            if (childrenUnlocked[j])
            {
                noneUnlocked = false;
            }
        }
        if (noneUnlocked)
        {
            return;
        }

        int activeTool = GetActiveTool();
        if (activeTool == -1)
        {
            activeTool++;
        }

        children[activeTool].SetActive(false);
        childrenStatus[activeTool] = false;

        int startingTool = activeTool + 1;
        if (startingTool == numChildren)
        {
            startingTool = 0;
        }
        for (int j = startingTool; j < numChildren; j++)
        {
            if (childrenUnlocked[j] && (childrenStatus[j] == false))
            {
                children[j].SetActive(true);
                childrenStatus[j] = true;
                return;
            }
        }
        for (int j = 0; j < activeTool; j++)
        {
            if (childrenUnlocked[j] && (childrenStatus[j] == false))
            {
                children[j].SetActive(true);
                childrenStatus[j] = true;
                return;
            }
        }

        children[activeTool].SetActive(true);
        childrenStatus[activeTool] = true;
    }

    int GetActiveTool()
    {
        for (int j = 0; j < numChildren; j++)
        {
            if (childrenStatus[j])
            {
                return j;
            }
        }
        return -1;
    }

}
