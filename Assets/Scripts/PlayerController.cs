using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;
    Weapon weapon;

    public GameObject playerAExplo;
    public GameObject playerBExplo;

    float h1, v1, h2, v2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        h1 = Input.GetAxis("Horizontal1");
        v1 = Input.GetAxis("Vertical1");

        h2 = Input.GetAxis("Horizontal2");
        v2 = Input.GetAxis("Vertical2");
    }

    private void FixedUpdate()
    {
        controller();
    }
    void controller()
    {
        if(this.gameObject.CompareTag("PlayerA"))
        {
            
            Vector3 playerAMoveDir = new Vector3(h1, 0, v1).normalized;
            this.rb.velocity = playerAMoveDir * speed;
            
            if (h1 != 0 || v1 != 0)
            {
                this.rb.rotation = Quaternion.LookRotation(playerAMoveDir);
            }
        }
       if(this.gameObject.CompareTag("PlayerB"))
        {
            

            Vector3 playerBMoveDir = new Vector3(h2, 0, v2).normalized;

            this.rb.velocity = playerBMoveDir * speed;

            if (h2 != 0 || v2 != 0)
            {
                this.rb.rotation = Quaternion.LookRotation(playerBMoveDir);
            }
        }
        
    }

    void shoot()
    {
        if (this.gameObject.CompareTag("PlayerA"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.weapon.shoot();
            }
        }
        if (this.gameObject.CompareTag("PlayerB"))
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                this.weapon.shoot();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            
            if (this.gameObject.CompareTag("PlayerA"))
            {
                GameObject pAExlo = Instantiate(playerAExplo, this.transform.position, this.transform.rotation);
                Destroy(pAExlo, 3f);
            }
            if (this.gameObject.CompareTag("PlayerB"))
            {
                GameObject pBExplo = Instantiate(playerBExplo, this.transform.position, this.transform.rotation);
                Destroy(pBExplo, 3f);
            }

            Destroy(this.gameObject);

            if (!GameManager.instance.hasPlayerDead)
            {
                GameManager.instance.hasPlayerDead = true;
                GameManager.instance.isEndGame = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "FreeShootItem(Clone)")
        {
            StartCoroutine(freeShootCollected());
            Destroy(other.gameObject);
        }
        if(other.name == "SpeedItem(Clone)")
        {
            StartCoroutine(speedItemCollected());
            Destroy(other.gameObject);
        }
        Debug.Log(other.gameObject.name);
    }

    IEnumerator freeShootCollected()
    {
        this.weapon.isFreeShootCollected = true;
        yield return new WaitForSeconds(3f);
        this.weapon.isFreeShootCollected = false;
    }

    IEnumerator speedItemCollected()
    {
        if(this.speed < 20)
        {
            this.speed += 5;
        }
        yield return new WaitForSeconds(5f);
        if(this.speed > 15)
        {
         this.speed -= 5;
        }
    }
}
