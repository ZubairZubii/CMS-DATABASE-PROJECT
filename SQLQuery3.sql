
CREATE VIEW UserOrdersView AS
SELECT o.OrderID, o.UserID, p.ProductName, o.Quantity, o.Total, o.OrderDate
FROM Orders o
JOIN Product p ON o.ProductID = p.ProductID;

SELECT * FROM UserOrdersView;

CREATE VIEW DailySalesView AS
SELECT CONVERT(DATE, SalesDate) AS SalesDate, SUM(TotalSale) AS TotalSales, COUNT(*) AS TotalOrders
FROM SalesRecord
GROUP BY CONVERT(DATE, SalesDate);

SELECT * FROM DailySalesView;

CREATE VIEW ProductStockView AS
SELECT p.ProductName, c.CategoryName, p.StockCategory, p.Status
FROM Product p
JOIN Category c ON p.CategoryID = c.CategoryID;


CREATE VIEW TopSellingProductsView AS
SELECT  p.ProductName, SUM(o.Quantity) AS TotalQuantitySold
FROM Orders o
JOIN Product p ON o.ProductID = p.ProductID
GROUP BY p.ProductName



CREATE VIEW UserRolesView AS
SELECT UserID, Username, Role, status
FROM Users;


CREATE VIEW ActiveUsersView AS
SELECT u.UserID, u.Username, COUNT(o.OrderID) AS TotalOrders
FROM Users u
LEFT JOIN Orders o ON u.UserID = o.UserID
WHERE u.status = 'Active'
GROUP BY u.UserID, u.Username;


CREATE VIEW RevenueByMonthView AS
SELECT YEAR(SalesDate) AS Year, MONTH(SalesDate) AS Month, SUM(TotalSale) AS MonthlyRevenue
FROM SalesRecord
GROUP BY YEAR(SalesDate), MONTH(SalesDate);


CREATE VIEW OrderDetailsWithDiscountsView AS
SELECT o.OrderID, o.UserID, o.ProductID, o.Quantity, d.DiscountName, d.DiscountAmount, o.Total
FROM Orders o
LEFT JOIN Discounts d ON o.DiscountID = d.DiscountID;
