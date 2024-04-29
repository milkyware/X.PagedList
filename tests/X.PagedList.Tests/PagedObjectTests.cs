using Bogus;
using System.Collections.Generic;
using System.Text.Json;

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

        [Fact]
        public void PagedObjectTest_Serialization()
        {
            var pageSize = 2;

            var models = new List<Model>
            {
                new Model
                {
                    Value = "Hello"
                },
                new Model
                {
                    Value = "World"
                },
                new Model
                {
                    Value = "Red"
                },
                new Model
                {
                    Value = "Blue"
                },
                new Model
                {
                    Value = "Green"
                }
            };
            var pagedObject = new PagedObject<Model>(models, 1, pageSize);
            var json = JsonSerializer.Serialize(pagedObject);
            json.Should()
                .Be("{\"MetaData\":{\"PageCount\":3,\"TotalItemCount\":5,\"PageNumber\":1,\"PageSize\":2,\"HasPreviousPage\":false,\"HasNextPage\":true,\"IsFirstPage\":true,\"IsLastPage\":false,\"FirstItemOnPage\":1,\"LastItemOnPage\":2},\"Items\":[{\"Value\":\"Hello\"},{\"Value\":\"World\"}]}");
        }

        internal class Model
        {
            public string Value { get; set; }
        }
    }
}