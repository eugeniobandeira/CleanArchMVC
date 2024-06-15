using CleanArchMVC.Application.Products.Commands;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interface;
using MediatR;

namespace CleanArchMVC.Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product is null)
            {
                throw new ArgumentException($"Entity could not be found");
            } else
            {
                var result = await _productRepository.DeleteAsync(product);
                return result;
            }
        }
    }
}
