using Elasticsearch.API.Models;
using Nest;

namespace Elasticsearch.API.Repositories;

public class ProductRepository
{
    private readonly ElasticClient _client;

    public ProductRepository(ElasticClient client)
    {
        _client = client;
    }

    public async Task<Product> GetAsync(string id)
    {
        var response = await _client.GetAsync<Product>(id);

        return response.Source;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var response = await _client.SearchAsync<Product>(s => s
                   .MatchAll()
                          );

        return response.Documents;
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
        
        var response = await _client.IndexAsync(product,x=>x.Index("products"));

        //fast fail
        if (!response.IsValid) return null;

        product.Id = response.Id;

        return product;
    }
}
