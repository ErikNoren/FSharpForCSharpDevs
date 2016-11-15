namespace FSharpLibrary

open FSharp.Data

type xmlData = FSharp.Data.XmlProvider<Sample = """<?xml version="1.0"?><catalog><book id="bk0"><author>Author</author><title>Title</title><genre>Genre</genre><price>1.23</price><publish_date>2000-01-01</publish_date><description></description></book><book id="bk1"><author>Author</author><title>Title</title><genre>Genre</genre><price>1.23</price><publish_date>2000-01-01</publish_date><description></description></book></catalog>""">

type XmlDataRepository() =
    let ctx = xmlData.Load(Settings.DataDirectoryRoot + "XMLDataFile.xml")
    
    member this.GetBookTitles =
        query {
            for book in ctx.Books do
            select book.Title
        }

    member this.GetBookData =
        query {
            for book in ctx.Books do
            select book
        }

    member this.GetAuthorList =
        query {
            for book in ctx.Books do
            select book.Author
            distinct
        }

