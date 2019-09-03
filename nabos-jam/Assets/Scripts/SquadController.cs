using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    private Unit _activeUnit;
    private Unit _myUnit;
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;


    public Unit ActiveUnit {
        get {
            return _activeUnit;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _cmdQ = new Queue<Command>();
        _isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cmdQ.Count > 0 && _isIdle)
        {
            _cmdQ.Dequeue().Do();
        }
    }
}
