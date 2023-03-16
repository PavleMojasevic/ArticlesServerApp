using ServerApp.Models;

namespace TestServerApp.MockData;

public class MockCategory
{
    private List<Category> _Categories = new()
    {
        new(){ Name="Category1",  Id=1},
        new(){ Name="Category2",  Id=2, ParentId=1}
    };
    public List<Category> GetCategories()
    {
        return _Categories;
    }
}
