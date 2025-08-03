//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using OP.Prueba.Application.Interfaces;
//using OP.Prueba.Persistence.Context;
//using Microsoft.EntityFrameworkCore.SqlServer;


//namespace OP.Prueba.Persistence.Factories
//{
//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//                .AddJsonFile("appsettings.json", optional: false)
//                .Build();

//            var connectionString = configuration.GetConnectionString("PruebaConnection");

//            optionsBuilder.UseSqlServer(connectionString);

//            return new ApplicationDbContext(optionsBuilder.Options, new DesignTimeDateTimeService());
//        }

//        private class DesignTimeDateTimeService : IDateTimeService
//        {
//            public DateTime NowUtc => DateTime.UtcNow;
//        }
//    }
//}
