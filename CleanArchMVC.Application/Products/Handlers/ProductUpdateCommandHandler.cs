﻿using CleanArchMVC.Application.Products.Commands;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interface;
using MediatR;

namespace CleanArchMVC.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product is null)
            {
                throw new ArgumentNullException($"Entity could not be found");
            } else
            {
                product.Update(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Stock,
                    request.Image,
                    request.CategoryId
                );

                return await _productRepository.UpdateAsync(product);
            }
        }
    }
}
