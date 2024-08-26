using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopping_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblPayment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPayment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblRole",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblVerifiedUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Tell = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVerifiedUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblProduct",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblProduct_TblCategory",
                        column: x => x.CategoryId,
                        principalTable: "TblCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tell = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    isVerified = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblUser_TblRole",
                        column: x => x.RoleId,
                        principalTable: "TblRole",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TblImage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblImage", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblImage_TblProduct",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl Address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tbl Address_TblUser",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblCart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCart", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblCart_TblUser",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRating", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblRating_TblProduct",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblRating_TblUser",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TblCartItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCartItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblCartItem_TblCart",
                        column: x => x.CartId,
                        principalTable: "TblCart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblCartItem_TblProduct",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl Address_UserId",
                table: "Tbl Address",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblCart_UserId",
                table: "TblCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCartItem_ProductId",
                table: "TblCartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UC_CartId_ProductId",
                table: "TblCartItem",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblImage_ImgUrl",
                table: "TblImage",
                column: "ImgUrl");

            migrationBuilder.CreateIndex(
                name: "IX_TblImage_ProductId",
                table: "TblImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProduct_CategoryId",
                table: "TblProduct",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRating_ProductId",
                table: "TblRating",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRating_UserId",
                table: "TblRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_RoleId",
                table: "TblUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_Tell",
                table: "TblUser",
                column: "Tell");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl Address");

            migrationBuilder.DropTable(
                name: "TblCartItem");

            migrationBuilder.DropTable(
                name: "TblImage");

            migrationBuilder.DropTable(
                name: "TblPayment");

            migrationBuilder.DropTable(
                name: "TblRating");

            migrationBuilder.DropTable(
                name: "TblVerifiedUsers");

            migrationBuilder.DropTable(
                name: "TblCart");

            migrationBuilder.DropTable(
                name: "TblProduct");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblCategory");

            migrationBuilder.DropTable(
                name: "TblRole");
        }
    }
}
