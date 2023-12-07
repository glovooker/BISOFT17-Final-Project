namespace ProyectoDiseñoBackend.Iterador
{
    public interface IIterableCollection<T>
    {
        IIterator<T> CreateIterator();
    }

}
