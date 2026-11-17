using System;
using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour, IActiveStateOverride
{

    private object OverridenBy = default;
    private bool _isOverriden = false;

    public event Action<bool> OnActiveStateChanged;
    public event Action<object> OnHandlerChanged;

    public OverrideHandler? Override(object overridenBy)
    {
        if (_isOverriden) return null;
        OverridenBy = overridenBy;
        OnHandlerChanged?.Invoke(OverridenBy);
        _isOverriden = true;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
        OnHandlerChanged?.Invoke(null);
        _isOverriden = false;
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
