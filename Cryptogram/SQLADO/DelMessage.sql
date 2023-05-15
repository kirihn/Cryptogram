CREATE PROCEDURE DeleteMessage
    @FromUserID int,
    @ToUserID int
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM UsersMessages WHERE FromUserID = @FromUserID and ToUserID = @ToUserID
END

