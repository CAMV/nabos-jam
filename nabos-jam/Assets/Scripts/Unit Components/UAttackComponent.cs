using UnityEngine;

public class UAttackComponent : MonoBehaviour 
{
    [SerializeField]
    public Skill _basicAttack;


    private Unit _myUnit;

    public void Initialize()
    {
        _basicAttack = ScriptableObject.Instantiate(_basicAttack);
        _basicAttack.Initialize(_myUnit);
    }

    public void Attack(Unit target)
    {
        _basicAttack.Execute(true);
    }

    void Start()
    {
        _myUnit = GetComponent<Unit>();
        Initialize();
    }
}