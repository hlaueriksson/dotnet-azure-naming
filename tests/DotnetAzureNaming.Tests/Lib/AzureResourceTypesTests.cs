using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class AzureResourceTypesTests
    {
        [LoFu, Test]
        public void findTransformerName()
        {
            void should_find_transformer__alphanumerics__for_Front_Door_firewall_policy()
            {
                // arrange
                var resourceType = "fdfp";

                // act
                var transformerName = AzureResourceTypes.FindTransformerName(resourceType);

                // arrange
                Expect(transformerName).To.Equal("alphanumerics");
            }
        }

        [LoFu, Test]
        public void findValidations()
        {
            void should_find_validations_for_Front_Door_firewall_policy()
            {
                // arrange
                var resourceType = "fdfp";

                // act
                var validations = AzureResourceTypes.FindValidations(resourceType);

                // arrange
                Expect(validations).To.Contain("startWithLetter");
            }
        }
    }
}
