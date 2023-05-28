namespace AnnouncementsAPI.Settings
{
    public interface IMongoDBSettings
    {
        string CategoriesCollectionName { get; set; }

        string AnnouncementsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
