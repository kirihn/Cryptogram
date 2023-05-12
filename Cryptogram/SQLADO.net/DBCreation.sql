CREATE DATABASE TestCryptogram;

use TestCryptogram;

CREATE TABLE AnyUser (
	
	UserID int PRIMARY KEY IDENTITY(1,1),
	Name char(20),
	LastName char(20),
	Username char(20) UNIQUE,
	--EncryptKey char(18),
	Status char(100),
	Email char(50) UNIQUE,
	AvatarIMG IMAGE
)

CREATE TABLE LogInUser(

	UserID int FOREIGN KEY REFERENCES AnyUser(UserID),
	Login char(50) FOREIGN KEy REFERENCES AnyUser(Email),
	Password char(50)
)

CREATE TABLE Messages(

	FromUserId int FOREIGN KEY REFERENCES AnyUser(UserId),
	ToUserID int FOREIGN KEy REFERENCES AnyUser(UserId),
	EncryptMessage char(3000)
)