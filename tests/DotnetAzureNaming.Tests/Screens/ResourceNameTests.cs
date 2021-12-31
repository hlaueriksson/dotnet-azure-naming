using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class ResourceNameTests
    {
        [LoFu, Test]
        public void Validate()
        {
            void should_display_validation_error_when_resource_name_doesnt_validate()
            {
                // act
                var result = ResourceName.Validate("-klabbet", AzureResourceTypes.All().Single(x => x.Abbr == "mysql"));

                // assert
                Expect(result.Message).To.Equal("Resource name must start with an alphanumeric character");
            }
        }
    }
}
