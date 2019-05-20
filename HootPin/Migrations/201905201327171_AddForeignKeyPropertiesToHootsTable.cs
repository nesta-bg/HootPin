namespace HootPin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToHootsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Hoots", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.Hoots", name: "Genre_Id", newName: "GenreId");
            RenameIndex(table: "dbo.Hoots", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Hoots", name: "IX_Genre_Id", newName: "IX_GenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Hoots", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameIndex(table: "dbo.Hoots", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Hoots", name: "GenreId", newName: "Genre_Id");
            RenameColumn(table: "dbo.Hoots", name: "ArtistId", newName: "Artist_Id");
        }
    }
}
