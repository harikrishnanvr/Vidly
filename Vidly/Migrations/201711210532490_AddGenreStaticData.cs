namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreStaticData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into genres (id,GenreType) values(1,'Comedy')");
            Sql("Insert into genres (id,GenreType) values(2,'Action')");
            Sql("Insert into genres (id,GenreType) values(3,'Family')");
            Sql("Insert into genres (id,GenreType) values(4,'Romance')");
            Sql("Insert into genres (id,GenreType) values(5,'Drama')");
        }
        
        public override void Down()
        {
        }
    }
}
