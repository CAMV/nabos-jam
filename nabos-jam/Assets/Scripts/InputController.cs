using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private List<InputAction> _inputActions;

    void Start()
    {
        StartCoroutine(UpdateCo());
    }


    private IEnumerator UpdateCo()
    {
        while (true)
        {
            foreach(var input in _inputActions)
            {
                if (input.CheckInput())
                {
                    Debug.Log("Done");
                    yield return StartCoroutine(input.ExecuteAction());  
                }

            }
            
            yield return new WaitForEndOfFrame();
        }

        
    }
}