using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.Tester;

record Test(TerminalParameters TerminalParameters, WFOParamters WFOParameters);

record TerminalParameters(
    string Expert,
    string Symbol,
    string TimeFrame,
    DateTime Start,
    DateTime End,
    DateTime Forward,
    Delays Delays,
    Modelling Modelling,
    double Deposit,
    string Currency,
    int Leverage
    );
enum Delays
{
    Zero,
    Random,
    Custom,
}
enum Modelling
{
    EveryTick,
    EveryTickBasedOnRealTicks,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
}
record WFOParamters();