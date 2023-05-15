
CREATE TABLE AnyUser (
	
	UserID int PRIMARY KEY IDENTITY(1,1),
	Name char(30),
	LastName char(30),
	Username char(30) UNIQUE,
	--EncryptKey char(18),
	Status char(100),
	Email char(50) UNIQUE,
	AvatarIMG VARBINARY(MAX)
)

CREATE TABLE LogInUser(

	UserID int FOREIGN KEY REFERENCES AnyUser(UserID) IDENTITY(1,1),
	Login char(50) FOREIGN KEY REFERENCES AnyUser(Email) UNIQUE,
	Password char(70)
)

CREATE TABLE UsersMessages(

	FromUserID int FOREIGN KEY REFERENCES AnyUser(UserID),
	ToUserID int FOREIGN KEY REFERENCES AnyUser(UserID),
	EncryptMessage char(6000)
)
