select * from GetTypeNameFromConfigurationTestData
update GetTypeNameFromConfigurationTestData
set TaskType = 'Demandforce.DFLink.Controller.RequestTask,  Controller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
where Id = 1

update GetTypeNameFromConfigurationTestData
set TaskType = 'Demandforce.DFLink.Update.UpdateTask, Update,  Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
where Id = 2

Create table CreateTaskTestData(
Id int IDENTITY(1,1) NOT NULL,
TaskName nvarchar(50) null,
Expected nvarchar(Max) null
)

insert into CreateTaskTestData values('Request', 'Request');
insert into CreateTaskTestData values('Update', 'Update');
