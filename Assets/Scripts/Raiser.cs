using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raiser : MonoBehaviour
{
    public float m_randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        m_randomOffset = Random.Range(0f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        float perlin =Mathf.Abs(Mathf.PerlinNoise(transform.position.x / m_randomOffset + Time.time, transform.position.z / m_randomOffset + Time.time));
        transform.localScale = new Vector3(2f, perlin * 4f, 2f);
    }
}
