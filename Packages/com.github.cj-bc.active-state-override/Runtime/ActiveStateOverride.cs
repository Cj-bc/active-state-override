using UnityEngine;

#nullable enable

public class ActiveStateOverride<T> : MonoBehaviour, IActiveStateOverride<T>
{

    private T OverridenBy = default;
    private bool _isOverriden = false;

    public OverrideHandler? Override(T overridenBy)
    {
        if (_isOverriden) return null;
        OverridenBy = overridenBy;
        _isOverriden = true;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = default(T);
        _isOverriden = false;
    }

    private void Activate() => gameObject.SetActive(true);
    private void Deactivate() => gameObject.SetActive(false);

}
