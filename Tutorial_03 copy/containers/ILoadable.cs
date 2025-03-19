using Tutorial_03.products;

namespace Tutorial_03.containers;

public interface ILoadable
{
    void Empty();
    void Load(Product product, int mass);
}