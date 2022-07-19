namespace hogskoleprovet_api.Model
{
    public class QuestionStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string QuestionsCollectionName { get; set; } = null!;
    }
}
