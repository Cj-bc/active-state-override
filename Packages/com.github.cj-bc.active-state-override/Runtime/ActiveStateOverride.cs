using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour, IActiveStateOverride
{

    private UnityEngine.Object? OverridenBy = null;

    public OverrideHandler? Override(UnityEngine.Object overridenBy)
    {
        if (OverridenBy != null) return null;
        OverridenBy = overridenBy;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
    }

    private void Activate() => gameObject.SetActive(true);
    private void Deactivate() => gameObject.SetActive(false);

}
