using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApp_Backend.DataBase.Migrations
{
    public partial class renamedateioncreationDateinTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "CreationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Transactions",
                newName: "Date");
        }
    }
}
