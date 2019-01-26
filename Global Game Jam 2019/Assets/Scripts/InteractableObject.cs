using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRange;
    public float glowIntensity = 1;
    public float shineCounter, previousShineCounter;
    public float shineCounterLimit = 10;
    public bool onlyInteractWhenCrouching;
    public bool beingLookedAt;
    private Material objectMaterial;
    private Interaction interaction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ResetCheck");
        objectMaterial = GetComponent<Renderer>().material;
        interaction = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEmission();

        if(shineCounter > (shineCounterLimit * 0.5) && Input.GetKeyDown(KeyCode.F))
        {
            interaction.Interact();
        }

        BalanceTheShine();
    }

    private void BalanceTheShine()
    {
        if(shineCounter < 0)
        {
            shineCounter = 0;
        }

        if(shineCounter > shineCounterLimit)
        {
            shineCounter = shineCounterLimit;
        }
    }
     
    IEnumerator ResetCheck()
    {
        while(true)
        {
            previousShineCounter = shineCounter;
            yield return new WaitForSeconds(2f);
            if (previousShineCounter >= shineCounterLimit)
            {
                shineCounter = shineCounterLimit - 1;
            }
            else if (previousShineCounter == shineCounter && previousShineCounter != shineCounterLimit)
            {
                shineCounter = 0;
            }
        }
    }
    //Checks whether the player has looked at the object for long enough
    void CheckForEmission()
    {
        if (shineCounter >= (shineCounterLimit - 1))
        {
            ShowEmission();
        }
        else
        {
            HideEmission();
        }
    }

    //Makes the object glow
    public void ShowEmission()
    {
        objectMaterial.SetFloat("_EmissionStrength", glowIntensity);
    }

    //Stops the object from glowing
    void HideEmission()
    {
        objectMaterial.SetFloat("_EmissionStrength", 0);
    }
}
