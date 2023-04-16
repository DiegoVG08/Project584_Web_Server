using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerInventory.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    VehicleTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DealershipID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.VehicleTypeID);
                });

            migrationBuilder.CreateTable(
                name: "dealerShips",
                columns: table => new
                {
                    DealershipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    VehicleTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealerShips", x => x.DealershipID);
                    table.ForeignKey(
                        name: "FK_dealerShips_vehicles_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "vehicles",
                        principalColumn: "VehicleTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dealerShips_VehicleTypeID",
                table: "dealerShips",
                column: "VehicleTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dealerShips");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
