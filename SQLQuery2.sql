INSERT INTO users (Username, Password, Email, Role, status, profile_image, reg_date)
VALUES ('zubair', 'password123', 'zubair@gmail.com', 'Customer', 'Active', '5259.png_1200.png', GETDATE());

INSERT INTO users (Username, Password, Email, Role, status, profile_image, reg_date)
VALUES ('admin', 'adminpassword123', 'admin@gmail.com', 'Manager', 'Active', '5259.png_1200.png', GETDATE());

INSERT INTO users (Username, Password, Email, Role, status, profile_image, reg_date)
VALUES ('cashier', 'cashierpassword123', 'cashier@gmail.com', 'Cashier', 'Active', 'user.png', GETDATE());

INSERT INTO users (Username, Password, Email, Role, status, profile_image, reg_date)
VALUES ('ali', 'alipassword123', 'ali@gmail.com', 'Customer', 'Active', 'user.png', GETDATE());


delete from users where Username = 'ali';

select * from users;


INSERT INTO Discounts (DiscountName, DiscountAmount) VALUES ('No Discount', 0.0);
INSERT INTO Discounts (DiscountName, DiscountAmount) VALUES ('10% Off', 0.1);
INSERT INTO Discounts (DiscountName, DiscountAmount) VALUES ('20% Off', 0.2);
INSERT INTO Discounts (DiscountName, DiscountAmount) VALUES ('30% Off', 0.3);

SELECT * FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID('dbo.users');



INSERT INTO Category (CategoryID, CategoryName) VALUES
(1, 'Beverages'),
(2, 'Pastries and Desserts'),
(3, 'Sandwiches and Wraps'),
(4, 'Breakfast Items'),
(5, 'Salads'),
(6, 'Snacks');


-- Beverages
-- Beverages
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Coffee', 2.99, 20, 'Active', GETDATE(), 'Hot', 'coffee.png', 1),
       ('Tea', 1.99, 30, 'Active', GETDATE(), 'Hot', 'tea.png', 1),
       ('Soft Drink', 1.49, 50, 'Active', GETDATE(), 'Cold', 'softdrink.png', 1);

-- Pastries and Desserts
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Chocolate Cake', 9.99, 10, 'Active', GETDATE(), 'Cake', 'cake.png', 2),
       ('Blueberry Muffin', 2.49, 15, 'Active', GETDATE(), 'Muffin', 'muffin.png', 2), --
       ('Chocolate Chip Cookie', 1.29, 25, 'Active', GETDATE(), 'Cookie', 'cookie.png', 2);




-- Sandwiches and Wraps
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Turkey Sandwich', 5.99, 10, 'Active', GETDATE(), 'Sandwich', 'turkeysandwich.png', 3),
       ('Veggie Wrap', 4.49, 15, 'Active', GETDATE(), 'Wrap', 'veggiewrap.png', 3);  --



-- Breakfast Items
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Croissant', 2.99, 20, 'Active', GETDATE(), 'Croissant', 'croissant.png', 4),
       ('Bagel', 1.99, 25, 'Active', GETDATE(), 'Bagel', 'bagel.png', 4);

-- Salads
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Caesar Salad', 6.99, 10, 'Active', GETDATE(), 'Caesar', 'caesarsalad.png', 5),
       ('Greek Salad', 7.49, 12, 'Active', GETDATE(), 'Greek', 'greeksalad.png', 5);

-- Snacks
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Potato Chips', 1.49, 30, 'Active', GETDATE(), 'Chips', 'potatochips.png', 6),
       ('Pretzels', 1.29, 35, 'Active', GETDATE(), 'Pretzels', 'pretzels.png', 6);







	   -- Beverages
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Iced Coffee', 3.49, 15, 'Active', GETDATE(), 'Cold', 'icedcoffee.png', 1)    --
-- Pastries and Desserts


INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Apple Pie', 4.99, 10, 'Active', GETDATE(), 'Pie', 'applepie.png', 2)

-- Sandwiches and Wraps
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Club Sandwich', 7.99, 10, 'Active', GETDATE(), 'Sandwich', 'clubsandwich.png', 3)

-- Breakfast Items
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Pancakes', 5.99, 15, 'Active', GETDATE(), 'Pancakes', 'pancakes.png', 4)        --




-- Salads
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Caprese Salad', 8.49, 10, 'Active', GETDATE(), 'Caprese', 'capresesalad.png', 5)

-- Snacks
INSERT INTO Product (ProductName, Price, StockCategory, Status, InsertDate, Type, Image, CategoryID) 
VALUES ('Trail Mix', 2.99, 25, 'Active', GETDATE(), 'Trail Mix', 'trailmix.png', 6)

select 
