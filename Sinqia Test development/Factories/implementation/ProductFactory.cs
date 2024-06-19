using Sinqia_Test_development.Factories.interfaces;

namespace Sinqia_Test_development.Factories.implementation
{
    public class ProductFactory
    {
        public IProduct CreateProduct(string type)
        {
            return type switch
            {
                "A" => new ProductA(),
                "B" => new ProductB(),
                _ => throw new ArgumentException("Invalid product type"),
            };
        }
    }
}
