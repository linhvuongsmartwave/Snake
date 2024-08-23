﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    [HideInInspector]
    public List<Transform> listContainPartBody;

    public GameObject Body;
    public GameObject PartBody1;
    public GameObject PartBody2;
    public GameObject head;
    public Transform ContainPartBody;
    public int numBody;

    private int numPartBody;
    private int numContainer;

    private int currenPartBody;
    private int currentBody;

    public Transform pointSpawn;
    public float size;
    public PointData pointData;

    public float timeSpaw;

    public List<Body> listBody = new List<Body>();
    public List<EnemyData> level;
    public GameObject[] playerPrefabs;
    int characterIndex;
    private int numberLevel;
    private int numberSelect;
    public SceneFader sceneFader;

    public bool canMove = true;
    public GameObject effectIce;
    public GameObject effectFire;

    public bool hasWin = false;
    public UiPanelDotween win;
    public GameObject noMoney;
    private Button fireBtn;
    private Button iceBtn;


    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        numberSelect = PlayerPrefs.GetInt("SelectedLevel", 0);
        numberLevel = PlayerPrefs.GetInt("CompletedLevel", 0);
        win = GameObject.Find("boardwin").GetComponent<UiPanelDotween>();
        fireBtn = GameObject.Find("fire").GetComponent<Button>();
        iceBtn = GameObject.Find("ice").GetComponent<Button>();
        noMoney.SetActive(false);
    }


    private void Start()
    {
        LoadMap(numberSelect);
        currenPartBody = -1; // nếu 0 = thì sẽ lòi ra1 cục
        currentBody = 0;
        for (int i = 0; i < numBody; i++)
        {
            Body body = Instantiate(Body, new Vector2(10, 0), Quaternion.identity).GetComponent<Body>();
            listBody.Add(body);
        }
        LoadSegment();

    }
    private void Update()
    {
        if (listBody.Count == 0 && !hasWin)
        {
            hasWin = true;
            Win();
        }
    }
    public void Win()
    {
        win.PanelFadeIn();
        Shop.Instance.gold += 200;
        Shop.Instance.Save();

    }
    void LoadSegment()
    {
        for (int i = 0; i < numContainer; i++)
        {
            currenPartBody++;

            Vector3 pointsp = pointSpawn.position;
            pointsp.x += i * size;

            Transform container = Instantiate(ContainPartBody, pointsp, Quaternion.identity);
            container.GetComponent<ContainPartBody>().hasBody = true;
            listContainPartBody.Add(container);


            if (currenPartBody == 10)
            {
                currenPartBody = 0;
                currentBody++;
            }
            GameObject partBodyPrefab = (currentBody % 2 == 0) ? PartBody1 : PartBody2;

            PartBody partBody = Instantiate(partBodyPrefab, new Vector2(10, 0), Quaternion.identity).GetComponent<PartBody>();
            partBody.body = listBody[currentBody];
            partBody.transform.SetParent(listBody[currentBody].transform);
            partBody.id = i;
        }
    }

    void LoadMap(int index)
    {
        Instantiate(pointData.point[index], new Vector2(0, 0), Quaternion.identity);
        numBody = level[index].numbody;
        numPartBody = level[index].numPartBody;
        numContainer = level[index].numContainer;

        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Instantiate(playerPrefabs[characterIndex], new Vector2(0, -4), Quaternion.identity);
    }
    public void NextLevel()
    {
        numberSelect++;
        PlayerPrefs.SetInt("plus", (numberSelect) * 5);
        if (numberSelect > numberLevel) numberLevel++;
        else numberLevel = numberSelect;
        PlayerPrefs.SetInt("SelectedLevel", numberSelect);
        Debug.Log("1");
        if (numberLevel >= numberSelect)
        {

            PlayerPrefs.SetInt("CompletedLevel", numberLevel);
            PlayerPrefs.SetInt("plus", (numberLevel) * 5);
            PlayerPrefs.Save();
            Debug.Log("2");
        }
        PlayerPrefs.Save();
        sceneFader.FadeTo("GamePlay");

    }
    public void HomeScene()
    {
        sceneFader.FadeTo("Home");
    }

    public void Ice()
    {
        int price = 100;
        if (Shop.Instance.gold >= 100)
        {
            Shop.Instance.gold -= price;
            Shop.Instance.Save();
            iceBtn.interactable = false;

            canMove = false;
            GameObject effIce = Instantiate(effectIce, transform.position, Quaternion.identity);
            Destroy(effIce, 5f);
            StartCoroutine("StopMoveSnake");
        }
        else noMoney.SetActive(true);
    }

    public IEnumerator StopMoveSnake()
    {
        yield return new WaitForSeconds(5f);
        canMove = true;
    }

    public void Fire()
    {
        int price = 100;
        if (Shop.Instance.gold >= 100)
        {
            Shop.Instance.gold -= price;
            Shop.Instance.Save();
            fireBtn.interactable = false;
            for (int i = 0; i < listBody.Count; i++)
            {
                if (listBody[i] != null)
                {
                    GameObject effFire = Instantiate(effectFire, listBody[i].transform.position, Quaternion.identity);
                    Destroy(effFire, 1f);
                    listBody[i].TakedDamage(5);
                }
            }

            listBody.RemoveAll(body => body == null);
        }
        else noMoney.SetActive(true);
    }

    public void Replay()
    {
        sceneFader.FadeTo("GamePlay");
    }

    public void Pause()
    {
        canMove = false;
    }

    public void Resume()
    {
        canMove = true;
    }

}