using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class EnvironmentsTests
    {
        [LoFu, Test]
        public void All()
        {
            void should_return_all_environments()
            {
                // act
                var result = Environments.All();

                // assert
                Expect(result[0].Name).To.Equal("Development");
                Expect(result[1].Name).To.Equal("Test");
                Expect(result[2].Name).To.Equal("Staging");
                Expect(result[3].Name).To.Equal("Production");
            }
        }
    }
}
