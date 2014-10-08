
drop table SimpleIntervalScheduleTestData
create table SimpleIntervalScheduleTestData(
Id int IDENTITY(1,1) not null,
ScheduleStartTime datetime  null,
Interval int null,
StartTime   datetime null,
IncludeStartTime bit null,
ExpectedTime datetime null
)

create table SimpleIntervalScheduleConstructorTestData(
Id int IDENTITY(1,1) not null,
StartTime datetime null,
Interval int null,
Count int null,
ExpectedEndTime datetime null
)

truncate table SimpleIntervalScheduleTestData
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:00:00', 5, '2014-08-27 11:00:00', 0, '2014-08-27 11:05:00');
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:00:00', 5, '2014-08-27 11:00:00', 1, '2014-08-27 11:00:00');
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:01:00', 5, '2014-08-27 10:00:00', 1, '2014-08-27 10:06:00');
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:01:00', 5, '2014-08-27 10:00:00', 0, '2014-08-27 10:06:00');
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:01:00', 5, '2014-08-27 10:13:00', 0, '2014-08-27 10:16:00');
insert into SimpleIntervalScheduleTestData values('2014-08-27 10:01:00', 5, '2014-08-27 10:13:00', 0, '2014-08-27 10:16:00');
insert into SimpleIntervalScheduleTestData values('2014-09-12 00:00:00', 2, '2014-09-12 15:25:30', 1, '2014-09-12 15:26:00');

select * from SimpleIntervalScheduleTestData
update  SimpleIntervalScheduleTestData
set ExpectedTime = '2014-08-27 10:16:00'
where Id = 5

Insert into SimpleIntervalScheduleConstructorTestData values('2014-08-27 10:00:00', 5, 0, '2014-08-27 10:00:00')
Insert into SimpleIntervalScheduleConstructorTestData values('2014-08-27 10:00:00', 5, 1, '2014-08-27 10:05:00')
Insert into SimpleIntervalScheduleConstructorTestData values('2014-08-27 10:00:00', 5, 2147483647, '9999-12-31 23:59:59')

select * from SimpleIntervalScheduleConstructorTestData

update SimpleIntervalScheduleConstructorTestData
set ExpectedEndTime = '9999-12-31 23:59:59.9999999'
where Id = 3
drop table SimpleIntervalScheduleRunTimesTestData
create table SimpleIntervalScheduleRunTimesTestData(
Id int IDENTITY(1, 1) not null,
ScheduleStartTime dateTime null,
Interval int null,
ScheduleEndTime dateTime null,
StartTime datetime null,
EndTime datetime null,
ExpectedTimes nvarchar(max) null
)

Create procedure GenerateDataForRuntimes
as
begin
declare @ScheduleStartTime datetime
declare @ScheduleEndTime datetime
declare @Interval int
declare @StartTime datetime
declare @EndTime datetime
declare @loopTime datetime
declare @ExpectedRunTimes nvarchar

set @ScheduleStartTime = '2014-08-27 10:00:00'
set @ScheduleEndTime = '2014-08-28 10:00:00'
set @Interval = 5
set @StartTime = '2014-08-27 11:00:00'
set @EndTime = '2014-08-27 11:36:00'
set @loopTime = @StartTime
set @ExpectedRunTimes =''
while (@loopTime < @EndTime)
begin
  set @ExpectedRunTimes = @ExpectedRunTimes + convert(nvarchar, @loopTime, 120) + ','
  set @loopTime = DateAdd(mm,@Interval,@StartTime)
end
insert into SimpleIntervalScheduleRunTimesTestData values(@ScheduleStartTime,@Interval,@ScheduleEndTime,@StartTime,@EndTime,@ExpectedRunTimes)
end

execute GenerateDataForRuntimes

select * from SimpleIntervalScheduleRunTimesTestData

insert into SimpleIntervalScheduleRunTimesTestData values('2014-09-12 00:00:00', 5, '2014-09-12 00:00:00', '2014-09-12 16:01:27', '2014-09-12 16:02:00','2014-08-27 11:00:00')
