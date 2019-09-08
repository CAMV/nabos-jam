using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceBarTracker : MonoBehaviour
{
    [SerializeField]
    private Image _bar;
    [SerializeField]
    private Resource _resource;


    // Update is called once per frame
    void Update()
    {
        if (_resource)
            _bar.fillAmount = ((float)_resource.currentResource)/((float)_resource.maxResource);
        else
            _bar.fillAmount = 0f;
    }
}
