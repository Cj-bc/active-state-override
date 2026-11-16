using UnityEngine;

public interface IActiveStateOverride
{
    public OverrideHandler? Override(UnityEngine.Object overridenBy);
}
