using FluentMigrator;

namespace AMS_News.Infraestructure.Data.Migrations.Versions;

[Migration(MigrationVersionNumber.CreateTableNews,"Create table News and Likes")]
public class CreateTableNews0000002 : BaseMigration
{
    public override void Up()
    {
        CreateTable("News").WithColumn("Title").AsString().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("Content").AsString().NotNullable()
            .WithColumn("Author").AsString().NotNullable()
            .WithColumn("Introduction").AsString().NotNullable()
            .WithColumn("CustomerId").AsInt64().NotNullable().ForeignKey("FK_Likes_Customers_Id", "Customers", "Id");

        CreateTable("Likes")
            .WithColumn("NewsId").AsInt64().NotNullable().ForeignKey("FK_Likes_News_Id", "News", "Id")
            .WithColumn("CustomerId").AsInt64().NotNullable().ForeignKey("FK_Likes_Customers_Id", "Customers", "Id");
        
        CreateTable("Topics").WithColumn("Description").AsString().NotNullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("ImageIdentifier").AsString().Nullable()
            .WithColumn("NewsId").AsInt64().NotNullable().ForeignKey("FK_Topics_News_Id", "News", "Id");

    }
}