using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;
using FluentAssertions;
using NUnit.Framework;

namespace DotnetAzureNaming.Tests
{
    public class IntegrationTests
    {
        [Test]
        public void Help()
        {
            var (ExitCode, StandardOutput, StandardError) = Run("--help");
            ExitCode.Should().Be(-1);
            StandardError.Should().NotBeEmpty();
            StandardOutput.Should().BeEmpty();
        }

        [Test]
        public void Version()
        {
            var (ExitCode, StandardOutput, StandardError) = Run("--version");
            ExitCode.Should().Be(-1);
            StandardError.Should().NotBeEmpty();
            StandardOutput.Should().BeEmpty();
        }

        [Test]
        public void Console()
        {
            var result = Run("-r AKS -p Titanic -c Web -e Development");
            result.ExitCode.Should().Be(0);
            result.StandardError.Should().BeEmpty();
            result.StandardOutput.Should().Be(
@"Resource Type: AKS cluster
Project Name:   Titanic
Component Name: Web
Environment:    Development

COMPUTED NAME:  titanic-web-dev-aks
RESOURCE GROUP: titanic-web-dev-rg
RESOURCE TAGS
Project:        Titanic
Component:      Web
Environment:    Development
");

            result = Run("-r AKS -p LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua -c Web -e Development");
            result.ExitCode.Should().Be(-1831741950);
            result.StandardError.Should().BeEmpty();
            result.StandardOutput.Should().Be(
@"Resource Type: AKS cluster
Project Name:   LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua
Component Name: Web
Environment:    Development

COMPUTED NAME:  loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-aks
Resource name must be at most 63 characters
RESOURCE GROUP: loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-rg
RESOURCE TAGS
Project:        LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua
Component:      Web
Environment:    Development
");

            result = Run("-r unknown -p Titanic -c Web -e Development");
            result.ExitCode.Should().Be(-40503431);
            result.StandardError.Should().BeEmpty();
            result.StandardOutput.Should().Be(
@"Resource Type*:
Project Name:   Titanic
Component Name: Web
Environment:    Development

COMPUTED NAME:  
Resource type is invalid
RESOURCE GROUP: 
RESOURCE TAGS
Project:        Titanic
Component:      Web
Environment:    Development
");
        }

        [Test]
        public void Json()
        {
            var result = Run("-r AKS -p Titanic -c Web -e Development -f json");
            result.ExitCode.Should().Be(0);
            result.StandardError.Should().BeEmpty();
            FromJson(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "Titanic",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ResourceType = AzureResourceTypes.Find("aks"),
                    ResourceName = "titanic-web-dev-aks",
                    ResourceGroup = "titanic-web-dev-rg",
                });

            result = Run("-r AKS -p LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua -c Web -e Development -f json");
            result.ExitCode.Should().Be(-1831741950);
            result.StandardError.Should().BeEmpty();
            FromJson(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ResourceType = AzureResourceTypes.Find("aks"),
                    ResourceName = "loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-aks",
                    ResourceGroup = "loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-rg",
                    ValidationResult = new ValidationResult { ValidatorName = "63characterLimit", Message = "Resource name must be at most 63 characters" },
                });

            result = Run("-r unknown -p Titanic -c Web -e Development -f json");
            result.ExitCode.Should().Be(-40503431);
            result.StandardError.Should().BeEmpty();
            FromJson(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "Titanic",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ValidationResult = new ValidationResult { Message = "Resource type is invalid" },
                });
        }

        [Test]
        public void Xml()
        {
            var result = Run("-r AKS -p Titanic -c Web -e Development -f xml");
            result.ExitCode.Should().Be(0);
            result.StandardError.Should().BeEmpty();
            FromXml(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "Titanic",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ResourceType = AzureResourceTypes.Find("aks"),
                    ResourceName = "titanic-web-dev-aks",
                    ResourceGroup = "titanic-web-dev-rg",
                });

            result = Run("-r AKS -p LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua -c Web -e Development -f xml");
            result.ExitCode.Should().Be(-1831741950);
            result.StandardError.Should().BeEmpty();
            FromXml(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "LoremIpsumDolorSitAmetConsecteturAdipiscingElitSedDoEiusmodTemporIncididuntUtLaboreEtDoloreMagnaAliqua",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ResourceType = AzureResourceTypes.Find("aks"),
                    ResourceName = "loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-aks",
                    ResourceGroup = "loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua-web-dev-rg",
                    ValidationResult = new ValidationResult { ValidatorName = "63characterLimit", Message = "Resource name must be at most 63 characters" },
                });

            result = Run("-r unknown -p Titanic -c Web -e Development -f xml");
            result.ExitCode.Should().Be(-40503431);
            result.StandardError.Should().BeEmpty();
            FromXml(result.StandardOutput).Should().BeEquivalentTo(
                new AzureResourceResult
                {
                    ProjectName = "Titanic",
                    ComponentName = "Web",
                    Environment = Environments.Find("dev"),
                    ValidationResult = new ValidationResult { Message = "Resource type is invalid" },
                });
        }

        static (int ExitCode, string StandardOutput, string StandardError) Run(string args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "DotnetAzureNaming.exe",
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            using (var exeProcess = Process.Start(startInfo))
            {
                exeProcess!.WaitForExit();
                return new(exeProcess.ExitCode, exeProcess.StandardOutput.ReadToEnd(), exeProcess.StandardError.ReadToEnd());
            }
        }

        static AzureResourceResult FromJson(string json) => JsonSerializer.Deserialize<AzureResourceResult>(json)!;

        static AzureResourceResult FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(AzureResourceResult));
            var reader = new StringReader(xml);
            return (AzureResourceResult)serializer.Deserialize(reader)!;
        }
    }
}
