using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class <c> USkills </c> handles the skills of a unit.
/// </summary>
[RequireComponent(typeof(Unit))]
public class USkills : MonoBehaviour 
{
    [SerializeField]
    private List<Skill> _mySkills = new List<Skill>();

    private Unit _myUnit;

    void Awake()
    {
        _myUnit = GetComponent<Unit>();

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


    /// <summary>
    /// Executes a coroutine that gets a vector through user input for an skill.
    /// </summary>
    /// <param name="skill">Skill to be send the inpu.t</param>
    /// <param name="type">Type of Gizmo required.</param>
    public void ExecuteSkillInput(Skill skill, VectorSkillType type)
    {
        
        StartCoroutine(ExecuteSkillInputCO(skill, type));
    }

    /// <summary>
    /// coroutine that gets a vector through user input for an skill.
    /// </summary>
    /// <param name="skill">Skill to be send the inpu.t</param>
    /// <param name="type">Type of Gizmo required.</param>
    public IEnumerator ExecuteSkillInputCO(Skill skill, VectorSkillType type)
    {
        bool cancel = false;
        Vector3 hitpoint = Vector3.zero;

        do
        {
            float rayPoint;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane newPlane = new Plane(Vector3.up, transform.position);

            // Get raycast point
            if (newPlane.Raycast(ray, out rayPoint))
                hitpoint = ray.GetPoint(rayPoint);

            // Set the gizmo
            switch (type)
            {
                case VectorSkillType.Vector:
                    if (_myUnit.VectorGizmo)
                        _myUnit.VectorGizmo.Show(hitpoint, ((VectorSkill) skill).Range);
                break;
                case VectorSkillType.Direction:
                    if (_myUnit.VectorGizmo)
                        _myUnit.VectorGizmo.Show(hitpoint, 1);
                break;
                case VectorSkillType.Area:
                    if (_myUnit.AreaGizmo)
                        _myUnit.AreaGizmo.Show(hitpoint, 1);
                break;
            }

            cancel = Input.GetButtonDown("Cancel");
            yield return new WaitForEndOfFrame();

        } while (!Input.GetButton("Fire1") || cancel);

        if (!cancel)
        {
            skill.Execute(true);
        }

        if (_myUnit.AreaGizmo)
            _myUnit.AreaGizmo.Hide();

        if (_myUnit.VectorGizmo)
            _myUnit.VectorGizmo.Hide();
    }
    
} 