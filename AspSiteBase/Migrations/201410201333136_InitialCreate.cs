namespace AspSiteBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contactus",
                c => new
                    {
                        ContactusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        Msg = c.String(),
                        Ip = c.String(),
                    })
                .PrimaryKey(t => t.ContactusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contactus");
        }
    }
}
