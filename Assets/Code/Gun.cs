using DG.Tweening;
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
    public float offset;
    private void OnEnable()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }

    public PickGun pickgun;
    public enum PickGun
    {
        gun1,
        gun2,
        gun3
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

    private void Update()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        this.transform.rotation = Quaternion.Euler(0, 0, angle);

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
        if (pickgun == PickGun.gun1)
        {

            GameObject bullet = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet != null)
            {
                bullet.transform.position = pointShoot.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero; 
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse); 
            }
        }
        if (pickgun == PickGun.gun2)
        {
            GameObject bullet = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet != null)
            {
                bullet.transform.position = pointShoot1.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            GameObject bullet1 = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet1 != null)
            {
                bullet1.transform.position = pointShoot2.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet1.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet1.SetActive(true);

                Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
        }
        if(pickgun == PickGun.gun3)
        {
            GameObject bullet = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet != null)
            {
                bullet.transform.position = pointShoot.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            GameObject bullet1 = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet1 != null)
            {
                bullet1.transform.position = pointShoot1.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet1.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet1.SetActive(true);

                Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            GameObject bullet2 = ObjectPooling.Instance.GetPooledObject("bullet");
            if (bullet2 != null)
            {
                bullet2.transform.position = pointShoot2.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                bullet2.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet2.SetActive(true);

                Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
