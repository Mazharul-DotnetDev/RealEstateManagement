using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateManagement.Migrations
{
    /// <inheritdoc />
    public partial class SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            string SpInsertAsset = @"CREATE or ALTER PROCEDURE dbo.SpInsertAsset 
    @PropertyName nvarchar(max), 
    @P_Address nvarchar(max), 
    @NumberOfUnits int,  
    @RentAmount decimal(18,2)  
    
AS
    
INSERT INTO [dbo].[AssetTble]
           ( [PropertyName]
           ,[P_Address]
           ,[NumberOfUnits]
           ,[RentAmount] )
           
     VALUES
           (@PropertyName, @P_Address, @NumberOfUnits, @RentAmount)

	      return @@identity
GO";

            migrationBuilder.Sql(SpInsertAsset);






            string SpInsertOwner = @"CREATE or ALTER PROCEDURE dbo.SpInsertOwner 
    @OwnerName nvarchar(max),
    @Own_ContactInformation nvarchar(max),
    @Salary decimal(18,2),
    @TenantId int,  
    @AssetId int  
	
AS
    

INSERT INTO [dbo].[OwnerTble]
           ([OwnerName]
           ,[Own_ContactInformation]
           ,[Salary]
           ,[TenantId]
           ,[AssetId])
     VALUES
           ( @OwnerName, @Own_ContactInformation, @Salary, @TenantId, @AssetId)

		   return @@rowcount
GO";


            migrationBuilder.Sql(SpInsertOwner);






            string SpUpdateAsset = @"CREATE or ALTER PROCEDURE dbo.SpUpdateAsset 
    @PropertyName nvarchar(max),
    @P_Address nvarchar(max),
    @NumberOfUnits int,
    @RentAmount decimal(18,2),
    @AssetId int
     
AS
    
UPDATE [dbo].[AssetTble]
   SET [PropertyName] =  @PropertyName
      ,[P_Address] = @P_Address
      ,[NumberOfUnits] = @NumberOfUnits
      ,[RentAmount] = @RentAmount
      --,[TenantId] = @TenantId

 WHERE  AssetId = @AssetId

 delete from OwnerTble where AssetId = @AssetId

	  return @@rowcount
GO";

            migrationBuilder.Sql(SpUpdateAsset);





            string SpDeleteAsset = @"CREATE or ALTER PROCEDURE dbo.SpDeleteAsset
     @AssetId int
    
AS
    
       delete from OwnerTble where AssetId = @AssetId
	   delete from AssetTble where AssetId = @AssetId

	  return @@rowcount
GO";

            migrationBuilder.Sql(SpDeleteAsset);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc SpInsertAsset");
            migrationBuilder.Sql("drop proc SpInsertOwner");
            migrationBuilder.Sql("drop proc SpUpdateAsset");
            migrationBuilder.Sql("drop proc SpDeleteAsset");
        }
    }
}
