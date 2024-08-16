using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public Slider slider;
    float time = 2f;
    float maxTime = 0f;
    public GameObject play;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        maxTime = time;
        play.SetActive(false);
        Invoke(nameof(Show),2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            float full = time / maxTime;  // Tính giá trị của thanh trượt
            slider.value = full;
        }
        else
        {
            slider.value = 0f;
        }
    }
    void Show()
    {
        play.SetActive(true);
        slider.gameObject.SetActive(false);
    }
}
