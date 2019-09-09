using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Class that tracks and handles updating resource bars on the GUI, based on
/// a given <c>Resource</c> class
/// </summary>
public class ResourceBarTracker : MonoBehaviour
{
    [SerializeField]
    private Image _bar = null;             //The fill bar to be updated
    [SerializeField]
    private Resource _resource = null;     //The resource to track

    // Update is called once per frame
    void Update()
    {
        if (_resource)
            _bar.fillAmount = ((float)_resource.currentResource)/((float)_resource.maxResource);
        else
            _bar.fillAmount = 0f;
    }
}
