using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] downRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject[] weaponType;
    public GameObject bonusType;
    public GameObject[] bossType;
    [Header("")]
    [SerializeField] GameObject NextLevelText;
    public int levelOfNumber;
    [HideInInspector] public List<GameObject> rooms;

    [Header("")]
    [SerializeField] GameObject mainRoomPref;
    void Start()
    {
        levelOfNumber = 1;
        NextLevelText = GameObject.Find("NumberOfLevel_Text");
        NextLevelText.SetActive(false);
        StartCoroutine(RandomSpawner());
        StartCoroutine(nextLevelTextCor(1));
    }
    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int randWeapon = Random.Range(0,weaponType.Length);

        Instantiate(weaponType[randWeapon], rooms[rooms.Count - 3].transform.position, Quaternion.identity);

        lastRoom.IsBossRoom = true;
        lastRoom.DoorOn();
        Debug.Log(rooms[rooms.Count - 1].name);
    }
    public void NextLevel()
    {
        StartCoroutine(NextLevelCor());
    }
    IEnumerator NextLevelCor()
    {
        Ui_Manager uiManager = GameObject.Find("Player_Ui").GetComponent<Ui_Manager>();
        uiManager.BlackScreanOn();
        GameObject[] roomsM = GameObject.FindGameObjectsWithTag("Room");
        yield return new WaitForSeconds(1.6f);
        for (int i = 0; i <= roomsM.Length - 1 ; i++)
        {
            Destroy(roomsM[i]);
        }
        GameObject[] bonus = GameObject.FindGameObjectsWithTag("bonus");
        for (int i = 0; i < bonus.Length; i++)
        {
            Destroy(bonus[i]);
        }
        rooms.Clear();
        Debug.Log(rooms.Count);
         
        Destroy(GameObject.FindGameObjectWithTag("DropWeapon"));
        Destroy(GameObject.FindGameObjectWithTag("MainRoom"));
   
        mainRoomPref.GetComponent<AddRoom>().enabled = false;
        Instantiate(mainRoomPref, new Vector3(0, 0, 0), Quaternion.identity);

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, 0);

        StartCoroutine(RandomSpawner());

        yield return new WaitForSeconds(1f);
        uiManager.StartCoroutine(uiManager.OffBlacScrean());
        levelOfNumber += 1;
        StartCoroutine(nextLevelTextCor(0));
    }
    IEnumerator nextLevelTextCor(float Startdelay)
    {
        yield return new WaitForSeconds(Startdelay);
        Text textNext = NextLevelText.GetComponent<Text>();
        textNext.text = $"Level {levelOfNumber}";

        NextLevelText.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        NextLevelText.SetActive(false);
    }
    
}
