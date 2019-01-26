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
    public int date;
    public List<GameObject> currentRooms;
    public int[] happyPercentages;
    public GameObject player;
    public Vector3 spawnPos;
    public GameObject[] allRooms;

    public void Start()
    {
        happyPercentages = new int[currentRooms.Count];
    }

    public void Sleep()
    {
        date++;
        player.transform.position = spawnPos;
        for (int i = 0; i < happyPercentages.Length; i++)
        {
            if (i < 100)
            {
                i += 15;
            }
        }
    }

    public void AddNewRoom()
    {

    }

}
