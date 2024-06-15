using AutoMapper;
using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Application.Products.Commands;
using CleanArchMVC.Application.Products.Queries;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interface;
using MediatR;

namespace CleanArchMVC.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productQuery = new GetProductsQuery();

            if (productQuery is null)
                throw new Exception("Entity could not be loaded");
            
            var result = await _mediator.Send(productQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery is null)
                throw new Exception("Entity could not be found");

            var result = await _mediator.Send(productByIdQuery);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task AddAsync(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task RemoveAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand is null)
                throw new Exception("Entity could not be found");

            await _mediator.Send(productRemoveCommand);
        }

        public Task<ProductDTO> GetProductCategoryAsync(int? id)
        {
            throw new NotImplementedException();
        }

        //private IProductRepository _productRepository;
        //private readonly IMapper _mapper;

        //public ProductService(IProductRepository productRepository, IMapper mapper)
        //{
        //    _productRepository = productRepository ?? 
        //        throw new ArgumentNullException(nameof(productRepository));
        //    _mapper = mapper;
        //}

        //public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        //{
        //    var productsEntity =  await _productRepository.GetProductsAsync();
        //    return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        //}

        //public async Task<ProductDTO> GetProductByIdAsync(int? id)
        //{
        //    var productEntity = await _productRepository.GetProductsAsync();
        //    return _mapper.Map<ProductDTO>(productEntity);
        //}

        //public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        //{
        //    var productEntity = await _productRepository.GetProductByIdAsync(id);
        //    return _mapper.Map<ProductDTO>(productEntity);
        //}

        //public async Task AddAsync(ProductDTO productDTO)
        //{
        //    var productEntity = _mapper.Map<Product>(productDTO);
        //    await _productRepository.CreateAsync(productEntity);
        //}

        //public async Task UpdateAsync(ProductDTO productDTO)
        //{
        //    var productEntity = _mapper.Map<Product>(productDTO);
        //    await _productRepository.UpdateAsync(productEntity);
        //}

        //public async Task RemoveAsync(int? id)
        //{
        //    var productEntity = _productRepository.GetProductByIdAsync(id).Result;
        //    await _productRepository.DeleteAsync(productEntity);
        //}
    }
}
