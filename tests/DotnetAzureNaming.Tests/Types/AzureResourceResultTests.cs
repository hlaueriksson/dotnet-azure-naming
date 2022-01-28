using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class AzureResourceResultTests
    {
        [LoFu, Test]
        public void ResourceGroup()
        {
            void should_transform_resource_group_name_from_just_project_name()
            {
                // act
                var result = new AzureResourceResult("klabbet", "", Environments.All().Single(x => x.Abbr == "stage"), AzureResourceTypes.All().First()).ResourceGroup;

                // assert
                Expect(result).To.Equal("klabbet-stage-rg");
            }

            void should_transform_resource_group_name_from_project_and_component_name()
            {
                // act
                var result = new AzureResourceResult("klabbet", "web", Environments.All().Single(x => x.Abbr == "prod"), AzureResourceTypes.All().First()).ResourceGroup;

                // assert
                Expect(result).To.Equal("klabbet-web-prod-rg");
            }
        }

        [LoFu, Test]
        public void ValidationResult()
        {
            void should_display_validation_error_when_resource_name_doesnt_validate()
            {
                // act
                var result = new AzureResourceResult("-klabbet", "", Environments.All().First(), AzureResourceTypes.All().Single(x => x.Abbr == "mysql")).ValidationResult;

                // assert
                Expect(result.Message).To.Equal("Resource name must start with an alphanumeric character");
            }
        }
    }
}
