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