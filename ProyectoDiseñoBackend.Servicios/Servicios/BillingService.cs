using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;

namespace ProyectoDiseñoBackend.Servicios
{
    public class BillingService
    {
        private readonly IMongoCollection<Billing> _billingsCollection;

        public BillingService(MongoDBInstance mongoDBInstance) // Adjust MongoDBInstance as needed
        {
            _billingsCollection = mongoDBInstance.GetDatabase().GetCollection<Billing>("Billing");
        }

        public async Task<List<Billing>> GetAsync() =>
            await _billingsCollection.Find(_ => true).ToListAsync();

        public async Task<Billing?> GetAsync(string id) =>
            await _billingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Billing newBilling) =>
            await _billingsCollection.InsertOneAsync(newBilling);

        public async Task UpdateAsync(string id, Billing updatedBilling) =>
            await _billingsCollection.ReplaceOneAsync(x => x.Id == id, updatedBilling);

        public async Task RemoveAsync(string id) =>
            await _billingsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
