using System;
using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour, IActiveStateOverride
{

    public object OverridenBy { get; private set; } = default;

    public event Action<bool> OnActiveStateChanged;
    public event Action<object> OnHandlerChanged;

    public OverrideHandler? Override(object overridenBy)
    {
        if (OverridenBy is not null) return null;
        OverridenBy = overridenBy;
        OnHandlerChanged?.Invoke(OverridenBy);
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
        OnHandlerChanged?.Invoke(null);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        OnActiveStateChanged?.Invoke(gameObject.activeSelf);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
        OnActiveStateChanged?.Invoke(gameObject.activeSelf);
    }

}
