using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<ObjectPoolItem> items = new List<ObjectPoolItem>();
    public List<GameObject> pooledItems = new List<GameObject>();

    public static ObjectPool instance;

    int itemIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {        
        GeneratePooledItems();
    }

    public GameObject Get(string tag)
    {
      
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (pooledItems[i].tag == tag && !pooledItems[i].activeInHierarchy)
            {
                return pooledItems[i];
            }
        }

        foreach (ObjectPoolItem item in items)
        {
            if (item.prefab.tag == tag && item.expandable)
            {
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                pooledItems.Add(go);

                return go;
            }
        }
                
        return null;
    }

    void GeneratePooledItems()
    {
        foreach (ObjectPoolItem item in items)
        {
            for (int i = 0; i < item.Ammount; i++)
            {
                GameObject g = Instantiate(item.prefab);
                g.SetActive(false);
                pooledItems.Add(g);
            }
        }          
    }
}
