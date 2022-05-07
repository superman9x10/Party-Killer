using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemRand : MonoBehaviour
{
    
    public List<GameObject> items;
    Vector3 max;
    Vector3 min;
    Vector3 itemSpawnPos;
    public LayerMask m_layerMask;

    float xPoint, zPoint;

    [SerializeField] int firstTimeSpawn;
    [SerializeField] int timeToNextSpawn;
    private void Start()
    {
        min = new Vector3(-27, 0, -16);
        max = new Vector3(16, 0, 20);
        StartCoroutine(randomItem());
    }

    IEnumerator randomItem()
    {
        yield return new WaitForSeconds(firstTimeSpawn);
        while (true)
        {
            if(GameManager.instance.canRandomItem)
            {
                xPoint = Random.Range(min.x, max.x);
                zPoint = Random.Range(min.z, max.z);
                itemSpawnPos = new Vector3(xPoint, 0, zPoint);
                Collider[] cubes = Physics.OverlapSphere(itemSpawnPos, 1f, m_layerMask);
                if (cubes.Length == 0)
                {
                    int item = Random.Range(0, items.Count);
                    GameObject itemSpawn = Instantiate(items[item], itemSpawnPos, Quaternion.identity);
                    Destroy(itemSpawn, 5f);
                }
            }
            
            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }

}
