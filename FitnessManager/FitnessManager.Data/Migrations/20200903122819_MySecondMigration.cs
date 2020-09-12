using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_HomeWork_4_CORE.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Coaches",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Coaches",
                maxLength: 255,
                nullable: false,
                defaultValue: "");


            migrationBuilder.Sql(@"
                
                DECLARE @currentId INT;
                SET @currentId = 1;
                DECLARE @recordCount INT;
                SET @recordCount = (SELECT COUNT(Id) FROM dbo.Coaches);

                WHILE @recordCount>= @currentId 
                BEGIN
	                UPDATE	dbo.Coaches
		                SET FirstName = CASE 
							                WHEN CHARINDEX(' ', FullName)  = 0 THEN SUBSTRING(FullName, 1, LEN(FullName)) 
							                ELSE SUBSTRING(FullName, 1, CHARINDEX(' ', FullName))
						                END		
		                WHERE Id = @currentId
		
	                UPDATE	dbo.Coaches
		                SET LastName = CASE 
							                WHEN CHARINDEX(' ', FullName)  = 0 THEN ''
							                ELSE SUBSTRING(FullName, CHARINDEX(' ', FullName), LEN(FullName))
						                END		
		                WHERE Id = @currentId
			
		                SET @currentId = @currentId + 1
                END
                ");
            
            
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Coaches");

            

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Petrovich", "" });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Samson", "" });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Oleksandr", "I" });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Anna", "G." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Coaches",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                
                DECLARE @currentId INT;
                SET @currentId = 1;
                DECLARE @recordCount INT;
                SET @recordCount = (SELECT COUNT(Id) FROM dbo.Coaches);

                WHILE @recordCount>= @currentId 
	                BEGIN
		                UPDATE	dbo.Coaches
			                SET FullName = CONCAT (FirstName + ' ',LastName)
			                WHERE Id = @currentId
			
			                SET @currentId = @currentId + 1
	                END
                                ");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Coaches");

            

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Petrovich");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Samson");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Oleksandr I");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 4,
                column: "FullName",
                value: "Anna G.");
        }
    }
}
