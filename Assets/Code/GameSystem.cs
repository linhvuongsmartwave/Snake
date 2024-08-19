﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;

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

    private List<Body> listBody = new List<Body>();
    public List<EnemyData> level;
    public GameObject[] playerPrefabs;
    int characterIndex;
    private int numberLevel;
    private int numberSelect;
    public SceneFader sceneFader;


    private void Awake()
    {
        Instance = this;
        //Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 0;
        numberSelect = PlayerPrefs.GetInt("SelectedLevel", 0);
        numberLevel = PlayerPrefs.GetInt("CompletedLevel", 0);
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
        if (numberSelect > numberLevel) numberLevel++;
        else numberLevel = numberSelect;
        sceneFader.FadeTo("GamePlay");
        PlayerPrefs.SetInt("SelectedLevel", numberSelect);
        PlayerPrefs.Save();
        if (numberLevel >= numberSelect)
        {
            PlayerPrefs.SetInt("CompletedLevel", numberLevel);
            PlayerPrefs.Save();
        }
    }

}