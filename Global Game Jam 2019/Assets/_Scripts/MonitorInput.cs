using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorInput : MonoBehaviour
{
    public GameObject player;
    public Renderer monitorScreen;
    public int numberOfInputSlots = 5;
    public KeyCode[] inputArray;
    public KeyCode lastCharacter;
    // Start is called before the first frame update
    void OnEnable()
    {
        inputArray = new KeyCode[numberOfInputSlots];
        player.GetComponent<PlayerMovement>().isFrozen = true;
        monitorScreen.material.SetFloat("_Desaturation", 0);
        monitorScreen.material.SetFloat("_EmissiveBlink_Min", 0.5f);
        monitorScreen.material.SetFloat("_EmissiveBlink_Max", 1f);
        monitorScreen.material.SetFloat("_EmissiveBlink_Velocity", 5f);
    }

    void OnDisable()
    {
        //teleport player to monitor position
        player.GetComponent<PlayerMovement>().isFrozen = false;
    }
    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.GetComponent<MonitorInput>().enabled = false;
        }
        if(numberOfInputSlots == 0)
        {
            CheckForRightAnswer();
        }
    }

    public void CheckForInput()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                    if (inputArray[0] == KeyCode.None)
                    {
                        lastCharacter = vKey;
                        inputArray[0] = lastCharacter;
                    numberOfInputSlots--;
                        Debug.Log("First Character is " + vKey);
                    }
                    else if (inputArray[1] == KeyCode.None)
                {
                    lastCharacter = vKey;
                    numberOfInputSlots--;
                    inputArray[1] = lastCharacter;
                    Debug.Log("Second Character is " + vKey);
                }
            }
        }
    }

    public void CheckForRightAnswer()
    {
        if((inputArray[0] == KeyCode.A && inputArray[1] == KeyCode.N) ||
           (inputArray[0] == KeyCode.N && inputArray[1] == KeyCode.A)) {
            Debug.Log("Victory!");
            monitorScreen.material.SetFloat("_Desaturation", 0);
            monitorScreen.material.SetFloat("_EmissiveBlink_Min", 0.5f);
            monitorScreen.material.SetFloat("_EmissiveBlink_Max", 1f);
            monitorScreen.material.SetFloat("_EmissiveBlink_Velocity", 5f);
            monitorScreen.material.SetFloat("_EmissiveScroll_Width" , 0);
            //Play success sound
        }
        else
        {
            inputArray[0] = KeyCode.None;
            inputArray[1] = KeyCode.None;
            Debug.Log("Nope!");
            //Play fail sound
        }
    }
}
