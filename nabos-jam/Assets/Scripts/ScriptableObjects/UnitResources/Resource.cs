using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : ScriptableObject
{
    [SerializeField]
    public int maxResource;
    [SerializeField]
    public int currentResource;

    private void OnEnable()
    {
        currentResource = maxResource;
    }


    public void modifyResource(int resourceMod) 
    {
        currentResource += resourceMod;
        if (currentResource < 0) 
        {
            currentResource = 0;
        }
        else if (currentResource > maxResource)
        {
            currentResource = maxResource;
        }
    }
}
