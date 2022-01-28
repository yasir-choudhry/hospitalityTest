
CREATE DATABASE IF NOT EXISTS Hospitality DEFAULT CHARACTER SET utf8;
USE Hospitality;

CREATE TABLE tblBasket (
  BasketID INT(11) NOT NULL AUTO_INCREMENT,
  Email VARCHAR(45) NULL DEFAULT NULL,
  MenuItemID INT(2) NULL DEFAULT NULL,
  MenuItemName VARCHAR(45) NULL DEFAULT NULL,
  MenuItemDesc VARCHAR(350) NULL DEFAULT NULL,
  Quantity INT(10) NULL DEFAULT NULL,
  MenuItemPrice DECIMAL(5,2) NULL DEFAULT NULL,
  ItemTime TIME NULL DEFAULT NULL,
  NoticeReq VARCHAR(25) NULL DEFAULT NULL,
  PRIMARY KEY (BasketID));

CREATE TABLE tblMenuItems (
  MenuItemID INT(11) NOT NULL AUTO_INCREMENT,
  Position INT(11) NULL DEFAULT NULL,
  MenuItemName VARCHAR(45) NULL DEFAULT NULL,
  MenuItemDesc VARCHAR(350) NULL DEFAULT NULL,
  MenuItemPrice DECIMAL(5,2) NULL DEFAULT NULL,
  NoticeReq VARCHAR(25) NULL DEFAULT NULL,
  RFD BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (MenuItemID));

  CREATE TABLE tblOrderedItems (
  OrderNum INT(11) NOT NULL AUTO_INCREMENT,
  OrderID INT(11) NULL DEFAULT NULL,
  MenuItemID INT(11) NULL DEFAULT NULL,
  Qty INT(11) NULL DEFAULT NULL,
  MenuItemPrice DECIMAL(5,2) NULL DEFAULT NULL,
  ItemTime TIME NULL DEFAULT NULL,
  PRIMARY KEY (OrderNum));

  CREATE TABLE tblOrders (
  OrderID INT(11) NOT NULL AUTO_INCREMENT,
  RequestedDateTime DATETIME NULL DEFAULT NULL,
  OrderDate DATE NULL DEFAULT NULL,
  OrderStatusID INT(11) NULL DEFAULT NULL,
  RoomID INT(11) NULL DEFAULT NULL,
  DietaryReq VARCHAR(255) NULL DEFAULT NULL,
  MeetingStartTime TIME NULL DEFAULT NULL,
  MeetingEndTime TIME NULL DEFAULT NULL,
  HostName VARCHAR(55) NULL DEFAULT NULL,
  HostEmail VARCHAR(55) NULL DEFAULT NULL,
  HostPhone VARCHAR(25) NULL DEFAULT NULL,
  UserName VARCHAR(55) NULL DEFAULT NULL,
  UserEmail VARCHAR(55) NULL DEFAULT NULL,
  UserPhone VARCHAR(25) NULL DEFAULT NULL,
  Notes VARCHAR(250) NULL DEFAULT NULL,
  CostCentre VARCHAR(45) NULL DEFAULT NULL,
  BasketTotal DECIMAL(5,2) NULL DEFAULT NULL,
  NumAttendees VARCHAR(25) NULL DEFAULT NULL,
  PRIMARY KEY (OrderID));

  CREATE TABLE tblOrderStatus (
  OrderStatusID INT(11) NOT NULL AUTO_INCREMENT,
  OrderStatus VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (OrderStatusID));

  CREATE TABLE tblRooms (
  RoomID INT(11) NOT NULL AUTO_INCREMENT,
  RoomDesc VARCHAR(60) NULL DEFAULT NULL,
  RFD BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (RoomID));

  CREATE TABLE tblUsers (
  UserID INT(11) NOT NULL AUTO_INCREMENT,
  UserFirstName VARCHAR(30) NULL DEFAULT NULL,
  UserLastName VARCHAR(30) NULL DEFAULT NULL,
  UserEmail VARCHAR(50) NULL DEFAULT NULL,
  IsAdmin BIT(1) NULL DEFAULT b'0',
  RFD BIT(1) NULL DEFAULT NULL,
  IsCaterer BIT(1) NULL DEFAULT b'0',
  EthosName VARCHAR(8) NULL DEFAULT NULL,
  PRIMARY KEY (UserID));


  CREATE VIEW vOrderedItems AS
    SELECT 
        oi.OrderNum AS OrderNum,
        oi.OrderID AS OrderID,
        mi.MenuItemName AS MenuItemName,
        mi.MenuItemDesc AS MenuItemDesc,
        mi.NoticeReq AS NoticeReq,
        oi.Qty AS Qty,
        oi.MenuItemPrice AS MenuItemPrice,
        oi.ItemTime AS ItemTime
    FROM
        tblOrderedItems oi LEFT JOIN tblMenuItems mi ON ((oi.MenuItemID = mi.MenuItemID));


        CREATE VIEW vOrders AS
    SELECT 
        o.OrderID AS OrderID,
        os.OrderStatus AS OrderStatus,
        o.RequestedDateTime AS RequestedDateTime,
        o.OrderDate AS OrderDate,
        o.UserName AS UserName,
        o.UserEmail AS UserEmail,
        o.UserPhone AS UserPhone,
        o.HostName AS HostName,
        o.HostEmail AS HostEmail,
        o.HostPhone AS HostPhone,
        o.CostCentre AS CostCentre,
        r.RoomDesc AS RoomDesc,
        o.DietaryReq AS DietaryReq,
        o.Notes AS Notes,
        o.MeetingStartTime AS MeetingStartTime,
        o.MeetingEndTime AS MeetingEndTime,
        o.BasketTotal AS BasketTotal,
        o.NumAttendees AS NumAttendees
    FROM
        tblOrders o
        LEFT JOIN tblOrderStatus os ON ((o.OrderStatusID = os.OrderStatusID))
        LEFT JOIN tblRooms r ON ((o.RoomID = r.RoomID))

