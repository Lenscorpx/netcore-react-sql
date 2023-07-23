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

insert into t_work
    (description_work)
values
    ('Work 1'),
    ('Work 2'),
    ('Work 3'),
    ('Work 4'),
    ('Work 5'),
    ('Work 6'),
    ('Work 7')
go