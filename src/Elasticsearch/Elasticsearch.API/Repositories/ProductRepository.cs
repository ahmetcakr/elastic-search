using Elasticsearch.API.Models;
using Nest;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories;

public class ProductRepository
{
    private readonly ElasticClient _client;
    private readonly string _indexName = "products";

    public ProductRepository(ElasticClient client)
    {
        _client = client;
    }

    public async Task<Product> GetAsync(string id)
    {
        var response = await _client.GetAsync<Product>(id);

        return response.Source;
    }

    public async Task<ImmutableList<Product>> GetAllAsync()
    {
        var result = await _client.SearchAsync<Product>(
            s => s.Index(_indexName).Query(q => q.MatchAll()));


        foreach (var hit in result.Hits) 
            hit.Source.Id = hit.Id;

        return result.Documents.ToImmutableList();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string query)
    {
        var response = await _client.SearchAsync<Product>(s => s
                          .Query( q => q.Match( m => m.Field( f => f.Name ).Query( query ) ) ) );

        return response.Documents;
    }

    public async Task<Product> SaveAsync(Product product)
    {
        product.Created = DateTime.UtcNow;
        
        var response = await _client.IndexAsync(product,x=>x.Index(_indexName).Id(Guid.NewGuid().ToString()));

        //fast fail
        if (!response.IsValid) return null;

        product.Id = response.Id;

        return product;
    }
}
