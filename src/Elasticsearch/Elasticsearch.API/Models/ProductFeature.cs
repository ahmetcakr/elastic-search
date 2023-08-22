﻿using Elasticsearch.API.DTOs;

namespace Elasticsearch.API.Models;

public class ProductFeature
{
    public int Width { get; set; }
    public int Height { get; set; }
    public EColor Color { get; set; }

    public ProductFeatureDto CreateDto()
    {
        return new ProductFeatureDto(Width, Height, Color);
    }
}
