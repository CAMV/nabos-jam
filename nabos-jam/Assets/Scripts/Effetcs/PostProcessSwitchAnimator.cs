using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessSwitchAnimator : MonoBehaviour
{
    //////////////////////////////////////////////////////
    public bool trigger;

    //////////////////////////////////////////////////////

    [SerializeField]
    private float _animDuration = 1.0f;
    [SerializeField]
    [Range(0,1)]
    private float _offValue = 0;
    private bool _isOn = false;

    private PostProcessVolume _myPPV;

    public float AnimDuration 
    {
        get {
            return _animDuration;
        }

        set {
            if (value >= 0)
                _animDuration = value;
        }
    }

    void Start()
    {
        _myPPV = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        if (trigger)
        {
            trigger = false;
            if (_isOn)
                TunrOff();
            else
                TunrOn();
        }
    }

    public void TunrOn()
    {
        if (!_isOn)
            StartCoroutine(AnimateWeight(1));
    
        _isOn = true;
    }

    public void TunrOff()
    {
        if (_isOn)
            StartCoroutine(AnimateWeight(_offValue));

        _isOn = false;
    }

    private IEnumerator AnimateWeight(float targetWeight)
    {
        float currentWeight = _myPPV.weight;
        float elapsedTime = 0;

        while (elapsedTime < _animDuration)
        {
            _myPPV.weight = Mathf.Lerp(currentWeight, targetWeight, elapsedTime/_animDuration);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        _myPPV.weight = targetWeight;
    }
}
