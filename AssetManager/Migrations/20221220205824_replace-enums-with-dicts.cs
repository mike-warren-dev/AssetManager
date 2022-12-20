using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    /// <inheritdoc />
    public partial class replaceenumswithdicts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "ProductOrder",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Site",
                table: "Asset",
                newName: "SiteId");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Asset",
                newName: "ModelId");

            migrationBuilder.RenameColumn(
                name: "AssetType",
                table: "Asset",
                newName: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_VendorId",
                table: "Order",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ModelId",
                table: "Asset",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SiteId",
                table: "Asset",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_DictOption_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId",
                principalTable: "DictOption",
                principalColumn: "DictOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_DictOption_ModelId",
                table: "Asset",
                column: "ModelId",
                principalTable: "DictOption",
                principalColumn: "DictOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_DictOption_SiteId",
                table: "Asset",
                column: "SiteId",
                principalTable: "DictOption",
                principalColumn: "DictOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DictOption_VendorId",
                table: "Order",
                column: "VendorId",
                principalTable: "DictOption",
                principalColumn: "DictOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_DictOption_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "DictOption",
                principalColumn: "DictOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_DictOption_AssetTypeId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_DictOption_ModelId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_DictOption_SiteId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DictOption_VendorId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_DictOption_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_Order_VendorId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_ModelId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_SiteId",
                table: "Asset");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductOrder",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "Asset",
                newName: "Site");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Asset",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "AssetTypeId",
                table: "Asset",
                newName: "AssetType");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
