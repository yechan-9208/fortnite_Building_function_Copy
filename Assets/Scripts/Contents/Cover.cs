using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _mapIndicator;
    [SerializeField]
    ParticleSystem _triggerIndicator;

    void Start()
    {
        _mapIndicator.Stop(true);
        _triggerIndicator.Stop(true);

        Managers.Game.MapIndicator -= MapIndicator;
        Managers.Game.MapIndicator += MapIndicator;
        Managers.Game.CoverIndicator -= TriggerIndicator;
        Managers.Game.CoverIndicator += TriggerIndicator;
    }

    void MapIndicator(bool state)
    {
        if (state)
            _mapIndicator.Play(true);
        else
            _mapIndicator.Stop(true);
    }

    void TriggerIndicator(bool state)
    {
        if (state)
            _triggerIndicator.Play(true);
        else
            _triggerIndicator.Stop(true);
    }
}
