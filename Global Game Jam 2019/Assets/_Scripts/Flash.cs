using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Flash : MonoBehaviour
{
    private Image OnHitFlash;
    private Stopwatch sw;
    public bool good = false;

        // Use this for initialization
    void Start()
    {
        sw = new Stopwatch();
        OnHitFlash = GetComponent<Image>();
        OnHitFlash.GetComponent<CanvasRenderer>().SetAlpha(0f);
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (sw.ElapsedMilliseconds > 100)
        {
            sw.Reset();
            FlashOut();
        }
    }

    public void FlashOut()
    {
        if (good)
        {
            OnHitFlash.color = new Color(0, 255, 0);
        }
        else
        {
            OnHitFlash.color = new Color(255, 0, 0);
        }
        OnHitFlash.CrossFadeAlpha(0f, 0.2f, false);
    }

    public void FlashIn()
    {
        if (good)
        {
            OnHitFlash.color = new Color(0, 255, 0);
        }
        else
        {
            OnHitFlash.color = new Color(255, 0, 0);
        }
        sw.Start();
        OnHitFlash.CrossFadeAlpha(0.8f, 0.2f, false);
    }
}