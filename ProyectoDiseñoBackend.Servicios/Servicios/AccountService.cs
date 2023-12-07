using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;

namespace ProyectoDiseñoBackend.Servicios
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accountsCollection;

        public AccountService(MongoDBInstance mongoDBInstance) // Adjust MongoDBInstance as needed
        {
            _accountsCollection = mongoDBInstance.GetDatabase().GetCollection<Account>("Account");
        }

        public async Task<List<Account>> GetAsync() =>
            await _accountsCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAsync(string id) =>
            await _accountsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Account newAccount) =>
            await _accountsCollection.InsertOneAsync(newAccount);

        public async Task UpdateAsync(string id, Account updatedAccount) =>
            await _accountsCollection.ReplaceOneAsync(x => x.Id == id, updatedAccount);

        public async Task RemoveAsync(string id) =>
            await _accountsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
