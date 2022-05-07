using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform FirePoint { set; get; }
    [SerializeField] float m_speed = 30f;

    private Rigidbody rb;
    public GameObject m_explosion;

    private AudioSource m_audio;

    private bool hasHitWall;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(FirePoint.forward * m_speed * 2f);
        m_audio = GetComponent<AudioSource>();
        m_audio.Play();
    }

    private void Update()
    {

    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerA" || collision.gameObject.tag == "PlayerB")
        {
            this.Collision();
        }
        else
        {
            if (!hasHitWall)
            {
                hasHitWall = true;
                this.Collision();
            }   
        }
    }
        

    private void Collision()
    {
            GameObject explosion = Instantiate(m_explosion, this.transform.position, this.transform.rotation);
            Destroy(explosion, 3f);
            //gameObject.SetActive(false);
            Destroy(gameObject);
            CameraShaker.Instance.ShakeOnce(2f, 5f, 0.3f, 0.3f);
    }
}
