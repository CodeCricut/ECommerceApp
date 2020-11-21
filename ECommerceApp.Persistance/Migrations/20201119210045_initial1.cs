using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ECommerceApp.Persistance.Migrations
{
	public partial class initial1 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Username = table.Column<string>(nullable: false),
					Password = table.Column<string>(nullable: false),
					Bio = table.Column<string>(nullable: false),
					Deleted = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ProductDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					HumanReadableId = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Description = table.Column<string>(nullable: false),
					UserId = table.Column<int>(nullable: false),
					SellerId = table.Column<int>(nullable: false),
					Brand = table.Column<string>(nullable: true),
					PricePerUnit = table.Column<decimal>(nullable: false),
					ListedAt = table.Column<DateTime>(nullable: false),
					BoughtAt = table.Column<DateTime>(nullable: false),
					QuantityBought = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_ProductDetails_Users_SellerId",
						column: x => x.SellerId,
						principalTable: "Users",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_ProductDetails_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "ProductListings",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					HumanReadableId = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Description = table.Column<string>(nullable: false),
					SellerId = table.Column<int>(nullable: false),
					Brand = table.Column<string>(nullable: false),
					Price = table.Column<decimal>(nullable: false),
					ListedAt = table.Column<DateTime>(nullable: false),
					Available = table.Column<bool>(nullable: false),
					QuantityAvailable = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductListings", x => x.Id);
					table.ForeignKey(
						name: "FK_ProductListings_Users_SellerId",
						column: x => x.SellerId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "UserProductListing_Saved",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					ProductListingId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserProductListing_Saved", x => new { x.UserId, x.ProductListingId });
					table.ForeignKey(
						name: "FK_UserProductListing_Saved_ProductListings_ProductListingId",
						column: x => x.ProductListingId,
						principalTable: "ProductListings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserProductListing_Saved_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "UserProductListing_Shopping",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					ProductListingId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserProductListing_Shopping", x => new { x.UserId, x.ProductListingId });
					table.ForeignKey(
						name: "FK_UserProductListing_Shopping_ProductListings_ProductListingId",
						column: x => x.ProductListingId,
						principalTable: "ProductListings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserProductListing_Shopping_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateIndex(
				name: "IX_ProductDetails_SellerId",
				table: "ProductDetails",
				column: "SellerId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductDetails_UserId",
				table: "ProductDetails",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductListings_SellerId",
				table: "ProductListings",
				column: "SellerId");

			migrationBuilder.CreateIndex(
				name: "IX_UserProductListing_Saved_ProductListingId",
				table: "UserProductListing_Saved",
				column: "ProductListingId");

			migrationBuilder.CreateIndex(
				name: "IX_UserProductListing_Shopping_ProductListingId",
				table: "UserProductListing_Shopping",
				column: "ProductListingId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ProductDetails");

			migrationBuilder.DropTable(
				name: "UserProductListing_Saved");

			migrationBuilder.DropTable(
				name: "UserProductListing_Shopping");

			migrationBuilder.DropTable(
				name: "ProductListings");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
