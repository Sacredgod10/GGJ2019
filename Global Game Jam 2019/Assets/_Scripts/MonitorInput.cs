using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonitorInput : MonoBehaviour
{
    public GameObject player;
    public Camera gameCamera;
    public int date;
    public TMP_InputField textMesh;
    private bool lerping;
    public bool haveSlept;
    public string inputValue;
    private bool monitorDone;
    public Renderer monitorScreen;
    public Vector3 startingCameraPosition, endPosition;
    // Start is called before the first frame update

    void OnEnable()
    {
        date = CharacteristicsAndData.Instance.date;
        haveSlept = false;
        startingCameraPosition = gameCamera.transform.position;
        GameObject cameraPerspective = GameObject.Find("MonitorCamPerspective");
        endPosition = cameraPerspective.transform.position;
        lerping = true;
        monitorDone = false;
        textMesh.gameObject.SetActive(true);
        textMesh.ActivateInputField();
        player.GetComponent<PlayerMovement>().isFrozen = true;
        monitorScreen.material.SetFloat("_EmissiveBlink_Min", 0.5f);
        monitorScreen.material.SetFloat("_EmissiveBlink_Max", 1f);
        monitorScreen.material.SetFloat("_EmissiveBlink_Velocity", 5f);
        monitorScreen.material.SetFloat("_EmissiveScroll_Width", 20);
    }

    void OnDisable()
    {
        //teleport player to monitor position
        player.GetComponent<PlayerMovement>().isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            LerpCamera(gameCamera.transform.position, endPosition, 5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape) || monitorDone)
        {
            ExitMonitor(gameCamera.transform.position, startingCameraPosition, 5f);
        }
        inputValue = textMesh.text;

        textMesh.onSubmit.AddListener(delegate { CheckForAnswer(textMesh.text); });
    }

    public void ExitMonitor(Vector3 curPos, Vector3 endPos, float speed)
    {
        gameCamera.transform.localPosition = new Vector3(0, 0.55f, 0);
        textMesh.gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().isFrozen = false;
        GetComponent<MonitorInput>().enabled = false;
    }
    public void LerpCamera(Vector3 curPos, Vector3 endPos, float speed)
    {
        gameCamera.transform.position = Vector3.Lerp(curPos, endPos, speed * Time.deltaTime);
        if (Vector3.Distance(curPos, endPos) < 0.01f)
        {
            lerping = false;
        }
    }

    public void CheckForAnswer(string playerInput)
    {
        switch(date)
        {
            case 25:
                if(playerInput == "AN" || playerInput == "NA")
                {
                    GoodAnswer();
                    break;
                }
                else
                {
                    WrongAnswer();
                }
                break;
            case 26:
                if (playerInput == "CE" || playerInput == "EC")
                {
                    GoodAnswer();
                    break;
                }
                else
                {
                    WrongAnswer();
                }
                break;
            case 27:
                if(playerInput == "LV" || playerInput == "VL")
                {
                    GoodAnswer();
                    break;
                }
                else
                {
                    WrongAnswer();
                }
                break;
        }
    }

    public void GoodAnswer()
    {
        if (!haveSlept)
        {
            haveSlept = true;
            Debug.Log("Yes");
            monitorScreen.material.SetFloat("_EmissiveBlink_Min", 0.5f);
            monitorScreen.material.SetFloat("_EmissiveBlink_Max", 1f);
            monitorScreen.material.SetFloat("_EmissiveBlink_Velocity", 5f);
            monitorScreen.material.SetFloat("_EmissiveScroll_Width", 0);
            CharacteristicsAndData.Instance.Sleep();
            monitorDone = true;
        }
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong!");
        textMesh.text = "";
    }
}
