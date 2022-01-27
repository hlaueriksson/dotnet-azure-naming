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
                // assert
                Expect(Environments.All()[0].Name).To.Equal("Development");
                Expect(Environments.All()[1].Name).To.Equal("Test");
                Expect(Environments.All()[2].Name).To.Equal("Staging");
                Expect(Environments.All()[3].Name).To.Equal("Production");
            }
        }
    }
}
