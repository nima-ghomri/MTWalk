namespace MTWalk.MetaQuotes;

public record InputSet(InputGroup[] Groups) : IReport
{
    public static InputSet Create(IEnumerable<Input>? inputs)
    {
        InputGroup group = new(null, inputs?.ToArray() ?? []);
        InputGroup[] groups = [group];
        InputSet inputSet = new(groups);
        return inputSet;
    }
}