use scott 
go

drop database Hospital
go

create database Hospital
go

use Hospital
go


create table Patient
(
	PatientID int primary key identity(1,1),
    [Name] varchar(30) not null,
	Gender varchar(1) constraint gen check(Gender in('M','F')) not null,
	Age int not null,
	[Address] varchar(50) not null,
	PhoneNo numeric not null,
	[Weight] int not null,
	Disease varchar(20) not null
)
go

create table Lab
(
	ReportId int primary key identity(1,1) not null,
	TestDate datetime not null,
	Remarks varchar(50),
	TestType varchar(20) not null,
	PatientID int foreign key (PatientID) references Patient(PatientID) not null
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
	PatientID int foreign key (PatientID) references Patient(PatientID) not null
)
go


create table PatientTreatment
(
	AppointmentId int primary key identity(1,1),
	DoctorName varchar(30) not null,
	RoomNo varchar(10),
	DateOfVisit datetime,
	AdmissionDate datetime,
	DischargeDate datetime,
	PatientType varchar(10) not null,
	PatientID int foreign key (PatientID) references Patient(PatientID) not null,
	ReportId int foreign key (ReportId) references Lab(ReportId) not null,
	BillNo int foreign key (BillNo) references BillData(BillNo) not null
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

/*changed this name from GetPatient to GetPatientInfo*/
create proc usp_GetPatientInfo
(
	@PatientID int
)
as
begin
select * from Patient where PatientID = @PatientID
end
go

create proc usp_GetPatientsById
(
	@PatientID int
)
as
begin
select Patient.PatientID,[Name],DoctorName,PatientType,DateOfVisit,AdmissionDate from Patient,PatientTreatment
where Patient.PatientID = PatientTreatment.PatientID
and Patient.PatientID=@PatientID
end
go

create proc usp_GetPatientsByDoctorName
(
	@DoctorName varchar(30)
)
as
begin
select Patient.PatientID,[Name],DoctorName,PatientType,DateOfVisit,AdmissionDate from Patient,PatientTreatment
where Patient.PatientID = PatientTreatment.PatientID
and PatientTreatment.DoctorName=@DoctorName
end
go

create proc usp_GetPatientsByDateOfVisit
(
	@DateOfVisit datetime
)
as
begin
select Patient.PatientID,[Name],DoctorName,PatientType,DateOfVisit,AdmissionDate from Patient,PatientTreatment
where Patient.PatientID = PatientTreatment.PatientID
and PatientTreatment.DateOfVisit=@DateOfVisit
end
go


create proc usp_GetPatientsByDateOfAdmission
(
	@AdmissionDate datetime
)
as
begin
select Patient.PatientID,[Name],DoctorName,PatientType,DateOfVisit,AdmissionDate from Patient,PatientTreatment
where Patient.PatientID = PatientTreatment.PatientID
and PatientTreatment.AdmissionDate=@AdmissionDate
end
go

create proc usp_GetAllPatientTreatmentInfo
as
begin
select Patient.PatientID,[Name],DoctorName,Lab.ReportId,BillData.BillNo,PatientType,DateOfVisit,AdmissionDate,TotalAmount from Patient,PatientTreatment,Lab,BillData 
where Patient.PatientID = PatientTreatment.PatientID
and Patient.PatientID=Lab.PatientID and Patient.PatientID=BillData.PatientID 
and PatientTreatment.ReportId = Lab.ReportId and PatientTreatment.BillNo = BillData.BillNo
end
go



create proc usp_insertPatientData
(
	@DoctorName varchar(30),
	@RoomNo varchar(10),
	@DateOfVisit datetime,
	@AdmissionDate datetime,
	@DischargeDate datetime,
	@PatientType varchar(10),
	@PatientID int,
	@TestDate datetime,
	@Remarks varchar(50),
	@TestType varchar(20), 
	@DoctorFees int,
	@RoomCharge int,
	@OperationCharge int,
	@MedicineFees int,
	@TotalDays int,
	@LabFees int,
	@TotalAmount int,
	@ReportId int,
	@BillNo int
)
as
begin
insert into Lab(TestDate,Remarks,TestType,PatientID) values(@TestDate,@Remarks,@TestType,@PatientID);
insert into BillData(DoctorFees,RoomCharge,OperationCharge,MedicineFees,TotalDays,LabFees,TotalAmount,PatientID) 
values(@DoctorFees,@RoomCharge,@OperationCharge,@MedicineFees,@TotalDays,@LabFees,@TotalAmount,@PatientID);
insert into PatientTreatment(DoctorName,RoomNo, DateOfVisit,AdmissionDate,DischargeDate,PatientType,PatientID,ReportId,BillNo) values
(@DoctorName,@RoomNo,@DateOfVisit,@AdmissionDate,@DischargeDate,@PatientType,@PatientID,@ReportId,@BillNo);
end
go


create proc usp_GetPatientId as 
begin
select top 1 PatientID from Patient order by PatientID desc
end
go

create proc usp_GetLabId as 
begin
select top 1 ReportId from Lab order by PatientID desc
end
go

create proc usp_GetBillDataId as 
begin
select top 1 BillNo from BillData order by PatientID desc
end
go

create proc usp_GetPatientTreatmentId as 
begin
select top 1 AppointmentId from PatientTreatment order by PatientID desc
end
go

create proc usp_GetLabReport
(
	@PatientID int
)
as
begin
select Patient.PatientID,[Name],DoctorName,Lab.ReportId,TestDate,TestType,Remarks from Lab,Patient,PatientTreatment 
where Patient.PatientID = @PatientID and
Patient.PatientID = PatientTreatment.PatientID
and Patient.PatientID=Lab.PatientID and Lab.ReportId = PatientTreatment.ReportId
end
go

create proc usp_GetBillData
(
	@PatientID int
)
as
begin
select Patient.PatientID,[Name],DoctorName,PatientType,BillData.BillNo,LabFees,MedicineFees,DoctorFees,OperationCharge,RoomCharge,TotalAmount
from BillData,Patient,PatientTreatment 
where Patient.PatientID = @PatientID and Patient.PatientID = PatientTreatment.PatientID
and Patient.PatientID=BillData.PatientID and BillData.BillNo = PatientTreatment.BillNo
end
go