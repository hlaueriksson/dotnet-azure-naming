using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class AzureResourceTypesTests
    {
        [LoFu, Test]
        public void Find()
        {
            void should_find_transformer__alphanumerics__for_Front_Door_firewall_policy()
            {
                // arrange
                var resourceType = "fdfp";

                // act
                var result = AzureResourceTypes.Find(resourceType);

                // arrange
                Expect(result.Transformer).To.Equal("alphanumerics");
            }

            void should_find_validations_for_Front_Door_firewall_policy()
            {
                // arrange
                var resourceType = "fdfp";

                // act
                var result = AzureResourceTypes.Find(resourceType);

                // arrange
                Expect(result.Validations).To.Contain("startWithLetter");
            }
        }
    }
}
