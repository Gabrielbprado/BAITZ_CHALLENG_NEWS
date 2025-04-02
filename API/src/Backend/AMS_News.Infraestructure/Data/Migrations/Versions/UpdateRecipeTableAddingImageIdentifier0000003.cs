using FluentMigrator;

namespace AMS_News.Infraestructure.Data.Migrations.Versions;

[Migration(MigrationVersionNumber.UpdateTableNewsAddingImageIdentifier,"Update table news adding image identifier")]
public class UpdateRecipeTableAddingImageIdentifier0000003 : BaseMigration
{
    public override void Up()
    {
        Alter.Table("News")
            .AddColumn("ImageIdentifier").AsString().Nullable();
    }
}