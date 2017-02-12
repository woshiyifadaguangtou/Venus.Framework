namespace Venus.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MsgRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserNO = c.String(),
                        UserAddress = c.String(),
                        PhoneNO = c.String(),
                        DueMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MsgState = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        SendIndexID = c.String(),
                        SendTime = c.DateTime(),
                        MessageType = c.String(),
                        ReceiveState = c.String(),
                        FeeCounts = c.Int(),
                        TaskID = c.Int(nullable: false),
                        MsgContent = c.String(),
                        RemainMoney = c.Decimal(precision: 18, scale: 2),
                        pwf = c.Decimal(precision: 18, scale: 2),
                        ljf = c.Decimal(precision: 18, scale: 2),
                        bysf = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MsgTemp",
                c => new
                    {
                        TempId = c.String(nullable: false, maxLength: 128),
                        TempName = c.String(),
                        TempType = c.String(),
                        TempContent = c.String(),
                    })
                .PrimaryKey(t => t.TempId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MASSeting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        imIP = c.String(),
                        LoginName = c.String(),
                        LoginPWD = c.String(),
                        ApiCode = c.String(),
                        DbName = c.String(),
                        Port = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MessageSystem",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskType = c.String(),
                        MsgCount = c.Int(nullable: false),
                        TaskState = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        StarTime = c.DateTime(nullable: false),
                        StopTime = c.DateTime(nullable: false),
                        MsgContext = c.String(),
                        MinMoney = c.Decimal(precision: 18, scale: 2),
                        MaxMoney = c.Decimal(precision: 18, scale: 2),
                        MinUserNo = c.String(),
                        MaxUserNo = c.String(),
                    })
                .PrimaryKey(t => t.TaskID);
            
            CreateTable(
                "dbo.CBTask",
                c => new
                    {
                        TaskId = c.String(nullable: false, maxLength: 128),
                        xh = c.String(),
                        fdate = c.String(),
                        cbrq = c.DateTime(nullable: false),
                        cbydm = c.String(),
                        state = c.Int(nullable: false),
                        detail_count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CBTask");
            DropTable("dbo.MessageSystem");
            DropTable("dbo.MASSeting");
            DropTable("dbo.Test");
            DropTable("dbo.MsgTemp");
            DropTable("dbo.MsgRecord");
        }
    }
}
