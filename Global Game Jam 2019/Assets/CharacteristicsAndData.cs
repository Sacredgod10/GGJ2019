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

    public void Sleep()
    {
        date++;
    }
}
