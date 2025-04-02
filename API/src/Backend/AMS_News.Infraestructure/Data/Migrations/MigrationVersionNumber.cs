namespace AMS_News.Infraestructure.Data.Migrations;

public abstract class MigrationVersionNumber
{
    public const int CreateUserTable = 1;
    public const int CreateTableNews = 2;
    public const int UpdateTableNewsAddingImageIdentifier = 3;
}