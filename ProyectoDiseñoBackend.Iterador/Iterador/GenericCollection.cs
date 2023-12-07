namespace ProyectoDiseñoBackend.Iterador
{
    public class GenericCollection<T> : IIterableCollection<T>
    {
        private readonly IList<T> _collection;

        public GenericCollection(IList<T> collection)
        {
            _collection = collection;
        }

        public IIterator<T> CreateIterator()
        {
            return new GenericIterator<T>(_collection);
        }
    }

}
