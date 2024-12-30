using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Venodr_u_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Business_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Business_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank_Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requested_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Admin_Approved = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
