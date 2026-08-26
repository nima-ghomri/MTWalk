namespace MTWalk.Terminal;

record WalkingParamters(
    WindowType WindowType,
    int ForwardSizeInDays,
    int? OptimizationSizeInDays,
    DateTime? OptimizationAnchorDate);
