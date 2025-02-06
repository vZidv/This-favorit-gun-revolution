using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    [SerializeField] GameObject weaponNow;
    public GameObject weaponSecond;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] weaponsDrops;

    [SerializeField] GameObject bulletPrefab;

    public float swapWeaponTime;
    private float swapWeaponTimeStart;

    Transform weaponPointL;
    Transform weaponPointR;

    private Player_Control player;
    private Weapon_imageManager weaponImage;

    [Header("Android")]
    public Joystick joystickFire;
    void Start()
    {
        weaponNow = weapons[0]; //0
        weaponNow.SetActive(true);

        swapWeaponTimeStart = swapWeaponTime;

        weaponPointL = gameObject.transform.Find("Weapon_PointL");
        weaponPointR = gameObject.transform.Find("Weapon_PointR");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        weaponImage = GameObject.FindObjectOfType<Weapon_imageManager>().GetComponent<Weapon_imageManager>();


        if (player.controlType == Player_Control.ControlType.PC)
        {
            joystickFire.gameObject.SetActive(false);
            GameObject android_Ui = GameObject.Find("Android_buttons");           
            android_Ui.SetActive(false);
        }
            

    }

    
    void FixedUpdate()
    {
        float angleX;
        //Pc
        if (player.controlType == Player_Control.ControlType.PC)
        {           

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
            if (mousePos.x - gameObject.transform.position.x >= 0)
            {
                weaponNow.transform.position = weaponPointR.position;
                angleX = 0;
            }
            else
            {
                weaponNow.transform.position = weaponPointL.position;
                angleX = 180;
            }

            mousePos -= transform.position;
            float rotate = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (angleX == 180)
                rotate = -rotate;
            weaponNow.transform.rotation = Quaternion.Euler(angleX, 0, rotate);

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
            if (Input.GetKey(KeyCode.Q) && weaponSecond != null && swapWeaponTime <= 0)
            {
                SwapWeapon();
            }
            
            
        }
        //Android
        else if (player.controlType == Player_Control.ControlType.Android)
        {     
            
            if (joystickFire.Horizontal >= 0)
            {
                weaponNow.transform.position = weaponPointR.position;
                angleX = 0;
            }
            else 
            {
                weaponNow.transform.position = weaponPointL.position;
                angleX = 180;
            }

            float rotate = Mathf.Atan2(joystickFire.Vertical, joystickFire.Horizontal) * Mathf.Rad2Deg;
            if (angleX == 180)
                rotate = -rotate;
            weaponNow.transform.rotation = Quaternion.Euler(angleX, 0, rotate);

            if(joystickFire.Horizontal != 0 || joystickFire.Vertical != 0)
            {
                Shoot();
            }
        }
         if (swapWeaponTime != 0)
        {
            swapWeaponTime -= Time.deltaTime;
        }

    }
    public void Shoot()
    {
        Weapn_Stats weapon = weaponNow.GetComponent<Weapn_Stats>();
        if (weapon.timeForFire <= 0) {
            weapon.Shoot(bulletPrefab);
            weapon.timeForFire = weapon.timeForFireStart;
        }
        else
        {
            weapon.timeForFire -= Time.deltaTime;
        }
    }
    public void SwitchWeapon(string nameWeapon)
    {
        int i = 0;
        while (weapons[i].name != nameWeapon)
        {
            i++;
        }
        if (weaponSecond == null)
        {            
            weaponSecond = weapons[i];
        }
        else
        {
            int w = 0;
            while (weaponsDrops[w].name !=  $"Weapon_upGun({weaponNow.name})")
            {
                w++;
            }
            Instantiate(weaponsDrops[w], gameObject.transform.position, Quaternion.identity);
            weaponNow.SetActive(false);
            weaponNow = weapons[i];
            weaponNow.SetActive(true);
        }
    }
    public void SwapWeapon()
    {
        if (weaponSecond != null)
        {
            weaponNow.SetActive(false);
            weaponSecond.SetActive(true);

            weaponImage.SwitchWeaponImage(weaponNow.name);

            GameObject weaponbefor;
            weaponbefor = weaponNow;
            weaponNow = weaponSecond;
            weaponSecond = weaponbefor;
            swapWeaponTime = swapWeaponTimeStart;
        }
    }
}
