using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using NaTourWine.Core.Repositories.NoSql.Data;
using NoSql.MongoDb.Abstraction.Interfaces;
using NaTourWine.Core.Repositories.NoSql.Querying;
using NoSql.MongoDb.Context;
using NoSql.MongoDb.NameConventions;
using NoSql.MongoDb.Repository;
using NoSql.MongoDb.Types;

namespace NoSql.MongoDb
{
    public static class StartupConfiguration
    {
        public static IServiceCollection AddNoSqlRepositoryConfiguration<TSession>(this IServiceCollection services, IConfiguration configuration, bool withCamelCase = true) where TSession : class, INoSqlSessionProvider
        {
            if (withCamelCase)
            {
                var pack = new ConventionPack { new CamelCaseNameConvention() };
                ConventionRegistry.Register("CamelCase", pack, _ => true);
            }

            services
                .AddHttpContextAccessor()
                .Configure<MongoConnection>(option => configuration.GetSection(nameof(MongoConnection)).Bind(option))
                .AddSingleton<IMongoClientService, MongoClientService>()
                .AddScoped<INoSqlSessionProvider, TSession>()
                .AddTransient(typeof(IPipelineQuerying<,>), typeof(PipelineQuerying<,>))
                .AddTransient(typeof(INoSqlDBContext<>), typeof(NoSqlDBContext<>))
                .AddTransient(typeof(INoSqlRepository<>), typeof(NoSqlRepository<>));

            return services;
        }
    }
}
