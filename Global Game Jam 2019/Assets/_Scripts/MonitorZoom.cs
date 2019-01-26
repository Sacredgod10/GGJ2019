using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorZoom : MonoBehaviour
{
    public bool isOn = false;

    public void ChangeTvState(bool state)
    {
        isOn = state;
    }
}
