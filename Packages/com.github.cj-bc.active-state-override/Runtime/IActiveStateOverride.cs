using UnityEngine;

public interface IActiveStateOverride<T>
{
    public OverrideHandler? Override(T overridenBy);
}
