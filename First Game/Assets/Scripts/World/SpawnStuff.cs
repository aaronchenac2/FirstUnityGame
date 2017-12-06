using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuff : MonoBehaviour {
    public GameObject[] objects;
    public int[] frequency;
    ArrayList names;

    int highestPoint;

    float x;
    float y;
    float z;

    void Start ()
    {
        Vector3 scale = GetComponent<Terrain>().terrainData.size;
        x = scale.x;
        y = scale.y;
        z = scale.z;
        highestPoint = (int) y;
        names = new ArrayList();
        Spawn();        
    }

    void Spawn()
    {
        for (int j = 0; j < objects.Length; j++)
        {
            names.Add(objects[j].name);
            int num = Convert(frequency[j]);
            for (int i = 0; i < num; i++)
            {
                Vector3 pos = new Vector3(Random.Range(0, x), 0, Random.Range(0, z));
                pos.y = Terrain.activeTerrain.SampleHeight(pos);
                GameObject work = Instantiate(objects[j], pos, new Quaternion(0, 0, 0, 0)) as GameObject;
                work.name = work.name.Replace("(Clone)", "").Trim();
            }
        }
    }

    int Convert(int frequency)
    {
        return (int) (x * z / 10000 * frequency);
    }

    public void Respawn(GameObject go)
    {
        Vector3 scale = GetComponent<Terrain>().terrainData.size;
        x = scale.x;
        y = scale.y;
        z = scale.z;
        Vector3 pos = new Vector3(Random.Range(0, x), highestPoint, Random.Range(0, z));
        go.transform.rotation = new Quaternion(0, 0, 0, 0);
        go.SetActive(true);
        go.gameObject.transform.position = pos;
    }

    public GameObject GetGO(string name)
    {
        for (int j = 0; j < names.Count; j++)
        {
            if ((string) names[j] == name)
            {
                return objects[j];
            }
        }
        return null;
    }
}
