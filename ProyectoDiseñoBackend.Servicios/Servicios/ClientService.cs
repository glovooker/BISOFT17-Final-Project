using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;


namespace ProyectoDiseñoBackend.Servicios
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clientsCollection;

        public ClientService(MongoDBInstance mongoDBInstance) // Adjust MongoDBInstance as needed
        {
            _clientsCollection = mongoDBInstance.GetDatabase().GetCollection<Client>("Client");
        }

        public async Task<List<Client>> GetAsync() =>
            await _clientsCollection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetAsync(string id) =>
            await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient) =>
            await _clientsCollection.InsertOneAsync(newClient);

        public async Task UpdateAsync(string id, Client updatedClient) =>
            await _clientsCollection.ReplaceOneAsync(x => x.Id == id, updatedClient);

        public async Task RemoveAsync(string id) =>
            await _clientsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
