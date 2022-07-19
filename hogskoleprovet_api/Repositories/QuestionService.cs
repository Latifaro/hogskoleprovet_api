using hogskoleprovet_api.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace hogskoleprovet_api.Repositories
{
    public class QuestionService
    {
        private readonly IMongoCollection<Questions> _questionCollection;

        public QuestionService(
            IOptions<QuestionStoreDatabaseSettings> questionStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                questionStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                questionStoreDatabaseSettings.Value.DatabaseName);

            _questionCollection = mongoDatabase.GetCollection<Questions>(
                questionStoreDatabaseSettings.Value.QuestionsCollectionName);
        }

        public async Task<List<Questions>> GetAsync() =>
            await _questionCollection.Find(_ => true).ToListAsync();

        public async Task<Questions?> GetAsync(string id) =>
            await _questionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Questions newQuestion) =>
            await _questionCollection.InsertOneAsync(newQuestion);

        public async Task UpdateAsync(string id, Questions updatedBook) =>
            await _questionCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _questionCollection.DeleteOneAsync(x => x.Id == id);
    
}
}
