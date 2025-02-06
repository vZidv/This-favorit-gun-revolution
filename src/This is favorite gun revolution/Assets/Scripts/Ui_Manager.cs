using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEditor;


public class Ui_Manager : MonoBehaviour
{
    public GameObject UpWeapon_Button;
    public Image[] hearts;
    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;
    [Header("")]
    public AudioMixerGroup masterMixer;
    [Header("")]
    public GameObject ButtonDoorForBoss;
    [Header("")]
    public GameObject PlusCoin;
    public Text ResultEnd;
    public GameObject blackScrean;
    Animator ani_BlackScrean;
    [Header("Screan Sets")]
    public GameObject[] screanType;


    Player_Control player;
    public Color newCol;
    void Start()
    {
        Time.timeScale = 1;

        Debug.Log(PlayerPrefs.GetString("Color"));

        PlusCoin.SetActive(false);
        ButtonDoorForBoss.SetActive(false);
        ani_BlackScrean = blackScrean.GetComponent<Animator>();
        blackScrean.SetActive(true);
        StartCoroutine(OffBlacScrean());

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();

        if (PlayerPrefs.GetString("Color") == "Stantart")
            newCol = new Color32(88, 219, 179, 255);
        else if (PlayerPrefs.GetString("Color") == "Grean")
            newCol = new Color32(0, 255, 2, 255);
        else if (PlayerPrefs.GetString("Color") == "Pip")
            newCol = new Color32(255, 0, 180, 255);

        player.gameObject.GetComponent<SpriteRenderer>().color = newCol;
        UpWeapon_Button = GameObject.Find("Button_UpWeapon");
        UpWeapon_Button.SetActive(false);

        SwitchScrean("MainScrean");
    }
    public void BlackScreanOn()
    {
        StartCoroutine(BlackSvreanOnCor());
    }

    public void UpdateHearts(int heartOfNumber)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < heartOfNumber)           
                hearts[i].sprite = heartFull;           
            else
                hearts[i].sprite = heartEmpty;
        }

    }
    IEnumerator BlackSvreanOnCor()
    {
        blackScrean.SetActive(true);
        ani_BlackScrean.SetInteger("Status", 2);
        yield return new WaitForSeconds(1.4f);
        ani_BlackScrean.SetInteger("Status", 0);
       
    }
    public IEnumerator OffBlacScrean()
    {
        yield return new WaitForSeconds(1.5f);
        ani_BlackScrean.SetInteger("Status", 1);
        yield return new WaitForSeconds(1.5f); //1.4
        blackScrean.SetActive(false);

    }
    public void PauseScrean()
    {
        SwitchScrean("PauseScrean");
        Time.timeScale = 0;
    }
    public void ReturButton()
    {
        SwitchScrean("MainScrean");
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void MenuReturn()
    {
        Time.timeScale = 1;
        StartCoroutine(MenuReturnCor());
    }
    public void DeadScrean()
    {
        ResultEnd.text =($"Ваш результат: {System.Convert.ToString(GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>().levelOfNumber)}");
        SwitchScrean("DeadScrean");
        Time.timeScale = 0;
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
    public void SwitchScrean(string nameScrean)
    {
        for (int i = 0; i < screanType.Length; i++)
        {
            if (screanType[i].name == nameScrean)
                screanType[i].SetActive(true);
            else
             screanType[i].SetActive(false);
        }       
    }
    public void CoinPlus()
    {
        StartCoroutine(CoinPlusCor());
    }
    IEnumerator MenuReturnCor()
    {
        blackScrean.SetActive(true);
        ani_BlackScrean.SetInteger("Status", 2);
        yield return new WaitForSeconds(1.4f);
        ani_BlackScrean.SetInteger("Status", 0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }
     IEnumerator CoinPlusCor()
    {
        DataHolder.CoinPlus();
        PlusCoin.SetActive(true);
        yield return new WaitForSeconds(0.99f);
        PlusCoin.SetActive(false);
    }
}
