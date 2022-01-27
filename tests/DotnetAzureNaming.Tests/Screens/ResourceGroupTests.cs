using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class ResourceGroupTests
    {
        [LoFu, Test]
        public void Transform()
        {
            void should_transform_resource_group_name_from_just_project_name()
            {
                // act
                var result = ResourceGroup.Transform("klabbet", "", Environments.All().Single(x => x.Abbr == "stage"));

                // assert
                Expect(result).To.Equal("klabbet-stage-rg");
            }

            void should_transform_resource_group_name_from_project_and_component_name()
            {
                // act
                var result = ResourceGroup.Transform("klabbet", "web", Environments.All().Single(x => x.Abbr == "prod"));

                // assert
                Expect(result).To.Equal("klabbet-web-prod-rg");
            }
        }
    }
}
