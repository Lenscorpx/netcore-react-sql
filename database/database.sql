use master
go
create database db_mywork
go
use db_mywork
go
create table t_work(
    id int identity,
    description_work nvarchar(max),
    constraint pk_work primary key(id)
)
go
create procedure getworks
as
select * from t_work order by id desc
go

create procedure addwork
@description_work nvarchar(50)
as
insert into t_work
    (description_work)
values
(@description_work)
go

create procedure deletework
@id int
as
    delete from t_work where id like @id
go