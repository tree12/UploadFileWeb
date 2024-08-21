# UploadFileWeb
## How to run <br/>
1.For web client I use Blazor assembly with Radzen(https://blazor.radzen.com/?theme=material3)<br/>
2.For web server(web API) I use .net core.<br/>
3.I use `NSwagstudio v14.0.7.0` to generate code from API to client. You can open NSwagstudio file at `"UploadFileWeb.Blazor\Service\ServerAPIGenerated.nswag"`.<br/>
4.You do not need to do anything just run program only because I use migration to generate Database.<br/>
5.Make sure you run both `"UploadFileWeb.API"` project and `"UploadFileWeb.Blazor"` project in same time.<br/>
6.Please check Database configuration string is correct or not. It locates at `"UploadFileWeb.API\appsettings.json"` (For my PC I use LocalDB).<br/>
