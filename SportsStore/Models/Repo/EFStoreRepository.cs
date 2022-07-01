using System.Linq;

namespace SportsStore.Models.Repo
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => context.Products;

        public void CreateProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Remove(product);
            context.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            context.SaveChanges();
        }
    }
}
