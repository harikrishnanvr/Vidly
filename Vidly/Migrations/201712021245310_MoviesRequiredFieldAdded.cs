namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesRequiredFieldAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "MovieReleaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "MovieReleaseDate", c => c.DateTime());
        }
    }
}
