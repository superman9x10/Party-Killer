using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;
    [SerializeField] GameObject arrow3;
    public Transform m_firePoint;

    [SerializeField] float reFillRate = 1;

    public bool isFreeShootCollected;
    int bulletReady;
    float nextTimeToReFill;

    // Start is called before the first frame update
    void Start()
    {
        
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
        
        bulletReady = 0;
        nextTimeToReFill = 1 / reFillRate + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFreeShootCollected)
        {
            bulletReady = 3;
            arrow1.SetActive(true);
            arrow2.SetActive(true);
            arrow3.SetActive(true);
        }
        else if (bulletReady < 3 && Time.time > nextTimeToReFill)
        {
            bulletReady++;
            if (bulletReady == 1)
            {
                arrow1.SetActive(true);
            }
            else if (bulletReady == 2)
            {
                arrow2.SetActive(true);
            } else if (bulletReady == 3)
            {
                arrow3.SetActive(true);
            }
            
            nextTimeToReFill = 1 / reFillRate + Time.time;
        }
    }

    public void shoot()
    {
        if (bulletReady <= 0)
        { 
            return;
        }


        GameObject bullet = Instantiate(bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        //GameObject bullet = ObjectPooling.sharedInstance.getPooledObject();
        //if (bullet != null)
        //{
        //    bullet.transform.position = m_firePoint.position;
        //    bullet.transform.rotation = m_firePoint.rotation;
        //    bullet.SetActive(true);
            
        //}
        bullet.GetComponent<Bullet>().FirePoint = m_firePoint;
        if (!isFreeShootCollected)
        {
            if (bulletReady == 1)
            {
                arrow1.SetActive(false);
            }
            else if (bulletReady == 2)
            {
                arrow2.SetActive(false);
            }
            else if (bulletReady == 3)
            {
                arrow3.SetActive(false);
            }

            bulletReady--;
            nextTimeToReFill = 1 / reFillRate + Time.time;
        }
        
        
    }
}
