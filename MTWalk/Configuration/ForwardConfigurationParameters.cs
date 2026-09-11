namespace MTWalk.Configuration;

/// <inheritdoc cref="TestConfigurationParameters"/>
record ForwardConfigurationParameters(
    string Expert,
    string? ExpertParameters,
    string Symbol,
    TimeFrame Period,
    int Login,
    double Deposit,
    int Leverage,
    Model Model,
    ExecutionMode ExecutionMode,
    DateTime FromDate,
    DateTime ToDate,
    string? Report) : TestConfigurationParameters(
        Expert: Expert,
        ExpertParameters: ExpertParameters,
        Symbol: Symbol,
        Period: Period,
        Login: Login,
        Deposit: Deposit,
        Leverage: Leverage,
        Model: Model,
        ExecutionMode: ExecutionMode,
        Optimization: Optimization.Disabled,
        OptimizationCriterion: default,
        FromDate: FromDate,
        ToDate: ToDate,
        ForwardMode: default,
        ForwardDate: default,
        Report: Report,
        ReplaceReport: true,
        ShutdownTerminal: true);