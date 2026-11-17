using System;
using UnityEngine;

public interface IActiveStateOverride
{
    public object OverridenBy { get; }

    public event Action<bool> OnActiveStateChanged;
    public event Action<object> OnHandlerChanged;
    public OverrideHandler? Override(object overridenBy);
}
