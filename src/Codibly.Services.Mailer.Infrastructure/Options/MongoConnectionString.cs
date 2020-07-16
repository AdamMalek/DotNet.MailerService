namespace Codibly.Services.Mailer.Infrastructure.Options
{
    public class MongoConnectionString
    {
        public MongoConnectionString(string mongoDbConnectionString)
        {
            MongoDbConnectionString = mongoDbConnectionString;
        }

        public string MongoDbConnectionString { get; }
    }
}