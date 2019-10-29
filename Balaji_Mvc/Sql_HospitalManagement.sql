create database HospitalMvc
go

use HospitalMvc
go

drop table Lab
go
drop table BillData
go
drop table PatientTreatment
go

drop table Patient
go

create table Patient
(
	PatientID int primary key identity(1,1),
    [Name] varchar(30) not null,
	Gender varchar(1) constraint gen check(Gender in('M','F')) not null,
	Age int not null,
	[Address] varchar(50) not null,
	PhoneNo bigint not null,
	[Weight] int not null,
	Disease varchar(20) not null
)
go

create table PatientTreatment
(
	AppointmentId int primary key identity(1,1),
	DoctorName varchar(30) not null,
	RoomNo varchar(10),
	DateOfVisit date,
	AdmissionDate date,
	DischargeDate date,
	PatientType varchar(10) not null,
	PatientID int foreign key (PatientID) references Patient(PatientID) not null
)
go

create table Lab
(
	ReportId int primary key identity(1,1) not null,
	TestDate date not null,
	Remarks varchar(50),
	TestType varchar(20) not null,
	PatientID int foreign key (PatientID) references Patient(PatientID) not null,
	AppointmentId int foreign key (AppointmentId) references PatientTreatment(AppointmentId) not null
)
go

create table BillData
(
	BillNo int primary key identity(1,1),
	DoctorFees int not null,
	RoomCharge int,
	OperationCharge int,
	MedicineFees int not null,
	TotalDays int,
	LabFees int not null,
	TotalAmount int not null,
	PatientID int foreign key (PatientID) references Patient(PatientID) not null,
	AppointmentId int foreign key (AppointmentId) references PatientTreatment(AppointmentId) not null
)
go

select * from Patient
go
select * from BillData
go
select * from Lab
go
select * from PatientTreatment
go


create proc usp_insertPatient
(
    @Name varchar(30),
	@Gender varchar(1),
	@Age int,
	@Address varchar(50),
	@PhoneNo numeric,
	@Weight int,
	@Disease varchar(20)
)
as
begin
insert into Patient([Name],Gender,Age,[Address],PhoneNo,[Weight],Disease) 
values(@Name,@Gender,@Age,@Address,@PhoneNo,@Weight,@Disease)
end
go

create proc usp_updatePatient
(
    @PatientID int,
	@Name varchar(30),
	@Gender varchar(1),
	@Age int,
	@Address varchar(50),
	@PhoneNo numeric,
	@Weight int,
	@Disease varchar(20)
)
as
begin
update Patient set [Name]=@Name,Gender=@Gender,Age=@Age,[Address]=@Address,
PhoneNo=@PhoneNo,[Weight]=@Weight,Disease=@Disease 
where PatientID = @PatientID
end
go

create proc usp_GetPatient
(
	@PatientID int
)
as
begin
select * from Patient where PatientID = @PatientID
end
go

alter proc usp_GetPatientsById
(
	@PatientID int
)
as
begin
select Patient.PatientID,Patient.Name from Patient
where Patient.PatientID=@PatientID
end
go

create proc usp_GetPatientsByDoctorName
(
	@DoctorName varchar(30)
)
as
begin
select * from Patient,PatientTreatment,Lab,BillData 
where Patient.PatientID = PatientTreatment.PatientID
and Patient.PatientID=Lab.PatientID and Patient.PatientID=BillData.PatientID
and PatientTreatment.DoctorName=@DoctorName
end
go

create proc usp_GetPatientsByDateOfVisit
(
	@DateOfVisit date
)
as
begin
select * from Patient,PatientTreatment
where Patient.PatientID = PatientTreatment.PatientID
and PatientTreatment.DoctorName=@DateOfVisit
end
go


create proc usp_GetPatientsByDateOfAdmission
(
	@AdmissionDate date
)
as
begin
select * from Patient,PatientTreatment 
where Patient.PatientID = PatientTreatment.PatientID
and PatientTreatment.AdmissionDate=@AdmissionDate
end
go

create proc usp_GetAllPatientTreatmentInfo
as
begin
select * from Patient
end
go

create proc usp_insertPatientData
(
	@DoctorName varchar(30),
	@RoomNo varchar(10),
	@DateOfVisit date,
	@AdmissionDate date,
	@DischargeDate date,
	@PatientType varchar(10),
	@PatientID int,
	@TestDate date,
	@Remarks varchar(50),
	@TestType varchar(20), 
	@DoctorFees int,
	@RoomCharge int,
	@OperationCharge int,
	@MedicineFees int,
	@TotalDays int,
	@LabFees int,
	@TotalAmount int
)
as
begin
insert into PatientTreatment(DoctorName,RoomNo, DateOfVisit,AdmissionDate,DischargeDate,PatientType,PatientID) values
(@DoctorName,@RoomNo,@DateOfVisit,@AdmissionDate,@DischargeDate,@PatientType,@PatientID);
insert into Lab(TestDate,Remarks,TestType,PatientID) values(@TestDate,@Remarks,@TestType,@PatientID);
insert into BillData(DoctorFees,RoomCharge,OperationCharge,MedicineFees,TotalDays,LabFees,TotalAmount,PatientID) 
values(@DoctorFees,@RoomCharge,@OperationCharge,@MedicineFees,@TotalDays,@LabFees,@TotalAmount,@PatientID);
end

alter proc usp_GetPatientId as 
begin
select top 1 PatientID from Patient order by PatientID desc
end
go

exec usp_GetPatientId

create proc usp_GetLabReport
(
	@PatientID int
)
as
begin
select * from Lab where PatientID = @PatientID
end


create proc usp_GetBillData
(
	@PatientID int
)
as
begin
select * from BillData where PatientID = @PatientID
end

insert into BillData values
(
	'1000','1200','10000', '1300','12','1200','160000', '2')
	insert into Lab values
( '2019-12-21', 'fdgbf', 'MRI','3')
insert into Lab values
( '2019-12-23', 'GFHDH', 'MRI2','2')
    