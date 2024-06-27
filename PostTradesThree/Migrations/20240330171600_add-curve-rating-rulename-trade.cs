using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTradesThree.Migrations
{
    public partial class addcurveratingrulenametrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurvePoints",
                columns: table => new
                {
                    CurvePointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurveId = table.Column<byte>(type: "tinyint", nullable: true),
                    AsOfDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Term = table.Column<double>(type: "float", nullable: true),
                    CurvePointValue = table.Column<double>(type: "float", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurvePoints", x => x.CurvePointId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoodysRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SandPRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FitchRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderNumber = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "RuleNames",
                columns: table => new
                {
                    RuleNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Json = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Template = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SqlStr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SqlPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleNames", x => x.RuleNameId);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    TradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BuyQuantity = table.Column<double>(type: "float", nullable: true),
                    SellQuantity = table.Column<double>(type: "float", nullable: true),
                    BuyPrice = table.Column<double>(type: "float", nullable: true),
                    SellPrice = table.Column<double>(type: "float", nullable: true),
                    TradeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TradeSecurity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradeStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trader = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Benchmark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_Trades", x => x.TradeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurvePoints");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RuleNames");

            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
