namespace AnnouncementsAPI.Settings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CategoriesCollectionName { get; set; }

        public string AnnouncementsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
