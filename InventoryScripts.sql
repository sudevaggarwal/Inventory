create DataBase InventaryDb
use InventaryDb

------Table-------
create table Inventary_Master(
Id int identity(1,1) primary key,
Name varchar(50),
Description varchar(200),
PricePerUnit int,
Quantity int,
TotalPrice int,
CreatedOn datetime)


-----Procedures----
----------Get Inventaries-------

create proc usp_Get_Inventaries
as
begin
select Id,Name,Description,PricePerUnit,Quantity,TotalPrice,CreatedOn from Inventary_Master with(nolock)
end

-------------Get Inventary------

create proc usp_Get_Inventary
@id int
as
begin
select Id,Name,Description,PricePerUnit,Quantity,TotalPrice,CreatedOn from Inventary_Master with(nolock) where Id=@id
end

--------Save Inventary------

create proc usp_Save_Inventary
@inventaryName varchar(50),
@inventaryDescription varchar(200),
@inventaryQuantity int,
@inventaryPricePerUnit int,
@inventaryTotalPrice int,
@inventaryCreatedOn datetime
as
begin
insert into Inventary_Master(Name,Description,Quantity,PricePerUnit,TotalPrice,CreatedOn)
values(@inventaryName,@inventaryDescription,@inventaryQuantity,
@inventaryPricePerUnit,@inventaryTotalPrice,@inventaryCreatedOn)
end

-----Update Inventary-----

create proc usp_Update_Inventary
@id int,
@inventaryName varchar(50),
@inventaryDescription varchar(200),
@inventaryQuantity int,
@inventaryPricePerUnit int,
@inventaryTotalPrice int,
@inventaryCreatedOn datetime
as
begin
update Inventary_Master set name = @inventaryName,Description= @inventaryDescription,
Quantity = @inventaryQuantity,PricePerUnit = @inventaryPricePerUnit,
TotalPrice = @inventaryTotalPrice,CreatedOn = @inventaryCreatedOn
where id=@id
end

----------Delete Inventary-------

create proc usp_Delete_Inventary
@id int
as
begin 
delete from Inventary_Master where id = @id
end