namespace FSharpLibrary

open FSharp.Data

type FinanceRecord = { Date: System.DateTime; Open: double; High: double; Low: double; Close: double; Volume: int; AdjClose: double }

type CsvRepository() =
    let ctx = CsvFile.Load("http://ichart.finance.yahoo.com/table.csv?s=MSFT").Cache()

    member this.Headers =
        match ctx.Headers with
        | Some h -> h
        | None -> [||]

    member this.ColumnCount =
        ctx.NumberOfColumns

    member this.RawData =
        query {
            for row in ctx.Rows do
            select row.Columns
        }

    member this.FinanceData =
        query {
            for row in ctx.Rows do
            select { Date = System.DateTime.Parse(row.Item(0)); Open = double(row.Item(1)); High = double(row.Item(2)); Low = double(row.Item(3)); Close = double(row.Item(4)); Volume = int(row.Item(5)); AdjClose = double(row.Item(6)) }
        }