namespace MTWalk.Reporting;

record OptimizationReport(OptimizationPass[] Passes) : IReport;
