using System;
using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour, IActiveStateOverride
{

    public object OverridenBy { get; private set; } = default;
    public bool IsOverriden { get; private set; } = false;

    public event Action<bool> OnActiveStateChanged;
    public event Action<object> OnHandlerChanged;

    public OverrideHandler? Override(object overridenBy)
    {
        if (IsOverriden) return null;
        OverridenBy = overridenBy;
        OnHandlerChanged?.Invoke(OverridenBy);
        IsOverriden = true;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
        OnHandlerChanged?.Invoke(null);
        IsOverriden = false;
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
