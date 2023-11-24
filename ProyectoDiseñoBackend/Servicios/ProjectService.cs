﻿using MongoDB.Driver;
using ProyectoDiseñoBackend.Database;
using ProyectoDiseñoBackend.Modelos;

namespace ProyectoDiseñoBackend.Servicios
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _projectsCollection;

        public ProjectService(MongoDBInstance mongoDBInstance) // Adjust MongoDBInstance as needed
        {
            _projectsCollection = mongoDBInstance.GetDatabase().GetCollection<Project>("Project");
        }

        public async Task<List<Project>> GetAsync() =>
            await _projectsCollection.Find(_ => true).ToListAsync();

        public async Task<Project?> GetAsync(string id) =>
            await _projectsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Project newProject) =>
            await _projectsCollection.InsertOneAsync(newProject);

        public async Task UpdateAsync(string id, Project updatedProject) =>
            await _projectsCollection.ReplaceOneAsync(x => x.Id == id, updatedProject);

        public async Task RemoveAsync(string id) =>
            await _projectsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
