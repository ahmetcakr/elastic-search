﻿using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace Elasticsearch.API.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetAsync(string id)
    {
        return await _productRepository.GetAsync(id);
    }

    public async Task < ResponseDto < List < ProductDto > > > GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        var productListDto = products.Select(x => x.CreateDto()).ToList();

        return ResponseDto < List < ProductDto > >.Success(productListDto, HttpStatusCode.OK);

    }

    public async Task<IEnumerable<Product>> SearchAsync(string query)
    {
        return await _productRepository.SearchAsync(query);
    }

    public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDTO product)
    {
        var response = await _productRepository.SaveAsync(product.CreateProduct());

        if (response == null)
        {
            return ResponseDto<ProductDto>.Fail("Kayıt esnasında hata meydana geldi", HttpStatusCode.InternalServerError);
        }

        return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.OK);
    }
}
