using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public DynamicJoystick joystick;
    public GameObject bulletPrefab; // Thêm biến cho Prefab của viên đạn
    public Transform pointShoot; // Thêm biến cho điểm xuất phát của viên đạn
    public float timeBetween;
    public float bulletSpeed = 50f;
    private void OnEnable()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }

    // Start is called before the first frame update
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
        //InvokeRepeating("Shoot", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu joystick đang di chuyển và nút bắn được nhấn
        if (joystick && joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Tính toán hướng bắn dựa trên joystick
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        // Tạo viên đạn tại firePoint và đặt hướng của nó
        GameObject bullet = Instantiate(bulletPrefab, pointShoot.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        // Xoay viên đạn theo hướng bắn
        //bullet.transform.forward = direction;
    }
}
