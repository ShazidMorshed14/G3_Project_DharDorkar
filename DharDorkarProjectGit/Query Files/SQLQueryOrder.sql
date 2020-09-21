create table Tbl_Order(

OrderId int identity primary key,
ProductNames varchar(500),
ProductQuantities nvarchar(50),
TotalPayment decimal,
FirstName varchar(500),
LastName varchar(100),
EmailId varchar(100),
NationalId varchar(max),
Address varchar(500),
PhoneNumber int,
)