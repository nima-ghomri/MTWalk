using MTWalk.Serialization;
using System.Text;

namespace MTWalk.Tests;

class SourceGeneration
{
    [Test]
    public void ExtractPrintableStrings_FromBinaryFile_CorrectlyExtracts()
    {
        // Arrange
        string filePath = @"C:\Users\nimag\AppData\Roaming\MetaQuotes\Terminal\D0E8209F77C8CF37AD8BF550E51FF075\MQL5\Experts\Examples\MACD\MACD Sample.mq5"; // Path to your .mq5 file
        int minLength = 4; // Minimum length of strings to extract
        // Act
        var extractedStrings = Mq5SourceReader.ReadInputs(filePath);
        Assert.That(extractedStrings, Is.Not.Empty);
    }
}
