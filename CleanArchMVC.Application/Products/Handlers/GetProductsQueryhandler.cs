using CleanArchMVC.Application.Products.Queries;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interface;
using MediatR;

namespace CleanArchMVC.Application.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productrepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productrepository = productRepository ?? 
                throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productrepository.GetProductsAsync();
        }
    }
}
