using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingService.Migrations
{
    /// <inheritdoc />
    public partial class addBidTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BidderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BidderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BidAmmount = table.Column<double>(type: "float", nullable: false),
                    BidDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bid");
        }
    }
}
