using System.Threading;
using R3;

public static class ActiveStateOverrideR3Extension
{
    public static Observable<bool> ActiveState(this IActiveStateOverride aso, CancellationToken cancellationToken)
        => Observable.FromEvent<bool>(h => aso.OnActiveStateChanged += h,
                                      h => aso.OnActiveStateChanged -= h,
                                      cancellationToken);

    public static Observable<object> Handler(this IActiveStateOverride aso, CancellationToken cancellationToken)
        => Observable.FromEvent<object>(h => aso.OnHandlerChanged += h,
                                        h => aso.OnHandlerChanged -= h,
                                        cancellationToken);
}
