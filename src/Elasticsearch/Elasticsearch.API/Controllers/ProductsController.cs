using Elasticsearch.API.DTOs;
using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Elasticsearch.API.Controllers;

public class ProductsController : BaseController
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult( await _productService.GetAllAsync() ); 
    }

    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDTO productCreate)
    {
        return CreateActionResult( await _productService.SaveAsync(productCreate) );
    }

}
