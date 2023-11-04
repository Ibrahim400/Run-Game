using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enviroment
{
    private static Enviroment singlton;
    private List<GameObject> items = new List<GameObject>();
    private List<GameObject> removableTiles = new List<GameObject>();


    public List<GameObject> Items { get { return items; } }
    public List<GameObject> RemovableTiles { get { return removableTiles; } }

    public static Enviroment Instance
    {
        get
        {
            if (singlton == null)
            {
                singlton = new Enviroment();
                Instance.RemovableTiles.AddRange(GameObject.FindGameObjectsWithTag("Removable"));
            }
            return singlton;
        }
    }

    public void AddItem(GameObject o)
    {
        items.Add(o);
    }

    public void RemoveTiles()
    {
        for (int i = 0; i < removableTiles.Count; i++)
        {
            Rigidbody rig = removableTiles[i].GetComponent<Rigidbody>();
            rig.useGravity = true;
        }
    }
}
