# DatabaseRepositoryWebTemplate
Dapper example for ASP.NET Core

## 範例講解

有時候突然想寫個 CRUD 的簡單網頁，但就是會忘記之前架構怎麼寫的，所以就寫個範例，方便以後有東西抄XD。

<a href="https://github.com/sholfen/DatabaseRepositoryWebTemplate" target="_blank">範例下載</a>

一開始先在 Startup.cs 設定連線字串：
~~~csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    Tools.ConnectionString = Configuration.GetConnectionString("SQLiteConnction");
    ...
}
~~~

在 Controller 上的使用：
~~~csharp
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IAlbumRepository _albumRepository;

    public HomeController(ILogger<HomeController> logger, IAlbumRepository albumRepository)
    {
        _logger = logger;
        _albumRepository = albumRepository;
    }
    
    public JsonResult TestAPI()
    {
        List<Album> list = _albumRepository.Query();
        return new JsonResult(list);
    }
}
~~~

不過由於使用的是 Dapper，所以新增功能都得使用 SQL 指令，不像 Entity Framework Core 這麼方便，但 Dapper 還滿輕量化的，所以速度會比 Entity Framework Core 來得快。