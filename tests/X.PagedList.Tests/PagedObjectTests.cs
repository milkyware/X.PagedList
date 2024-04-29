using Bogus;

namespace X.PagedList.Tests
{
    public class PagedObjectTests
    {
        [Theory()]
        [InlineData(1000, 50)]
        [InlineData(5000, 20)]
        public void PagedObjectTest(int supersetSize, int pageSize)
        {
            var faker = new Faker<Model>();
            var fakes = faker.Generate(supersetSize);
            var pagedObject = new PagedObject<Model>(fakes, 1, pageSize);
            pagedObject.Items.Should()
                .HaveCount(pageSize);
            pagedObject.MetaData.TotalItemCount
                .Should()
                .Be(supersetSize);
            pagedObject.MetaData.PageCount
                .Should()
                .Be(supersetSize / pageSize);
        }

        private class Model
        {
            private string Value { get; set; }
        }
    }
}