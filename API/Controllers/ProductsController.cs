using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> productTypesRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypesRepo = productTypesRepo;
            _productBrandsRepo = productBrandsRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
                var countSpect = new ProductWithFilterForCountSpecification(productParams);
                var products = await _productsRepo.GetListWithSpecAsync(spec);
                var totalItems = await _productsRepo.CountAsync(countSpect);

                var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
                return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(id);
                var product = await _productsRepo.GetEntityWithSpec(spec);

                if (product == null) return NotFound(new ApiResponse(404));

                ProductToReturnDto result = _mapper.Map<Product, ProductToReturnDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            try
            {
                var brands = await _productBrandsRepo.GetListAllAsync();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            try
            {
                var types = await _productTypesRepo.GetListAllAsync();

                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}