[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/FSGTCrc2)
# TransactionController - GetByParameter API 📝
Required  📫
- Filter criteria: AccountNumber, MinAmountCredit, MaxAmountCredit, MinAmountDebit, MaxAmountDebit, Description, BeginDate, EndDate, and ReferenceNumber.
- The fields AccountNumber and ReferenceNumber will be used with the equality operator (==).
- The fields AccountNumber and ReferenceNumber will be used with the equality operator (==).
- The BeginDate and EndDate fields will be used for range-based searches.

## Data Layer
We are creating our Transaction model in the data layer and inheriting it from the IdBaseModel.
```c#
namespace SipayApi.DataLayer.Entities
{
    public class Transaction : IdBaseModel
    {
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }
        public decimal CreditAmount { get; set; }   // -
        public string DebitAmount { get; set; }    // +
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
```
 Database connection between the database and our models;
First We store our database connection information in the appsettings.json file, which allows us to easily change the database connection details without modifying the application's source code.
(Later, don't forget to add your connection to the Startup)
```c#
  "ConnectionStrings": {
    "DbType": "Sql",
    "MsSqlConnection": "Server=DESKTOP-29HNT04; Database=sipy;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
SimDbContext class inherits from the DbContext class provided by Entity Framework Core, facilitating communication between model classes in your application and the corresponding database tables. It streamlines database operations by acting as an intermediary, allowing easy manipulation and retrieval of data. 
```c#
 public class SimDbContext : DbContext
{
    public SimDbContext(DbContextOptions<SimDbContext> options) : base(options)
    {

    }
        public DbSet<Transaction> Transactions { get; set; }
}
```
DbSet<Transaction> Transactions property represents a database table named "Transactions," and Entity Framework automatically maps this DbSet to the corresponding table in the database.

✍🏻 Creat a GenericRepository

By gathering the methods used in the defined models into a central repository and creating inheritance from it, we will achieve clean code. For this reason, we create an interface (IGenericRepository) to collect the signatures of our methods. Later, we proceed to create the class (GenericRepository) for these methods.
```c#
public interface IGenericRepository<Entity> where Entity : class
{
    void Save();
    Entity GetById(int id);
    void Insert (Entity entity);
    void Update (Entity entity);    
    void Delete (Entity entity);
    void DeleteById(int id);
    List<Entity> GetAll();
    IQueryable<Entity> GetAllAsQueryable();
}
public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel 
{
       public IQueryable<Entity> GetAllAsQueryable()
    {
        return dbContext.Set<Entity>().AsQueryable();
    }
    .
    . //We are filling in the implementations of our methods here.
    .  
}
```

# Transaction 👩🏻‍💻
First, we create our class that contains the requested query parameters.

✏️ Schema Layer
```c#
public class TransactionSearchRequest  
{
    public int AccountNumber { get; set; }
    public decimal MinAccountCredit { get; set; }
    public decimal MaxAccountCredit { get; set; }
    public int MinAmountDebit { get; set; }
    public string? Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ReferenceNumber { get; set; }
}
```
To ensure data compatibility between the TransactionSearchRequest class in the Schema layer and the Transaction class in the Data layer, we will utilize the AutoMapper library. By using AutoMapper, data transfer and transformation between Transaction and TransactionSearchRequest can be achieved more easily and with less code.

```bash
Install-Package AutoMapper
```
Startup.cs
```bash
var config = new MapperConfiguration(cfg =>
  { cfg.AddProfile(new MapperConfig()); });
services.AddSingleton(config.CreateMapper());

```
✍🏻 Using Mapper
```c#
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<TransactionRequest,Transaction>(); 
        CreateMap<Transaction, TransactionResponse>();
        CreateMap<TransactionSearchRequest, Transaction>().ReverseMap();
    }
}
```

After returning to our Data layer, we inherit from the previously created GenericRepository and implement the requested GetByParameter method.(First, of course, we create the interface that will carry the signature of the method !)
```c#
public interface ITransactionRepository : IGenericRepository<Transaction>
{
     List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression);   //
}
```
⚡ I used a list because I expected multiple results, as specified in the task. In my opinion, unless it's a 'ById' method returning a single result, keeping the return type as a list is better ⚡
```c# 
public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly SimDbContext dbContext;
    public TransactionRepository(SimDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression)
    {
        throw new NotImplementedException();
    }
```
This method can be used to retrieve the objects of the Transaction class that meet a specific criterion. The method takes a parameter of type Expression<Func<Transaction, bool>>, which is an expression in the form of a LINQ query, specifying a condition to filter the instances of the Transaction class.

## API LAYER 🌱
✍🏻 The usage of the 'GetByParameter' API that we created on the TransactionController 💬

TransactionController
```c# 
   [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository repository;
        private readonly IMapper mapper;
        public TransactionController(ITransactionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
```
ITransactionRepository repository: Data layer for accessing and manipulating Transaction data.
 
IMapper mapper: It is a field that utilizes AutoMapper to handle the transformation and mapping of Transaction data.

✍🏻 And, it's time to use the method we created. 
```c# 
    [HttpGet("GetByParameter")]
        public ApiResponse<List<TransactionResponse>> GetByParameter([FromQuery] TransactionSearchRequest request)
        {
           Expression<Func<Transaction, bool>> expression =
            a => a.AccountNumber == request.AccountNumber
            && a.ReferenceNumber == request.ReferenceNumber
            && (a.CreditAmount >= request.MinAccountCredit && a.CreditAmount <= request.MaxAccountCredit)
            && (a.CreditAmount >= request.MinAmountDebit)
            && (request.BeginDate == null || a.TransactionDate >= request.BeginDate)
            && (request.EndDate == null || a.TransactionDate <= request.EndDate);


            var entityList = repository.GetByParameter(expression);
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }
    }
```
👍🏻 Sure, let's explain the expressions one by one: 
```c# 
    a => a.AccountNumber == request.AccountNumber
    && a.ReferenceNumber == request.ReferenceNumber
```
This expression checks if the AccountNumber property of Transaction object a is equal to the AccountNumber property of the request object, and also checks if the ReferenceNumber property of Transaction object a is equal to the ReferenceNumber property of the request object.

```c# 
   a.CreditAmount >= request.MinAccountCredit && a.CreditAmount <= request.MaxAccountCredit)
   && (a.CreditAmount >= request.MinAmountDebit)
```

CreditAmount should be within the range of request.MinAccountCredit and request.MaxAccountCredit and CreditAmount should be greater than or equal to request.MinAmountDebit.
Transaction objects that satisfy all these conditions will be included in the result list.
```c# 
   request.BeginDate == null || a.TransactionDate >= request.BeginDate)
   && (request.EndDate == null || a.TransactionDate <= request.EndDate);
```
With these expressions, we aim to filter the Transaction objects based on specific date intervals. So, the TransactionDate property will be included in the result list if it falls within the specified start and end dates.

As a result, after satisfying these conditions, it converts the Transaction objects to TransactionResponse objects and returns the result as an API response.

That's it! 🤟🏼 I hope it was clear. See you soon!  👩🏼‍🦰 🧡 


