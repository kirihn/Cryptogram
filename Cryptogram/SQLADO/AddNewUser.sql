CREATE PROCEDURE AddAnyUser
(
    @Name char(20),
    @LastName char(20),
    @Username char(20),
    @Status char(100),
    @Email char(70),
    @AvatarIMG VARBINARY(MAX),

    @Login char(70),
    @Password char(70)
)
AS
BEGIN
    INSERT INTO AnyUser (Name, LastName, Username, Status, Email, AvatarIMG)
    VALUES (@Name, @LastName, @Username, @Status, @Email, @AvatarIMG);

    INSERT INTO LogInUser (Login, Password)
    VALUES (@Login, @Password)
END