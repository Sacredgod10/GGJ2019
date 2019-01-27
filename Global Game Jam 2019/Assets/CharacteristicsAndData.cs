using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsAndData : MonoBehaviour
{
    private static CharacteristicsAndData _instance;

    public static CharacteristicsAndData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CharacteristicsAndData>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public enum Pets
    {
        CAT,
        DOG
    }

    public enum Characteristic
    {
        HUNGRY,
        MESSY,
        OUTSIDE,
        MOODY
    }

    public string name;
    public Pets favPet;
    public Characteristic characteristic;
    public int date = 25;
    public int currentRooms;
    public List<int> happyPercentages;
    public GameObject player;
    public GameObject startdoor;
    public Vector3 spawnPos;
    public GameObject[] allRooms;

    public void Sleep()
    {
        date++;
        for (int i = 0; i < happyPercentages.Count; i++)
        {
            if (happyPercentages[i] < 100)
            {
                happyPercentages[i] += 15; // Add 15 percent to each unlocked room's happyness
                for (int j = 0; j < allRooms[i].GetComponent<HiddenItems>().blockingItems.Count; j++)
                {
                    allRooms[i].GetComponent<HiddenItems>().blockingItems[j].SetActive(false);
                }
            }

            if (happyPercentages[i] >= 0 && happyPercentages[i] < 25)
            {
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[0].SetActive(true);
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[1].SetActive(true);
            }

            if (happyPercentages[i] >= 25 && happyPercentages[i] < 50) // Reward 1
            {
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[2].SetActive(true);
            }
            else if (happyPercentages[i] >= 50 && happyPercentages[i] < 75) // Reward 2
            {
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[3].SetActive(true);
            }
            else if (happyPercentages[i] >= 75 && happyPercentages[i] < 100) // Reward 3
            {
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[4].SetActive(true);
            }
            else if (happyPercentages[i] >= 100) // Reward 4
            {
                allRooms[i].GetComponent<HiddenItems>().allHiddenItems[5].SetActive(true);
            }
        }

        if (currentRooms < allRooms.Length - 1)
        {
            AddNewRoom();
        }

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = spawnPos;
        player.GetComponent<CharacterController>().enabled = true;

    }

    public void AddNewRoom()
    {
        startdoor.transform.localEulerAngles = new Vector3(0, 0, 0);
        currentRooms++;
        happyPercentages.Add(new int());
        allRooms[currentRooms].SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sleep();
        }
    }
}
