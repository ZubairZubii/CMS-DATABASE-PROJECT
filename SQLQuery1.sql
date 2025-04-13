
CREATE TRIGGER UpdateOrderStatus
ON Orders
AFTER INSERT
AS
BEGIN
    UPDATE Orders
    SET Status = 'Pending'
    FROM inserted
    WHERE Orders.OrderID = inserted.OrderID;
END;




CREATE TRIGGER maintain_order_stock
ON Orders
AFTER INSERT
AS
BEGIN
    DECLARE @ProductID INT, @Quantity INT;

    SELECT @ProductID = ProductID,
           @Quantity = Quantity
    FROM inserted;

    UPDATE Product
    SET StockCategory = StockCategory - @Quantity
    WHERE ProductID = @ProductID;
END;



CREATE TRIGGER UpdateSalesRecord
ON Orders
AFTER INSERT
AS
BEGIN
    DECLARE @SalesID INT, @TotalSale DECIMAL(10, 2), @TotalQuantity INT, @CashierID INT, @CashierName VARCHAR(255);


    SELECT @TotalSale = SUM(o.Quantity * p.Price),
           @TotalQuantity = SUM(o.Quantity),
           @CashierID = MAX(o.UserID) 
    FROM inserted o
    INNER JOIN Product p ON o.ProductID = p.ProductID;

   
    SELECT @CashierName = Username FROM Users WHERE UserID = @CashierID;

    
    SET @SalesID = (SELECT SalesID FROM SalesRecord WHERE CAST(SalesDate AS DATE) = CAST(GETDATE() AS DATE) AND CashierID = @CashierID);

 
    IF @SalesID IS NOT NULL
    BEGIN
        UPDATE SalesRecord
        SET TotalSale = TotalSale + @TotalSale,
            TotalQuantitySold = TotalQuantitySold + @TotalQuantity,
            CashierName = @CashierName
        WHERE SalesID = @SalesID;
    END
    ELSE
    BEGIN
        INSERT INTO SalesRecord (CashierID, CashierName, TotalSale, TotalQuantitySold, SalesDate)
        VALUES (@CashierID, @CashierName, @TotalSale, @TotalQuantity, GETDATE());
    END;
END;
