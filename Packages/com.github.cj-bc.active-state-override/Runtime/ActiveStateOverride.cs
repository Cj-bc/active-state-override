using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour, IActiveStateOverride
{

    private object OverridenBy = default;
    private bool _isOverriden = false;

    public OverrideHandler? Override(object overridenBy)
    {
        if (_isOverriden) return null;
        OverridenBy = overridenBy;
        _isOverriden = true;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
        _isOverriden = false;
    }

    private void Activate() => gameObject.SetActive(true);
    private void Deactivate() => gameObject.SetActive(false);

}
