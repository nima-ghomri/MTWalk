using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.Terminal;

/// <summary>
/// Represents the parameters for a test run in the terminal, including both terminal-specific parameters and walking parameters.
/// </summary>
/// <param name="TesterParameters">The terminal-specific parameters.</param>
/// <param name="WalkingParameters">The walking parameters.</param>
record WFOParameters(TesterParameters TesterParameters, WalkingParamters WalkingParameters);

/// <summary>
/// Represents the parameters required to configure and run a test in the terminal's Strategy tester.
/// </summary>
/// <param name="Expert">The expert advisor file path.</param>
/// <param name="Symbol">The trading symbol.</param>
/// <param name="TimeFrame">The time frame for the test.</param>
/// <param name="Start">The start date for the test.</param>
/// <param name="End">The end date for the test.</param>
/// <param name="Forward">The forward date for the test.</param>
/// <param name="ExecutionMode">The delays for the test.</param>
/// <param name="Model">The modelling approach for the test.</param>
/// <param name="Deposit">The initial deposit for the test.</param>
/// <param name="Currency">The currency for the test.</param>
/// <param name="Leverage">The leverage for the test.</param>
record TesterParameters(
    string Expert,
    string Symbol,
    TimeFrame TimeFrame,
    DateTime Start,
    DateTime End,
    ForwardMode ForwardMode,
    DateTime? ForwardDate,
    ExecutionMode ExecutionMode,
    Model Model,
    double Deposit,
    string Currency,
    int Leverage,
    Optimization Optimization,
    OptimizationCriterion OptimizationCriterion);

/// <summary>
/// Represents the types of delays that can be used in the test.
/// </summary>
enum ExecutionMode
{
    ZeroDelay,
    RandomDelay,
    CustomDelay,
}

/// <summary>
/// Represents the different modelling approaches that can be used in the test.
/// </summary>
enum Model
{
    EveryTick,
    EveryTickBasedOnRealTicks,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
}

enum Optimization
{
    Disabled,
    SlowCompleteAlgorithm,
    FastGeneticAlgorithm,
    AllMarketWatchSymbols,
}


record WalkingParamters(
    WindowType WindowType,
    int ForwardSizeInDays,
    int? OptimizationSizeInDays,
    DateTime? OptimizationAnchorDate);

enum OptimizationCriterion
{
    BalanceMax,
}

enum ForwardMode
{
    No,
    Half,
    Custom,
}

enum TimeFrame
{
    M1,
    M5,
    M15,
    M30,
    H1,
    H4,
    D1,
    W1,
    MN1
}