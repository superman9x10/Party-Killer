using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling sharedInstance;
    public List<GameObject> bullets;
    public GameObject bullet;
    public int amountToPool;
    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        bullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bullet);
            tmp.SetActive(false);
            bullets.Add(tmp);
        }
    }

    public GameObject getPooledObject ()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }
        return null;
    }

}
