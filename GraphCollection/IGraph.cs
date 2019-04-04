namespace GraphCollection
{
    public interface IGraph<T>
    {
        bool AddNodes(T value);
        bool RemoveNodes(T value);
    }
}