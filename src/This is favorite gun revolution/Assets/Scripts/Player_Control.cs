using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float health;
    private float healthStart;
    [Header("")]
    public float speed;
    [Header("")]
    public ControlType controlType;
    [Header("Android")]
    public Joystick joystickMove;

    bool isDead = false;
    bool pauseMenu = false;
    float hor, ver;
    public enum ControlType {PC, Android}
    void Start()
    {
        healthStart = health;
        if (controlType == ControlType.PC)
        {
            joystickMove.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        if (controlType == ControlType.PC)
        {
            hor = (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            ver = (Input.GetAxis("Vertical") * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu)
            {
                GameObject.Find("Player_Ui").GetComponent<Ui_Manager>().PauseScrean();
                pauseMenu = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu)
            {
                GameObject.Find("Player_Ui").GetComponent<Ui_Manager>().ReturButton();
                pauseMenu = false;
            }
        }
        else if (controlType == ControlType.Android)
        {
            hor = (joystickMove.Horizontal * speed * Time.deltaTime);
            ver = (joystickMove.Vertical * speed * Time.deltaTime);
        }
        Vector3 pos = new Vector3(hor, ver, 0);
        transform.position += pos;
        if (health > healthStart)
            health = healthStart;
        else if (health <= 0 && !isDead)
        {
            GameObject.Find("Player_Ui").GetComponent<Ui_Manager>().DeadScrean();
            isDead = true;
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        Ui_Manager ui = GameObject.Find("Player_Ui").GetComponent<Ui_Manager>();
        ui.UpdateHearts(Mathf.RoundToInt(health));
    }
}
