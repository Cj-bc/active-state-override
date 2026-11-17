using System;

public class SpyActiveStateOverride : IActiveStateOverride
{
    public object OverridenBy { get; private set; } = default;
    public bool IsActivated { get; private set; } = false;

    public int Activated { get; private set; } = 0;
    public int Deactivated { get; private set; } = 0;
    public int Returned { get; private set; } = 0;

    public event Action<bool> OnActiveStateChanged;
    public event Action<object> OnHandlerChanged;

    public OverrideHandler? Override(object overridenBy)
    {
        if (OverridenBy is not null) return null;
        OverridenBy = overridenBy;
        return new OverrideHandler(activate, deactivate, stop);
    }

    public void ResetAllCounts()
    {
        Activated = 0;
        Deactivated = 0;
        Returned = 0;
    }

    private void activate() => Activated++;
    private void deactivate() => Deactivated++;
    private void stop() => Returned++;
}
