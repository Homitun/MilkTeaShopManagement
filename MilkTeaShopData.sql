CREATE DATABASE MilkTeaShopManagement
GO

USE MilkTeaShopManagement
GO

--Drink
--Table
--Category
--Account
--Bill
--BilLInfo


CREATE TABLE TableDrink
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chua dat ten',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trong',  --trong|| availble
)
GO

CREATE TABLE Account
(
	userName NVARCHAR(100) NOT NULL PRIMARY KEY,
	displayName NVARCHAR(100) NOT NULL ,
	passWord NVARCHAR(1000) NOT NULL,
	type INT NOT NULL DEFAULT 0 -- 1 ADMIN|| 0 STAFF
)
GO

CREATE TABLE DrinkCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chua dat ten'
)
GO

CREATE TABLE Drink
(
	 id INT IDENTITY PRIMARY KEY,
	 name NVARCHAR(100) NOT NULL DEFAULT N'Chua dat ten',
	 idCategory INT NOT NULL,
	 price Float NOT NULL

	 FOREIGN KEY (idCategory) REFERENCES DrinkCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	dateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	dateCheckOut DATE NOT NULL,
	idTable INT NOT NULL,
	status INT NOT  NULL DEFAULT 0--1: DA THANH TOAN && 0: Chua thanh toan

	FOREIGN KEY (idTable) REFERENCES TableDrink(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idDrink INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idDrink) REFERENCES Drink(id)
)
GO

-- insert sample data

INSERT INTO TableDrink (name, status) VALUES
(N'Table 1', N'Trong'),
(N'Table 2', N'Trong'),
(N'Table 3', N'Trong'),
(N'Table 4', N'Available');

INSERT INTO Account (userName, displayName, passWord, type) VALUES
(N'admin', N'Admin User', N'password123', 1),
(N'staff1', N'Staff One', N'password456', 0),
(N'staff2', N'Staff Two', N'password789', 0);

INSERT INTO DrinkCategory (name) VALUES
(N'Tea'),
(N'Coffee'),
(N'Smoothie');

INSERT INTO Drink (name, idCategory, price) VALUES
(N'Green Tea', 1, 3.50),
(N'Black Coffee', 2, 2.50),
(N'Mango Smoothie', 3, 4.00);

INSERT INTO Bill (dateCheckIn, dateCheckOut, idTable, status) VALUES
('2024-08-15', '2024-08-15', 1, 1),
('2024-08-16', '2024-08-16', 2, 0),
('2024-08-17', '2024-08-17', 3, 1);


INSERT INTO BillInfo (idBill, idDrink, count) VALUES
(1, 1, 2),  -- 2 Green Tea for Bill 1
(2, 2, 1),  -- 1 Black Coffee for Bill 2
(3, 3, 3);  -- 3 Mango Smoothies for Bill 3


