USE [payroll_service]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAddEmployee]
(
@EmployeeName VARCHAR(30),
@Gender CHAR(1),
@PhoneNumber BIGINT,
@EmployeeAddress VARCHAR(100),
@StartDate DATE,
@BasicPay FLOAT,
@Deductions FLOAT,
@IncomeTax FLOAT,
@CompanySelect INT,
@DepartmentSelect INT,
@EmployeeSelect INT 
)
AS
BEGIN TRY 
INSERT INTO EmployeeTable(EmployeeName,Gender,StartDate,PhoneNo,EmployeeAddress,CompanySelect)
VALUES (@EmployeeName,@Gender,@StartDate,@PhoneNumber,@EmployeeAddress,@CompanySelect)
INSERT INTO PayrollTable(BasicPay,Deductions,IncomeTax,EmployeeSelect)
VALUES(@BasicPay,@Deductions,@IncomeTax,@EmployeeSelect)
INSERT INTO EmployeeDepartmentTable(DepartmentSelect,EmployeeSelect)
VALUES(@DepartmentSelect,@EmployeeSelect)
END TRY

BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH