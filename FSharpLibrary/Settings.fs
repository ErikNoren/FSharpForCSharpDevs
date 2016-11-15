module Settings

[<Literal>]
let DataDirectoryRoot = @"D:\Source\GitHub\FSharpForCSharpDevs\CSharpConsoleApplication\Data\"

[<Literal>]
let LocalDatabaseConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DataDirectoryRoot + "LocalDatabase.mdf;Integrated Security=True"