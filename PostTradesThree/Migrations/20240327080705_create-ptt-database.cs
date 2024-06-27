using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTradesThree.Migrations
{
    public partial class createpttdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BidType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BidQuantity = table.Column<double>(type: "float", nullable: true),
                    AskQuantity = table.Column<double>(type: "float", nullable: true),
                    BidAmount = table.Column<double>(type: "float", nullable: true),
                    AskAmount = table.Column<double>(type: "float", nullable: true),
                    Benchmark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BidListDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Commentary = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BidSecurity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BidStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trader = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Book = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevisionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DealType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceListId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Side = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
