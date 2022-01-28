using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class SettingsTests
    {
        [LoFu, Test]
        public void Current()
        {
            void should_get_default_settings_when_current_has_not_been_set()
            {
                // act
                var result = Settings.Current;

                // assert
                Expect(result.AzureResourceTypesPath).To.Equal("azure-resource-types.csv");
                Expect(result.Environments.Select(x => x.Abbr)).To.Equal(new[] { "dev", "test", "stage", "prod" });
                Expect(result.ResourceNameFormat).To.Equal("{projectName} {componentName} {environment} {resourceType}");
                Expect(result.ResourceGroupFormat).To.Equal("{projectName} {componentName} {environment} rg");
            }

            void should_get_new_settings_when_current_has_been_set()
            {
                // act
                Settings.Current = new Settings { Environments = new[] { new Environment { Name = "Pre-Production", Abbr = "preprod" }, new Environment { Name = "Production", Abbr = "prod" } } };
                var result = Settings.Current;

                // assert
                Expect(result.AzureResourceTypesPath).To.Equal("azure-resource-types.csv");
                Expect(result.Environments.Select(x => x.Abbr)).To.Equal(new[] { "preprod", "prod" });
                Expect(result.ResourceNameFormat).To.Equal("{projectName} {componentName} {environment} {resourceType}");
                Expect(result.ResourceGroupFormat).To.Equal("{projectName} {componentName} {environment} rg");
            }
        }
    }
}
