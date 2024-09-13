using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public GameObject[] buttons;
    public Button[] btn;
    public int characterSelect;

    int price;
    //public GameObject nomoney;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        characterSelect = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[characterSelect].SetActive(true);
        //nomoney.SetActive(false);
        int index = 0;
        foreach (Button bt in btn)
        {
            int buttonIndex = index;
            bt.onClick.AddListener(() => BuyCharacter(buttonIndex + 1));
            index++;
        }
 
    }

    public void Next()
    {
        //AudioManager.Instance.AudioButtonClick();
        skins[characterSelect].SetActive(false);
        characterSelect++;
        if (characterSelect == skins.Length) characterSelect = 0;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);


        if (PlayerPrefs.GetInt("Character_" + (characterSelect) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }
    public void Back()
    {
        //AudioManager.Instance.AudioButtonClick();
        skins[characterSelect].SetActive(false);
        characterSelect--;
        if (characterSelect == -1) characterSelect = skins.Length - 1;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);

        if (PlayerPrefs.GetInt("Character_" + (characterSelect) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }
    public void Price(int price)
    {
        this.price=price;
    }
    public void BuyCharacter(int characterIndex)
    {
        //AudioManager.Instance.AudioButtonClick();

        //int priceCharacter = 1000;
        if (Shop.Instance.gold >= price)
        {
            Shop.Instance.gold -= price;
            Shop.Instance.Save();
            Shop.Instance.UpdateGold();
            PlayerPrefs.SetInt("Character_" + (characterIndex) + "_Bought", 1);
            buttons[characterIndex].SetActive(false);
        }
        //else
        //{
        //    nomoney.SetActive(true);
        //}
    }
    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(i == characterSelect - 1);
            if (PlayerPrefs.GetInt("Character_" + (i + 1) + "_Bought", 0) == 1)
            {
                buttons[i].SetActive(false);
            }
        }
    }
}
