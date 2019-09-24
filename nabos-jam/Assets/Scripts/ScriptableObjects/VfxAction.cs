using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that represent a preset of a vfx to be instantiated in a particular part o a unit.
/// </summary>
public class VfxAction {
    [SerializeField]
    private GameObject _effectGO = null;

    [SerializeField]
    private UnitPart[] _spawnPoints = new UnitPart[0];

    [SerializeField]
    private float _duration = 0;

    private List<GameObject> _instantiatedVfx;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Shows the vfx, instantianting all of the neede vfx.null If given a duration value greater than 0, it will self destruct in that time.
    /// </summary>
    /// <param name="unit">Unit to cast the </param>
    /// <param name="duration">Duration of the effect being active.</param>
    public void Show(Unit unit)
    {
        UAnimatorComponent unitReferences = unit.Animation;
        _instantiatedVfx = new List<GameObject>();

        if (!unitReferences)
            return;

        List<GameObject> effects = new List<GameObject>();

        foreach (var spawnPoint in _spawnPoints)
        {
            GameObject currentVfx = GameObject.Instantiate(_effectGO, unitReferences[spawnPoint]);
            currentVfx.transform.parent = unitReferences[spawnPoint];
            _instantiatedVfx.Add(currentVfx);

            if (currentVfx.GetComponent<ParticleSystem>())
                currentVfx.GetComponent<ParticleSystem>().Play(true);

            if (currentVfx.GetComponent<AudioSource>())
                currentVfx.GetComponent<AudioSource>().Play();


            if (_duration > 0)
                GameObject.Destroy(currentVfx, _duration);

        }
    }   

    /// <summary>
    /// Desttroy all the effects currently instantiated.
    /// </summary>
    public void Hide()
    {
        foreach (var vfx in _instantiatedVfx)
        {
            if (vfx.GetComponent<AudioSource>())
                    vfx.GetComponent<AudioSource>().Pause();   
        }

        foreach (var vfx in _instantiatedVfx)
        {
            GameObject.Destroy(vfx);
        }
    } 

}