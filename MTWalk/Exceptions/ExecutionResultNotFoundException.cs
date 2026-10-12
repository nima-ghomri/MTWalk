namespace MTWalk.Exceptions;

public class ExecutionResultNotFoundException(FileNotFoundException innerException)
    : Exception($"Execution result not found: {innerException.FileName}", innerException);
public class NoOptimizationParametersSelectedException()
    : Exception("No optimization parameters selected.");