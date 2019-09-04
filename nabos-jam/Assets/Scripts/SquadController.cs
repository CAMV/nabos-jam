using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{

    [SerializeField]
    private List<Unit> _myUnits;

    private Unit _activeUnit;
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;

    public Unit ActiveUnit {
        get {
            if (!_activeUnit)
                _activeUnit = _myUnits[0];
            
            return _activeUnit;
        }
    }

    public void AddCommand(Command c)
    {
        _cmdQ.Enqueue(c);
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
