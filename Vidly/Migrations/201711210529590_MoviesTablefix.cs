namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesTablefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "MovieReleaseDate", c => c.DateTime());
            AddColumn("dbo.Movies", "DateAddedTOCollection", c => c.DateTime());
            AddColumn("dbo.Movies", "NumberInStocks", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 256));
            //DropColumn("dbo.Movies", "ReleaseDate");
            //DropColumn("dbo.Movies", "DateAdded");
            //DropColumn("dbo.Movies", "NumberInStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime());
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime());
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movies", "NumberInStocks");
            DropColumn("dbo.Movies", "DateAddedTOCollection");
            DropColumn("dbo.Movies", "MovieReleaseDate");
        }
    }
}
