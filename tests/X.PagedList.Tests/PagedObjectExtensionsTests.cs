using Bogus;
using System.Linq;

namespace X.PagedList.Tests
{
    public class PagedObjectExtensionsTests
    {
        [Fact()]
        public void ToPagedObjectTest()
        {
            var supersetSize = 1000;
            var pageSize = 50;

            var faker = new Faker<Model>();
            var fakes = faker.Generate(supersetSize);
            var pagedObject = fakes.ToPagedObject(1, pageSize);
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

        [Fact()]
        public void ToPagedObjectTest1()
        {
            var supersetSize = 1000;
            var pageSize = 50;

            var faker = new Faker<Model>();
            var fakes = faker.Generate(supersetSize)
                .AsQueryable();
            var pagedObject = fakes.ToPagedObject(1, pageSize);
            pagedObject.Items.Should()
                .HaveCount(pageSize);
            pagedObject.MetaData.TotalItemCount
                .Should()
                .Be(supersetSize);
            pagedObject.MetaData.PageCount
                .Should()
                .Be(supersetSize / pageSize);
        }
    }
}