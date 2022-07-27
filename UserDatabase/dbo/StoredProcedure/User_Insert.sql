CREATE PROCEDURE [dbo].[User_Insert]
	@Username nvarchar(30),
	@Email nvarchar(30),
	@Fullname nvarchar(30),
	@Password nvarchar(30)
AS
begin
	INSERT INTO 
		[dbo].[User]
           ([Username]
           ,[Email]
           ,[Fullname]
           ,[Password])
	VALUES (@Username,@Email,@Fullname,@Password);
end
