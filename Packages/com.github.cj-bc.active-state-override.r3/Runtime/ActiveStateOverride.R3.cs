using System.Threading;
using R3;

public static class ActiveStateOverrideR3Extension
{
    public static ReadOnlyReactiveProperty<bool> ActiveState(this IActiveStateOverride aso, CancellationToken cancellationToken)
        => Observable.FromEvent<bool>(h => aso.OnActiveStateChanged += h,
                                      h => aso.OnActiveStateChanged -= h,
                                      cancellationToken)
        .ToReadOnlyReactiveProperty();

    public static ReadOnlyReactiveProperty<object> Handler(this IActiveStateOverride aso, CancellationToken cancellationToken)
        => Observable.FromEvent<object>(h => aso.OnHandlerChanged += h,
                                        h => aso.OnHandlerChanged -= h,
                                        cancellationToken)
        .ToReadOnlyReactiveProperty();
}
