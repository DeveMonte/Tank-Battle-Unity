using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    float minX, minZ, maxX, maxZ, z, x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateBomb();
        }

    }
    public void GenerateBomb()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        minX = rend.bounds.min.x - 20;
        maxX = rend.bounds.max.x + 20;
        minZ = rend.bounds.min.z - 20;
        maxZ = rend.bounds.max.z + 20;
        x = Random.Range(minX, maxX);
        z = Random.Range(minZ, maxZ);
        gameObject.transform.localScale = new Vector3(3, 1, 3);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.AddComponent<BoxCollider>();
        gameObject.transform.position = new Vector3(x, 20, z);
        gameObject.AddComponent<Rigidbody>();
    }

}
