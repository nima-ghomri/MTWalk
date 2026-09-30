namespace MTWalk.Terminal;

/// <summary>
/// Represents the parameters required to configure and run a test in the terminal's Strategy tester.
/// </summary>
/// <param name="Expert">The expert advisor file path.</param>
/// <param name="Symbol">The trading symbol.</param>
/// <param name="TimeFrame">The time frame for the test.</param>
/// <param name="Start">The start date for the test.</param>
/// <param name="End">The end date for the test.</param>
/// <param name="ForwardMode">The forward mode for the test.</param>
/// <param name="ForwardDate">The forward date for the test.</param>
/// <param name="ExecutionMode">The delays for the test.</param>
/// <param name="Model">The modelling approach for the test.</param>
/// <param name="Deposit">The initial deposit for the test.</param>
/// <param name="Currency">The currency for the test.</param>
/// <param name="Leverage">The leverage for the test.</param>
public record TesterRequest(string Expert,
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
