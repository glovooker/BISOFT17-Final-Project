namespace ProyectoDiseñoBackend.Iterador
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }
}
