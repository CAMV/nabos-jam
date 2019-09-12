using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class <c> USkills </c> handles the skills of a unit.
/// </summary>
[RequireComponent(typeof(Unit))]
public class USkills : MonoBehaviour 
{
    [SerializeField]
    private List<Skill> _mySkills = new List<Skill>();

    void Awake()
    {
        for (int i = 0; i < _mySkills.Count; i++)
        {
            _mySkills[i] = ScriptableObject.Instantiate(_mySkills[i]);
            _mySkills[i].Initialize(GetComponent<Unit>());
        }
    }

    /// <summary>
    /// Get number of Skills the unit has.
    /// </summary>
    public int Size {
        get {
            return _mySkills.Count;
        }
    }

    /// <summary>
    /// Indexer to access skills
    /// </summary>
    public Skill this[int index] {
        get {
            return _mySkills[index];
        }
    }

    /// <summary>
    /// Checks if a given skill is in the skill list of the unit by comparing the names.
    /// </summary>
    /// <param name="skillToCheck">Skill to check if it's present</param>
    /// <returns>If the skill if present, returns true.  Otherwise, false.</returns>
    private bool HasSkill(Skill skillToCheck)
    {
        foreach (var skill in _mySkills)
        {
            if (skill.name == skillToCheck.name)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Add an instance of a given skill to the Unit.
    /// </summary>
    /// <param name="skillPreset">Sill to be instantiated.</param>
    public void AddSkill(Skill skillPreset)
    {
        if (!HasSkill(skillPreset))
        {
            Skill newSkil = ScriptableObject.Instantiate(skillPreset);
            newSkil.Initialize(GetComponent<Unit>()); 
            _mySkills.Add(newSkil);
        }


    }
    
} 