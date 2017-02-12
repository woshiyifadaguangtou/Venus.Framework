namespace Venus.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_cbdetailentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CBDetail",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TaskId = c.Int(nullable: false),
                        xh = c.String(maxLength: 4),
                        hh = c.String(maxLength: 8),
                        hm = c.String(maxLength: 20),
                        bh = c.String(maxLength: 6),
                        dh = c.String(maxLength: 20),
                        dz = c.String(maxLength: 40),
                        byds = c.Int(nullable: false),
                        bcsl = c.Int(nullable: false),
                        scds = c.Int(nullable: false),
                        bkdm = c.Int(nullable: false),
                        zje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        lq = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dj = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cbrq = c.DateTime(nullable: false),
                        bs = c.String(),
                        hbsl = c.Int(nullable: false),
                        tbsl = c.Int(nullable: false),
                        jmsl = c.Int(nullable: false),
                        fsl = c.Int(nullable: false),
                        skfsname = c.String(),
                        pwf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ljf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        jyf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        glf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        zkf = c.Decimal(nullable: false, precision: 18, scale: 2),
                        jtsl1 = c.Int(nullable: false),
                        jtsl2 = c.Int(nullable: false),
                        jtsl3 = c.Int(nullable: false),
                        jtsf1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        jtsf2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        jtsf3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        bz = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CBDetail");
        }
    }
}
