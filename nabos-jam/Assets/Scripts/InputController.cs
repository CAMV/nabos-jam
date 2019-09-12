using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controller that handles all the input actions available.
/// </summary>
public class InputController : MonoBehaviour
{
    public static bool isAlwaysQuickCast = false;

    [SerializeField]
    private List<InputAction> _inputActions = new List<InputAction>();


    void Start()
    {
        StartCoroutine(UpdateCo());
    }

    /// <summary>
    /// Updates coroutine to make wait until a input action finish before exceuting another one. 
    /// </summary>
    private IEnumerator UpdateCo()
    {
        while (true)
        {
            foreach(var input in _inputActions)
            {
                if (input.CheckInput())
                {
                    yield return StartCoroutine(input.ExecuteAction());  
                }

            }
            
            yield return new WaitForEndOfFrame();
        }

        
    }
}