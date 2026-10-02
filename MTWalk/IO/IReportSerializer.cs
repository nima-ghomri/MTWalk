namespace MTWalk.IO;

interface IReportSerializer<T> where T : IReport
{
    string Serialize(T report);
    T Deserialize(string content);
}
