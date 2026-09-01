using MTWalk.Configuration;
using MTWalk.MetaQuotes;
using MTWalk.Terminal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.Tests;

class ConfigurationTests
{
    [Test]
    public void ConfigurationBuilder_TestParameters_CorrectResult()
    {
        var builder = new ConfigurationBuilder();
        var parameters = new TestConfigurationParameters(
                 Expert: @"Examples\MACD\MACD Sample.ex5",
                 ExpertParameters: default,
                 Symbol: "EURUSD",
                 Period: TimeFrame.H1,
                 Login: default,
                 Deposit: 1000,
                 Leverage: 100,
                 Model: Model.EveryTick,
                 ExecutionMode: ExecutionMode.ZeroDelay,
                 Optimization: Optimization.Disabled,
                 OptimizationCriterion: OptimizationCriterion.BalanceMax,
                 FromDate: new DateTime(2025, 1, 1),
                 ToDate: new DateTime(2025, 3, 1),
                 ForwardMode: ForwardMode.Custom,
                 ForwardDate: default,
                 Report: default,
                 ReplaceReport: false,
                 ShutdownTerminal: false
             );
        var configurationFile = builder.AddParameters(parameters).Build();
    }
}
