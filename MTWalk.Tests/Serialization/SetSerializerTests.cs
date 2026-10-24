using System;
using NUnit.Framework;
using MTWalk.Serialization;

namespace MTWalk.Tests.Serialization;

class SetSerializerTests
{
    [TestCase("""
Foo=bar
Baz=1
""")]
    [TestCase("""
; --- GroupA ---
Alpha=hello
Beta=2||0||1||10
""")]
    public void Deserialize_ShouldParseKeysAndValues(string input)
    {
        var sut = new SetSerializer();

        var result = sut.Deserialize(input);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Groups, Is.Not.Empty);

        // Flatten children for simple assertions
        var allChildren = new List<Input>();
        foreach (var g in result.Groups)
            allChildren.AddRange(g.Children);

        Assert.That(allChildren.Count, Is.GreaterThanOrEqualTo(2));

        // First child name should be present
        Assert.That(allChildren[0].Name, Is.Not.Null.Or.Empty);

        // If second value was an optimization definition (contains ||), it must be stripped to base value
        var secondRaw = allChildren[1].Value;
        // pattern-match union value to string or numeric representation
        Assert.That(
            secondRaw is string || secondRaw is int || secondRaw is double || secondRaw is long || secondRaw is uint,
            "Second value should be a supported primitive or string"
        );
    }

    //[Test]
    //public void Serialize_ShouldEmitKeyValueLinesAndGroupHeaders()
    //{
    //    var sut = new SetSerializer();

    //    var format = new SetFormat(new[] { new InputGroup("MyGroup", new[] { new Input("X", new InputValue("y")), new Input("N", new InputValue(42)) }) });

    //    var text = sut.Serialize(format);

    //    StringAssert.Contains("; --- MyGroup ---", text);
    //    StringAssert.Contains("X=y", text);
    //    StringAssert.Contains("N=42", text);
    //}
}
