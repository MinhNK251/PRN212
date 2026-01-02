-- DROP DATABASE PRN212_Day11;
CREATE DATABASE PRN212_Day11;
Go
USE PRN212_Day11;

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
	Description NVARCHAR(255),
	CreatedDate DATETIME DEFAULT GETDATE(),
	UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    CategoryId INT,
    Price DECIMAL(18, 2) NOT NULL,
	Quantity INT NOT NULL,
    Description NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
	UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(50),
    CreatedDate DATETIME DEFAULT GETDATE(),
	UpdatedDate DATETIME DEFAULT GETDATE(),
);

CREATE TABLE OrderDetail (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT NOT NULL,
	UnitPrice DECIMAL(18, 2) NOT NULL,
	Discount DECIMAL(10, 2) DEFAULT 0,
    Total_price AS (Quantity * UnitPrice * (1 - Discount / 100)) PERSISTED,
    CreatedDate DATETIME DEFAULT GETDATE(),
	UpdatedDate DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
-- Sample data for Category table
INSERT INTO Category (CategoryName, Description)
VALUES 
('Electronics', 'Devices and gadgets'),
('Clothing', 'Apparel and accessories'),
('Home Appliances', 'Appliances for home use'),
('Books', 'Various kinds of books');

-- Sample data for Product table
INSERT INTO Product (ProductName, CategoryID, Price, Quantity, Description)
VALUES 
('Smartphone', 1, 299.99, 50, 'Latest model smartphone'),
('Laptop', 1, 799.99, 30, 'High-performance laptop'),
('T-Shirt', 2, 19.99, 100, 'Cotton T-Shirt'),
('Jeans', 2, 49.99, 60, 'Denim jeans'),
('Microwave Oven', 3, 89.99, 40, 'Efficient microwave oven'),
('Refrigerator', 3, 499.99, 20, 'Large capacity refrigerator'),
('Fiction Book', 4, 9.99, 200, 'Popular fiction book'),
('Science Book', 4, 29.99, 100, 'Informative science book');

-- Sample data for Order table
INSERT INTO [Order] (CustomerID, OrderDate, TotalAmount, Status)
VALUES 
(1, '2024-06-01', 399.98, 'Completed'),
(2, '2024-06-02', 69.98, 'Pending'),
(3, '2024-06-03', 599.98, 'Shipped'),
(4, '2024-06-04', 49.99, 'Completed');

-- Sample data for OrderDetail table
INSERT INTO OrderDetail (OrderID, ProductID, Quantity, UnitPrice, Discount)
VALUES 
(1, 1, 1, 299.99, 0),
(1, 3, 5, 19.99, 10),
(2, 4, 1, 49.99, 0),
(3, 2, 1, 799.99, 25),
(3, 6, 1, 499.99, 0),
(4, 4, 1, 49.99, 0);