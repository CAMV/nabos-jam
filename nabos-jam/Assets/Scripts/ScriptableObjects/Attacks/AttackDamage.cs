using UnityEngine;

/// <summary>
/// Class that defines the types of damages an action can deal.
/// </summary>
[CreateAssetMenu(menuName = "AttackDamage")]
public class AttackDamage : ScriptableObject
{
    
    [SerializeField]
    private GUIData _guiData = null;

    [SerializeField]
    private UnitStat[] _propertyContributors = new UnitStat[0];
    
    [SerializeField]
    private float[] _contributionValue = new float[0];

    [SerializeField]
    private UnitResource[] _resourcesAffected = new UnitResource[0];

    [SerializeField]
    private UnitStat[] _statsMitigators = new UnitStat[0];
    
    [SerializeField]
    private float[] _mitagationValue = new float[0];

    private int _baseDamage;
    private Unit _originUnit;

    //////////////// PROPERTIES ////////////////
    
    /// <summary>
    /// Gets the gui data of the attack 
    /// </summary>
    public GUIData GUIData {
        get {
            return _guiData;
        }
    }

    //////////////// METHODS ////////////////

    /// <summary>
    /// Intialize an attack damege with a given base damage an the unit that is dealing the attack.
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="damage"></param>
    public void Initialize(Unit unit, int damage)
    {
        _baseDamage = damage;
        float aditionalDamage = 0;
        _originUnit = unit;
        
        if (!unit || !unit.Properties)
            return;

        // gets any aditional damage due to properties
        for (int i = 0; i < _propertyContributors.Length; i++)
        {
            aditionalDamage += unit.Properties.GetProperty(_propertyContributors[i]).CurrentValue * _contributionValue[i];
        }

        _baseDamage += Mathf.RoundToInt(aditionalDamage);

    }

    /// <summary>
    /// Given a target unit, calculates the final damage to be recived by the unit
    /// </summary>
    /// <param name="targetUnit">Target Unit to use to calculate final damage.</param>
    /// <returns>Damge to be taken by the character.</returns>
    public int FinalDamage(Unit targetUnit)
    {
        int FinalDamage = _baseDamage;

        if (!targetUnit.Properties)
            return 0;

        for (int i = 0; i < _mitagationValue.Length; i++)
        {
            UnitStat currentArmor = targetUnit.Properties.GetStat(_statsMitigators[i]);

            if (currentArmor)
            {
                FinalDamage -= FinalDamage * Mathf.RoundToInt(currentArmor.CurrentValue * _mitagationValue[i]);
            }
        }

        return FinalDamage;
    }

    /// <summary>
    /// Apllies the damage to a given Unit.
    /// </summary>
    /// <param name="targetUnit">Unit to apply damage</param>
    public void ApplyDamage(Unit targetUnit)
    {
        if (!targetUnit.Properties)
            return;

        int damageToApply = FinalDamage(targetUnit);

        for (int i = 0; i < _resourcesAffected.Length; i++)
        {
            UnitResource currentresource = targetUnit.Properties.GetResource(_resourcesAffected[i]);

            if (!currentresource)
                continue;

            int remainingDamage = damageToApply - currentresource.CurrentValue;
            currentresource.ChangeBase(-damageToApply);

            if (remainingDamage < 0)
            {
                break;
            }

            damageToApply = remainingDamage;
        }
    }

    /// <summary>
    /// Apllies the damage to a given Unit.
    /// </summary>
    /// <param name="targetUnit">Unit to apply damage</param>
    /// <param name="tick">delta time to use </param>
    public void ApplyDamageAsTimeRate(Unit targetUnit, float tick)
    {
        if (!targetUnit.Properties)
            return;

        int damageToApply = Mathf.RoundToInt(FinalDamage(targetUnit) * tick);

        for (int i = 0; i < _resourcesAffected.Length; i++)
        {
            UnitResource currentresource = targetUnit.Properties.GetResource(_resourcesAffected[i]);

            if (!currentresource)
                continue;

            int remainingDamage = damageToApply - currentresource.CurrentValue;
            currentresource.ChangeBase(-damageToApply);

            if (remainingDamage < 0)
            {
                break;
            }

            damageToApply = remainingDamage;
        }
    }
}
