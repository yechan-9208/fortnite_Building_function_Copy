using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour, IItem
{
    [SerializeField]
    ParticleSystem _mapIndicator;

    void Start()
    {
        _mapIndicator.Stop(true);

        Managers.Game.MapIndicator -= MapIndicator;
        Managers.Game.MapIndicator += MapIndicator;
    }

    public void Use(GameObject target)
    {
        Managers.Game.AddCollection();

        Managers.Game.MapIndicator -= MapIndicator;
        Managers.Resource.Destroy(gameObject);
    }

    void MapIndicator(bool state)
    {
        if (state)
            _mapIndicator.Play(true);
        else
            _mapIndicator.Stop(true);
    }
}
