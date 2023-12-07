namespace ProyectoDiseñoBackend.Iterador
{
    public class GenericIterator<T> : IIterator<T>
    {
        private readonly IList<T> _collection;
        private int _position = 0;

        public GenericIterator(IList<T> collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _position < _collection.Count;
        }

        public T Next()
        {
            return _collection[_position++];
        }
    }

}
