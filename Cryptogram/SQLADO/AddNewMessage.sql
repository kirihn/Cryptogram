CREATE PROCEDURE AddMessage
    @FromUserID int,
    @ToUserID int,
    @EncryptMessage char(3000)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO UsersMessages (FromUserID, ToUserID, EncryptMessage)
    VALUES (@FromUserID, @ToUserID, @EncryptMessage)
END