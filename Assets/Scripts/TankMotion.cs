using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TankMotion : MonoBehaviour
{
    public GameObject explosion; // эффект взрыва при падении бомбы на танк
    public GameObject tower; // башня танка
    public GameObject trunc; // ствол танка
    public GameObject text; // надпись в канвасе
    public GameObject image; // темный в канвасе
    float speed = 10f; // скорость танка
    float rotateX, rotateY, angle, ang; // углы для поворат башни и ствола
    private int lifes = 5;

    // Start is called before the first frame update
    void Start()
    {
        angle = tower.transform.rotation.y;
        ang = trunc.transform.rotation.x;
        text.SetActive(false);
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes == 0) // Проверяем, остались ли жизни
        {
            speed = 0;
            text.SetActive(true); // Отображаем текст "Вы проиграли"
            image.SetActive(true); // Отображаем фон
        }

        #region Поворот башни

        rotateX = Input.GetAxis("Mouse X");
        angle += rotateX;
        rotateY = Input.GetAxis("Mouse Y");
        ang += rotateY;
        angle = Mathf.Clamp(angle, -90, 90);
        ang = Mathf.Clamp(ang, -5, 15);

        tower.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
        trunc.transform.localRotation = Quaternion.AngleAxis(ang, Vector3.left);
        #endregion

        Vector3 movement = new Vector3(0, 0, 0);
        Vector3 tankRotation = new Vector3(0, 0, 0);

        #region Ускорение
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
        #endregion

        #region Движение танка
        if (Input.GetKey(KeyCode.D))
        {
            tankRotation.y = speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            tankRotation.y = - speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement.z = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z = - speed * Time.deltaTime;
        }
        #endregion

        transform.Translate(movement, Space.Self);
        transform.Rotate(tankRotation, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere") // Если на танк упала бомба, то уменьшаем жизни
        {
            Instantiate(explosion, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
            Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            //explosion.SetActive(true);
            lifes--;
            Destroy(collision.gameObject);
        }
    }

}
