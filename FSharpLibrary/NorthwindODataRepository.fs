namespace FSharpLibrary

open FSharp.Data
open Microsoft.FSharp.Data.TypeProviders

type NorthwindEntities = Microsoft.FSharp.Data.TypeProviders.ODataService<ServiceUri = "http://services.odata.org/Northwind/Northwind.svc">

type NorthwindRepository() =
    let ctx = NorthwindEntities.GetDataContext()

    member this.Categories =
        query {
            for category in ctx.Categories do
            select category
        }

    member this.Products =
        query {
            for product in ctx.Products do
            select product
        }

    member this.ProductsSorted =
        query {
            for product in ctx.Alphabetical_list_of_products do
            select product
        }

    member this.SummarySalesByYear =
        query {
            for sales in ctx.Summary_of_Sales_by_Years do
            select sales
        }

    member this.GetCustomerByCompany companyName =
        query {
            for customer in ctx.Customers do
            where (customer.CompanyName.StartsWith(companyName))
            select customer
        }

    member this.GetOrdersForCustomerById customerId =
        query {
            for orders in ctx.Order_Details do
            where (orders.Order.CustomerID = customerId)
            select orders
        }
