using UnityEngine;

public interface IActiveStateOverride
{
    public OverrideHandler? Override(object overridenBy);
}
