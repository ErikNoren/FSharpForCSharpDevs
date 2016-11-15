namespace FSharpLibrary

open FSharp.Data

type countryAndCapital = { Name : string; Capital : string }

type WorldBankRepository() =
    let ctx = WorldBankData.GetDataContext()

    member this.Countries =
        query {
            for country in ctx.Countries do
            select country
        }

    member this.CountriesAndCapitals =
        query {
            for country in ctx.Countries do
            select { Name = country.Name; Capital = country.CapitalCity }
        }

    member this.Regions =
        query {
            for region in ctx.Regions do
            select region
        }

    member this.Topics =
        query {
            for topic in ctx.Topics do
            select topic
        }

    member this.GetIndicatorsForCountry (country:string) =
        query {
            for country in ctx.Countries do
            where (country.Name.Equals(country))
            select country.Indicators
        }

    member this.GetIndicatorForCountry (country:string) (indicator:string) =
        let country = query {
            for country in ctx.Countries do
            where (country.Name.Equals(country))
            select country
            exactlyOneOrDefault
        }
        query {
            for indicator in country.Indicators do
            where (indicator.Name.Equals(indicator))
            select indicator
        }