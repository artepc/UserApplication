CREATE PROCEDURE [dbo].[User_GetUserByUsername]
@Username nvarchar(30)
AS
begin
	SELECT *
	FROM [dbo].[User]
    WHERE Username = @Username;
end