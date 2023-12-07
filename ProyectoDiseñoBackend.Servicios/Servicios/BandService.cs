using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;

namespace ProyectoDiseñoBackend.Servicios
{
    public class BandService
    {
        private readonly IMongoCollection<Band> _bandsCollection;

        public BandService(MongoDBInstance mongoDBInstance)
        {
            _bandsCollection = mongoDBInstance.GetDatabase().GetCollection<Band>("Band");
        }

        public async Task<List<Band>> GetAsync() =>
            await _bandsCollection.Find(_ => true).ToListAsync();

        public async Task<Band?> GetAsync(string id) =>
            await _bandsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Band newBand) =>
            await _bandsCollection.InsertOneAsync(newBand);

        public async Task UpdateAsync(string id, Band updatedBand) =>
            await _bandsCollection.ReplaceOneAsync(x => x.Id == id, updatedBand);

        public async Task RemoveAsync(string id) =>
            await _bandsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
