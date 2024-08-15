using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public DynamicJoystick joystick;
    public string bulletTag;  // Tag cho pool của viên đạn
    public Transform pointShoot;
    public Transform pointShoot1;
    public Transform pointShoot2;
    public float timeBetween;
    public float bulletSpeed = 50f;

    private void OnEnable()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }

    void Start()
    {
        if (!joystick)
        {
            print("Null Joystick");
        }
        else
        {
            print("co Joystick");
        }
        InvokeRepeating(nameof(Shoot), 0, 0.5f);
    }


    void Shoot()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.sqrMagnitude < 0.01f)
        {
            // Nếu joystick không được sử dụng, trả về
            return;
        }

        direction = direction.normalized;

        GameObject bullet = ObjectPooling.Instance.GetPooledObject("bullet");
        if (bullet != null)
        {
            // Đặt vị trí và hướng cho đạn theo pointShoot
            bullet.transform.position = pointShoot.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // Đặt vận tốc hiện tại của đạn bằng 0
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse); // Thêm lực để đẩy đạn đi theo hướng joystick
        }

        //GameObject bullet1 = ObjectPooling.Instance.GetPooledObject("bullet");
        //if (bullet1 != null)
        //{
        //    bullet1.transform.position = pointShoot1.position;
        //    bullet1.transform.rotation = Quaternion.identity;
        //    bullet1.SetActive(true);

        //    Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        //}

        //GameObject bullet2 = ObjectPooling.Instance.GetPooledObject("bullet");
        //if (bullet2 != null)
        //{
        //    bullet2.transform.position = pointShoot2.position;
        //    bullet2.transform.rotation = Quaternion.identity;
        //    bullet2.SetActive(true);

        //    Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        //}
    }
}
