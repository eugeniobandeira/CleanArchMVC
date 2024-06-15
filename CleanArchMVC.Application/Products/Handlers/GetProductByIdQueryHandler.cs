using CleanArchMVC.Application.Products.Queries;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interface;
using MediatR;

namespace CleanArchMVC.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductCategoryAsync(request.Id);
        }
    }
}
