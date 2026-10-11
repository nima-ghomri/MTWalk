namespace MTWalk.Serialization;

public interface ISerializer<T> where T : IFormat
{
    string Serialize(T report);
    T Deserialize(string content);
}
