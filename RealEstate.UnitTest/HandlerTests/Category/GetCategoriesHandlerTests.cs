using NUnit.Framework.Legacy;
using RealEstate.Api.Handlers.Category;
using RealEstate.UnitTest.ModelBuilders;

namespace RealEstate.UnitTest;

[TestFixture]
public class WhenGetCategories: GetCategoriesHandlerTestContext
{
    protected override void SetUpData()
    {
        _categories = [
            new CategoryBuilder(1, "Category 1").Build(),
            new CategoryBuilder(2, "Category 2").Build(),
        ];
    }

    [Test]
    public async Task ShouldReturnValidDataAsync()
    {
        var result = (await _handler.Handle(_query, CancellationToken.None)).Categories;
        Assert.That(result, Is.Not.Empty);
        var resultList = result.ToList();
        Assert.That(resultList, Has.Count.EqualTo(2));
    }

}

public class GetCategoriesHandlerTestContext: TestContextBase
{
    protected GetCategoriesHandler _handler = null!;
    protected GetCategoriesQuery _query = null!;

    public override void SetUp()
    {
        base.SetUp();
        _query = new GetCategoriesQuery();
        _handler = new GetCategoriesHandler(_unitOfWork);

    }
}
