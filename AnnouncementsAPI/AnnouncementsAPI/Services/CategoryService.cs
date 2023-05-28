using AnnouncementsAPI.Models;
using AnnouncementsAPI.Services.Abstractions;
using AnnouncementsAPI.Settings;
using MongoDB.Driver;

namespace AnnouncementsAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categories = database.GetCollection<Category>(settings.CategoriesCollectionName);
        }

        public async Task<bool> Create(Category entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            await _categories.InsertOneAsync(entity);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _categories.DeleteOneAsync(category => category.Id == id);
            if (!result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Category> Get(Guid id)
        {
            return (await _categories.FindAsync(category => category.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var resultsFromDb = await _categories.FindAsync(category => true);

            return resultsFromDb.ToList();
        }

        public async Task<bool> Update(Guid id, Category entity)
        {
            entity.Id = id;
            var result = await _categories.ReplaceOneAsync(category => category.Id == id, entity);
            if (!result.IsAcknowledged && result.ModifiedCount == 0)
            {
                await _categories.InsertOneAsync(entity);
                return false;
            }

            return true;
        }
    }
}
