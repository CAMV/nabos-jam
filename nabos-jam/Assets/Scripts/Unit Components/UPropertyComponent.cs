using UnityEngine;
using System.Collections.Generic;


public class UPropertyComponent : MonoBehaviour 
{
    [SerializeField]
    private List<UnitAttribute> _attributes = new List<UnitAttribute>();

    [SerializeField]
    private List<UnitStat> _stats = new List<UnitStat>();

    [SerializeField]
    private List<UnitResource> _resoursces = new List<UnitResource>();

    // Initial values

    [SerializeField]
    private List<int> _initialAttributes = new List<int>();

    [SerializeField]
    private List<int> _initialStats = new List<int>();

    [SerializeField]
    private List<int> _initialMaxResources = new List<int>();
    
    [SerializeField]
    private List<bool> _isResourceFull = new List<bool>();


    //////////////// METHODS ////////////////

    // Initialization methods

    /// <summary>
    /// Initilize the component
    /// </summary>
    public void Initialize()
    {
        for (int i = 0; i < _attributes.Count; i++)
        {
            UnitAttribute attribute = _attributes[i];
            UnitAttribute propertyToAdd = ScriptableObject.Instantiate(attribute);
            propertyToAdd.Initialize(GetComponentInParent<Unit>(), _initialAttributes[i]); 
            ReplaceElement(_attributes, propertyToAdd, attribute);
        }

        for (int i = 0; i < _stats.Count; i++)
        {
            UnitStat stat = _stats[i];
            UnitStat statToAdd = ScriptableObject.Instantiate(stat);
            statToAdd.Initialize(GetComponentInParent<Unit>(), _initialStats[i]);
            ReplaceElement(_stats, statToAdd, stat);
        }

        for (int i = 0; i < _resoursces.Count; i++)
        {
            UnitResource resource = _resoursces[i];
            UnitResource resourceToAdd = ScriptableObject.Instantiate(resource);
            resourceToAdd.Initialize(GetComponentInParent<Unit>(), _initialStats[i]);
            ReplaceElement(_resoursces, resourceToAdd, resource);
        }
    }

    /// <summary>
    /// Adds a given Attribute to the unit with an initial base value. 
    /// It would not update any already added stat that depends on the attribute.
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="baseValue"></param>
    public void AddAttribute(UnitAttribute attribute, int baseValue = 0)
    {
        if (!GetAttribute(attribute))
        {
            UnitAttribute propertyToAdd = ScriptableObject.Instantiate(attribute);
            propertyToAdd.Initialize(GetComponentInParent<Unit>(), baseValue); 
            _attributes.Add(propertyToAdd);
        }
    }

    /// <summary>
    /// Instance a given stat with the given base value.
    /// </summary>
    /// <param name="stat">Stas to instance.</param>
    /// <param name="baseValue">bas balue to initialize the stat with.</param>
    public void AddStat(UnitStat stat, int baseValue = 0)
    {
        if (!GetStat(stat))
        {
            UnitStat propertyToAdd = ScriptableObject.Instantiate(stat);
            propertyToAdd.Initialize(GetComponentInParent<Unit>(), baseValue); 
            _stats.Add(propertyToAdd);
        }
    }


    /// <summary>
    /// Instance a given resource with the given max value and indicates if it starts full.
    /// </summary>
    /// <param name="resource">Resource to instance.<param>
    /// <param name="masValue">MaxValue to initialize the resource.</param>
    /// <param name="isFull">Is the resource starts full or is it empty.</param>
    public void AddResource(UnitResource resource, int maxValue, bool isFull = true)
    {
        if (!GetResource(resource))
        {
            UnitResource propertyToAdd = ScriptableObject.Instantiate(resource);
            propertyToAdd.Initialize(GetComponentInParent<Unit>(), maxValue); 
            if (!isFull)
                propertyToAdd.ChangePerecentage(-1);
            _resoursces.Add(propertyToAdd);
        }
    }

    /// <summary>
    /// Replaces an element in a list with a new element. 
    /// If the element to replace wasn't found, the new element
    /// is added at the end.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="ElementToAdd"></param>
    /// <param name="ElementToReplace"></param>
    /// <typeparam name="T"></typeparam>
    public void ReplaceElement<T>(List<T> list, T elementToAdd, T elementToReplace)
    {
        int index = list.IndexOf(elementToReplace);
        if (index != -1)
        {
            list.RemoveAt(index);
            list.Insert(index, elementToAdd);
        }
        else
        {
            list.Add(elementToAdd);
        }
    }

    // Finders

    /// <summary>
    /// Given a string of a property return given propery intance in tue unit.
    /// </summary>
    /// <param name="property">Property to find.</param>
    /// <returns>Instance of the property in the unit if found, otherwise, null.</returns>
    public UnitProperty GetProperty(UnitProperty property)
    {
        UnitProperty propertyToFind = GetAttribute(property.GUIData.Name);

        if (propertyToFind)
            return propertyToFind;

        propertyToFind = GetStat(property.GUIData.Name);

        if (propertyToFind)
            return propertyToFind;

        propertyToFind = GetResource(property.GUIData.Name);

        if (propertyToFind)
            return propertyToFind;

        return null;
    }

    /// <summary>
    /// Given a name, look if the unit has an attribute with tha name and returns it. Otherwise, return null.
    /// </summary>
    /// <param name="name">Name o the attribute to look for.</param>
    /// <returns>Attribute if found, otherwise null.</returns>
    public UnitAttribute GetAttribute(string name)
    {
        for (int i = 0; i < _attributes.Count; i++)
        {
            if (_attributes[i].GUIData.Name == name)
                return _attributes[i];
        }

        return null;
    }

    /// <summary>
    /// Given an attribute, look if the unit has an instance of it.
    /// </summary>
    /// <param name="attribute">Attribute to look for.</param>
    /// <returns>Attribute if found, otherwise null.</returns>
    public UnitAttribute GetAttribute(UnitAttribute attribute)
    {
        return GetAttribute(attribute.GUIData.Name);
    }

    /// <summary>
    /// Given a name, look if the unit has a Resource with that name and returns it. Otherwise, return null.
    /// </summary>
    /// <param name="name">Name of the Resource to look for.</param>
    /// <returns>Resource if found, otherwise null.</returns>
    public UnitResource GetResource(string name)
    {
        for (int i = 0; i < _resoursces.Count; i++)
        {
            if (_resoursces[i].GUIData.Name == name)
                return _resoursces[i];
        }

        return null;
    }

    /// <summary>
    /// Given a Resource, look if the unit has an instance of it.
    /// </summary>
    /// <param name="attribute">Attribute to look for.</param>
    /// <returns>Resource if found, otherwise null.</returns>
    public UnitResource GetResource(UnitResource resource)
    {
        return GetResource(resource.GUIData.Name);
    }

    /// <summary>
    /// Given a name, look if the unit has a Stat with that name and returns it. Otherwise, return null.
    /// </summary>
    /// <param name="name">Name of the Stat to look for.</param>
    /// <returns>Stat if found, otherwise null.</returns>
    public UnitStat GetStat(string name)
    {
        for (int i = 0; i < _stats.Count; i++)
        {
            if (_stats[i].GUIData.Name == name)
                return _stats[i];
        }

        return null;
    }

    /// <summary>
    /// Given a stat, look if the unit has an instance of it.
    /// </summary>
    /// <param name="stat">Stat to look for.</param>
    /// <returns>Stat if found, otherwise null.</returns>
    public UnitStat GetStat(UnitStat stat)
    {
        return GetStat(stat.GUIData.Name);
    }

}