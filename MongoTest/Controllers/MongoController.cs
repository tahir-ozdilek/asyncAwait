using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace MongoTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MongoController : ControllerBase
    { 
        private readonly ILogger<MongoController> _logger;
        private readonly MongoDBService _mongoDBService;

        public MongoController(ILogger<MongoController> logger, MongoDBService mongoDBService)
        {
            _logger = logger;
            _mongoDBService = mongoDBService;
        }

        [HttpGet("MongoGetByUserIdAsync")]
        public async Task<List<testCollection>> MongoGetByUserIdAsync(int id)
        {
            return await _mongoDBService.GetByUserIdAsync(id);
        }

        [HttpGet("MongoGetAllAsync")]
        public async Task<List<testCollection>> MongoGetAllAsync()
        {
            return await _mongoDBService.GetAllAsync();
        }        
        
        [HttpGet("MongoEFGetAllAsync")]
        public async Task<List<testCollection>> MongoEFGetAllAsync()
        {
            return await _mongoDBService.GetAllEFAsync();
        }
    }

    public class testCollection
    {
        public dynamic _id { get; set; }
        public int userID { get; set; }
        public byte[] dosya { get; set; }
    }

    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }

    public class MongoDBService
    {
        private readonly IMongoCollection<testCollection> _data;
        private readonly MflixDbContext _efCore;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _data = database.GetCollection<testCollection>(mongoDBSettings.Value.CollectionName);
            _efCore = MflixDbContext.Create(database);
        }

        public async Task<List<testCollection>> GetByUserIdAsync(int id) { 
            return await _data.Find(t=> t.userID ==id).ToListAsync(); 
        }

        public async Task<List<testCollection>> GetAllAsync()
        {
            return await _data.Find(new BsonDocument()).ToListAsync(); ;
        }

        public async Task<List<testCollection>> GetAllEFAsync()
        {
            return await _efCore.testCollection.ToListAsync();
        }

        public async Task CreateAsync(object playlist) { }
        public async Task AddToPlaylistAsync(string id, string movieId) { }
        public async Task DeleteAsync(string id) { }
    }


    public class MflixDbContext : DbContext
    {
        public DbSet<testCollection> testCollection { get; init; }
        public static MflixDbContext Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuilder<MflixDbContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);

        public MflixDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<testCollection>().ToCollection("movies");
        }
    }
}
