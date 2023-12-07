using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;
namespace ProyectoDiseñoBackend.Servicios
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employeesCollection;

        public EmployeeService(MongoDBInstance mongoDBInstance) // Adjust MongoDBInstance as needed
        {
            _employeesCollection = mongoDBInstance.GetDatabase().GetCollection<Employee>("Employee");
        }

        public async Task<List<Employee>> GetAsync() =>
            await _employeesCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(string id) =>
            await _employeesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee newEmployee) =>
            await _employeesCollection.InsertOneAsync(newEmployee);

        public async Task UpdateAsync(string id, Employee updatedEmployee) =>
            await _employeesCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

        public async Task RemoveAsync(string id) =>
            await _employeesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
