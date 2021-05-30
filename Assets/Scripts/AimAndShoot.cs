using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    Camera[] cams;
    public GameObject sight;
    public GameObject explosion;
    public Camera activeCam;
    public Camera aimCamera;
    private RectTransform sightTransform;
    private Vector3 point;
    private Vector2 sightSize = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        sightTransform = sight.GetComponent<RectTransform>();
        sightTransform.sizeDelta = sightSize;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Aim();
        }
        if (Input.GetMouseButtonUp(1))
        {
            DefaultCamera();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }


    public void Aim()
    {
        sightTransform.sizeDelta = new Vector2(5, 5);
        point = new Vector3(aimCamera.pixelWidth / 2, aimCamera.pixelHeight / 2, 0);
        sight.transform.position = point;
        activeCam.enabled = false;
        aimCamera.enabled = true;
    }

    public void DefaultCamera()
    {
        activeCam.enabled = true;
        aimCamera.enabled = false;
        sightTransform.sizeDelta = new Vector2(0, 0);
    }

    public void Shoot()
    {
        //Vector3 point = new Vector3(aimCamera.pixelWidth / 2, aimCamera.pixelHeight / 2, 0);
        Ray ray = aimCamera.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Vector3 pos = hit.point;
            sphere.transform.position = pos;
            Instantiate(explosion, pos, sphere.transform.rotation);
        }
    }
}
