namespace FSharpLibrary

#if INTERACTIVE
#r "System.Data"
#r "System.Data.Entity"
#r "FSharp.Data.TypeProviders"
#endif

open System.Data
open System.Data.Entity
open Microsoft.FSharp.Data.TypeProviders

type internal SqlConnection = Microsoft.FSharp.Data.TypeProviders.SqlEntityConnection<ConnectionString = Settings.LocalDatabaseConnectionString, Pluralize = true>

type Employee = { EmployeeId: string; FirstName: string; Surname: string; Email: string}

type LocalDataRepository() =
    let ctx = SqlConnection.GetDataContext()

    member this.Employees =
        query {
            for employee in ctx.Employees do
            select { EmployeeId = employee.EmployeeId; FirstName = employee.FirstName; Surname = employee.Surname; Email = employee.Email }
        }

    member this.SearchEmployees name =
        query {
            for employee in ctx.Employees do
            where (employee.Surname.StartsWith(name) || employee.FirstName.StartsWith(name))
            select employee.EmployeeId
        }

    member this.GetEmployeeEmail employeeId =
        query {
            for employee in ctx.Employees do
            where (employee.EmployeeId.Equals(employeeId))
            select employee.Email
            exactlyOneOrDefault
        }

    member this.GetEmployeesByOfficeCode officeCode =
        query {
            for employee in ctx.Employees do
            join office in ctx.Offices
                on (employee.HomeOfficeFK = office.Id)
            where (office.Code = officeCode)
            select { EmployeeId = employee.EmployeeId; FirstName = employee.FirstName; Surname = employee.Surname; Email = employee.Email }
        }
