using FluentMigrator;

namespace AMS_News.Infraestructure.Data.Migrations.Versions;

[Migration(MigrationVersionNumber.CreateUserTable,"Create table Customers")]
public class CreateTableCustomers0000001 : BaseMigration
{
    public override void Up()
    {
        CreateTable("Customers").WithColumn("Name").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("UserIdentifier").AsString().NotNullable();
    }
}