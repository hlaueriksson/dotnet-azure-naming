using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class ValidatorTests
    {
        [LoFu, Test]
        public void startWithLetter()
        {
            void should_be_valid_when_resource_name_starts_with_letter()
            {
                // arrange
                var resourceName = "klabbet-web-dev-aa";

                // act
                var result = Validator.startWithLetter(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_starts_with_a_number()
            {
                // arrange
                var resourceName = "0klabbet-web-dev-aa";

                // act
                var result = Validator.startWithLetter(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("startWithLetter");
            }
        }

        [LoFu, Test]
        public void startWithAlphanumeric()
        {
            void should_be_valid_when_resource_name_starts_with_number()
            {
                // arrange
                var resourceName = "0klabbet-web-dev-aks";

                // act
                var result = Validator.startWithAlphanumeric(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_starts_with_a_dash()
            {
                // arrange
                var resourceName = "-klabbet-web-dev-aks";

                // act
                var result = Validator.startWithAlphanumeric(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("startWithAlphanumeric");
            }
        }

        [LoFu, Test]
        public void atLeast2Labels()
        {
            void should_be_valid_when_resource_name_has_two_labels_separated_by_a_dot()
            {
                // arrange
                var resourceName = "klabbet.web.prod.dnsz";

                // act
                var result = Validator.atLeast2Labels(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_has_only_one_label()
            {
                // arrange
                var resourceName = "localhost";

                // act
                var result = Validator.atLeast2Labels(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("atLeast2Labels");
            }
        }

        [LoFu, Test]
        public void endWithAlphanumericOrUnderscore()
        {
            void should_be_valid_when_resource_name_ends_with_underscore()
            {
                // arrange
                var resourceName = "AzureServiceEscapePlan_";

                // act
                var result = Validator.endWithAlphanumericOrUnderscore(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_ends_with_a_period()
            {
                // arrange
                var resourceName = "AzureServiceEscapePlan.";

                // act
                var result = Validator.endWithAlphanumericOrUnderscore(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("endWithAlphanumericOrUnderscore");
            }
        }

        [LoFu, Test]
        public void endWithAlphanumeric()
        {
            void should_be_valid_when_resource_name_ends_with_number()
            {
                // arrange
                var resourceName = "AzureServiceEscapePlan1";

                // act
                var result = Validator.endWithAlphanumeric(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_ends_with_an_underscore()
            {
                // arrange
                var resourceName = "AzureServiceEscapePlan_";

                // act
                var result = Validator.endWithAlphanumeric(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("endWithAlphanumeric");
            }
        }

        [LoFu, Test]
        public void maxLength()
        {
            void should_be_valid_when_resource_name_is_within_maxLength()
            {
                // arrange
                var resourceName = "klabbet-web-stage-appi";

                // act
                var result = Validator.maxLengthValidator(63)(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_valid_when_resource_name_is_exactly_maxLength()
            {
                // arrange
                var resourceName = "klabbet-web-stage-ase";

                // act
                var result = Validator.maxLengthValidator(resourceName.Length)(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_is_exceeding_maxLength()
            {
                // arrange
                var resourceName = "klabbet-web-stage-app-";

                // act
                var result = Validator.maxLengthValidator(15)(resourceName);

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("15characterLimit");
            }
        }

        [LoFu, Test]
        public void minLength()
        {
            void should_be_valid_when_resource_name_is_longer_than_min_length()
            {
                // arrange
                var resourceName = "klabbet-web-stage-app";

                // act
                var result = Validator.minLengthValidator(5)(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_valid_when_resource_name_is_exactly_at_min_length()
            {
                // arrange
                var resourceName = "klabbet-web-stage-app";

                // act
                var result = Validator.minLengthValidator(resourceName.Length)(resourceName);

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_resource_name_is_shorter_than_min_length()
            {
                // arrange
                var resourceName = "web";

                // act
                var result = Validator.minLengthValidator(5)(resourceName);

                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("atLeast5Characters");
            }
        }

        [LoFu, Test]
        public void validate()
        {
            void should_be_valid_when_web_app_has_less_than_60_characters()
            {
                // arrange
                var resourceName = "klabbet-web-stage-app";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("app"));

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_web_app_has_more_than_60_characters()
            {
                // arrange
                var resourceName =
                  "klabbet-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-web-stage-app";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("app"));

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("60characterLimit");
            }

            void should_be_valid_when_web_app_has_more_than_2_characters()
            {
                // arrange
                var resourceName = "web";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("app"));

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_web_app_has_less_than_2_characters()
            {
                // arrange
                var resourceName = "w";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("app"));

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("atLeast2Characters");
            }

            void should_be_valid_when_dns_as_at_least_2_labels()
            {
                // arrange
                var resourceName = "klabbet.web.prod.dnsz";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("dnsz"));

                // assert
                Expect(result).To.Equal(true);
            }

            void should_be_invalid_when_dns_has_only_1_label()
            {
                // arrange
                var resourceName = "localhost";

                // act
                var result = Validator.Validate(resourceName, AzureResourceTypes.Find("dnsz"));

                // assert
                Expect(result).Not.To.Equal(true);
                Expect(result.ValidatorName).To.Equal("atLeast2Labels");
            }
        }
    }
}
