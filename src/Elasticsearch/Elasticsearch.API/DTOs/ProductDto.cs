using Elasticsearch.API.Models;
using Nest;

namespace Elasticsearch.API.DTOs;

public record ProductDto(string id, string name, decimal price, int Stoack, ProductFeatureDto feature)
{
}
