using Elasticsearch.Net;
using Nest;

namespace Elasticsearch.API.Extensions;

public static class Elasticsearch
{
    public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var poll = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));

        var settings = new ConnectionSettings(poll);

        var client = new ElasticClient(settings);

        services.AddSingleton(client);
    }
}
