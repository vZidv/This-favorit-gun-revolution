using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu_Manager : MonoBehaviour
{
    [Header("Menu Sets")]
    public GameObject mainMenu;
    public GameObject shopMenu;

    [Header("Shop")]
    public Text Coins;
    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    bool IsBuyskin2 = false;
    bool IsBuyskin3 = false;
    [Header("")]
    public AudioMixerGroup masterMixer;
    [Header("")]
    public GameObject blackScrean;
    Animator ani_BlackScrean;
 
    void Start()
    {

        if (PlayerPrefs.GetString("Color") == null)
            Button1();
            //PlayerPrefs.SetString("Color", "Stantart");

        IsBuyskin2 = intToBool(PlayerPrefs.GetInt("IsBuyskin2"));
        IsBuyskin3 = intToBool(PlayerPrefs.GetInt("IsBuyskin3"));

        

        if (PlayerPrefs.GetString("Color") == "Stantart")
        {
            skin1.transform.Find("Text").GetComponent<Text>().text = "Надет";
            Debug.Log("a");
        }        
        else
        {
            skin1.transform.Find("Text").GetComponent<Text>().text = "Надеть";

        }
         if (PlayerPrefs.GetString("Color") == "Grean" && IsBuyskin2)
            skin2.transform.Find("Text").GetComponent<Text>().text = "Надет";
        else if (!IsBuyskin2)
        {
            skin2.transform.Find("Text").GetComponent<Text>().text = "Купить";
        }
        else
        {
            skin2.transform.Find("Text").GetComponent<Text>().text = "Надеть";
        }
        if (PlayerPrefs.GetString("Color") == "Pip" && IsBuyskin3)
            skin3.transform.Find("Text").GetComponent<Text>().text = "Надет";
        else if (!IsBuyskin2)
        {
            skin3.transform.Find("Text").GetComponent<Text>().text = "Купить";
        }
        else
        {
            skin3.transform.Find("Text").GetComponent<Text>().text = "Надеть";
        }

        DataHolder.coins = PlayerPrefs.GetInt("Coin");
        Coins.text =System.Convert.ToString(DataHolder.coins);
        mainMenu.SetActive(true);
        shopMenu.SetActive(false);

        blackScrean.SetActive(true);
        ani_BlackScrean = blackScrean.GetComponent<Animator>();
        ani_BlackScrean.SetInteger("Status", 3);
        StartCoroutine(OffBlacScrean());

        
    }

    public void soundOn()
    {
        if (DataHolder.sound)
        {
            masterMixer.audioMixer.SetFloat("SoundMaster", -80);
            DataHolder.sound = false;
        }
        else
        {
            masterMixer.audioMixer.SetFloat("SoundMaster", 0);
            DataHolder.sound = true;
        }
    }
    public void Play()
    {
        StartCoroutine(PlayStart());
    }
    public void ShopButton()
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    public void Button1()
    {

            skin1.transform.Find("Text").GetComponent<Text>().text = "Надет";
            PlayerPrefs.SetString("Color", "Stantart");
      

    }
    public void Button2()
    {

        if (IsBuyskin2 == false)
        {
            if (DataHolder.coins >= 10)
            {
                skin2.transform.Find("Text").GetComponent<Text>().text = "Надеть";
                IsBuyskin2 = true;
                PlayerPrefs.GetInt("IsBuyskin2", boolToInt(true));
                MinusMoney();
            }
        }
        else
        {
            skin2.transform.Find("Text").GetComponent<Text>().text = "Надет";
            PlayerPrefs.SetString("Color", "Grean");
        }
    }
    public void Button3()
    {
        if (IsBuyskin3 == false)
        {
            if (DataHolder.coins >= 10)
            {
                skin3.transform.Find("Text").GetComponent<Text>().text = "Надеть";
                IsBuyskin3 = true;
                PlayerPrefs.GetInt("IsBuyskin3", boolToInt(true));
                MinusMoney();
            }
        }
        else
        {
            skin3.transform.Find("Text").GetComponent<Text>().text = "Надет";
            PlayerPrefs.SetString("Color", "Pip");
        }
    }
    void MinusMoney()
    {
        DataHolder.coins -= 10;
        PlayerPrefs.SetInt("Coin", DataHolder.coins);
        Coins.text = System.Convert.ToString(DataHolder.coins);
    }
    private void FixedUpdate()
    {
        if (shopMenu == true) 
        {
            if (PlayerPrefs.GetString("Color") == "Stantart")
            {
                skin1.transform.Find("Text").GetComponent<Text>().text = "Надет";
                Debug.Log("a");
            }
            else
            {
                skin1.transform.Find("Text").GetComponent<Text>().text = "Надеть";

            }
            if (PlayerPrefs.GetString("Color") == "Grean" && IsBuyskin2)
                skin2.transform.Find("Text").GetComponent<Text>().text = "Надет";
            else if (!IsBuyskin2)
            {
                skin2.transform.Find("Text").GetComponent<Text>().text = "Купить";
            }
            else
            {
                skin2.transform.Find("Text").GetComponent<Text>().text = "Надеть";
            }
            if (PlayerPrefs.GetString("Color") == "Pip" && IsBuyskin3)
                skin3.transform.Find("Text").GetComponent<Text>().text = "Надет";
            else if (!IsBuyskin3)
            {
                skin3.transform.Find("Text").GetComponent<Text>().text = "Купить";
            }
            else
            {
                skin3.transform.Find("Text").GetComponent<Text>().text = "Надеть";
            }
        }
    }


    public int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
    public bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    IEnumerator PlayStart()
    {
        blackScrean.SetActive(true);
        ani_BlackScrean.SetInteger("Status", 2);
        yield return new WaitForSeconds(1.4f);
        ani_BlackScrean.SetInteger("Status", 0);
        yield return new WaitForSeconds(1.4f);

        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(1.4f);
        blackScrean.SetActive(false);
    }
    IEnumerator OffBlacScrean()
    {
        yield return new WaitForSeconds(1.4f);
        ani_BlackScrean.SetInteger("Status", 1);
        yield return new WaitForSeconds(1.5f); //1.4
        blackScrean.SetActive(false);
    }
}
