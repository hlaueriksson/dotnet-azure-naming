using LoFuUnit.NUnit;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace DotnetAzureNaming.Tests
{
    public class TransformerTests
    {
        [LoFu, Test]
        public void transform()
        {
            void should_trim_the_string()
            {
                // arrange
                var input = " helloworld ";

                // act
                var result = Transformer.Transform(input);

                // assert
                Expect(result).To.Equal("helloworld");
            }

            void should_lower_case_the_string()
            {
                // arrange
                var input = "HELLO World";

                // act
                var result = Transformer.Transform(input);

                // assert
                Expect(result).To.Equal("hello-world");
            }

            void should_replace_whitespaces_with_dash()
            {
                // arrange
                var input = "hello   world";

                // act
                var result = Transformer.Transform(input);

                // assert
                Expect(result).To.Equal("hello-world");
            }

            void should_replace_non_latin1_characters_with_latin1_characters()
            {
                // arrange
                var input = "Hällö Wårld";

                // act
                var result = Transformer.Transform(input);

                // assert
                Expect(result).To.Equal("hallo-warld");
            }
        }

        [LoFu, Test]
        public void alphanumerics()
        {
            void should_replace_characters_not_letters_or_numbers()
            {
                // arrange
                var input = " h.e{l}l[o_w!o:r;L&d1";

                // act
                var result = Transformer.Transform(input, "alphanumerics");

                // assert
                Expect(result).To.Equal("helloworld1");
            }
        }

        [LoFu, Test]
        public void alphanumericsHyphens()
        {
            void should_replace_characters_not_alphanumerics_letters_or_hyphens()
            {
                // arrange
                var input = "hello world 23";

                // act
                var result = Transformer.Transform(input, "alphanumericsHyphens");

                // assert
                Expect(result).To.Equal("hello-world-23");
            }
        }

        [LoFu, Test]
        public void alphanumericsUnderscores()
        {
            void should_replace_characters_not_alphanumerics_letters_or_underscores()
            {
                // arrange
                var input = "Hello World_45";

                // act
                var result = Transformer.Transform(input, "alphanumericsUnderscores");

                // assert
                Expect(result).To.Equal("helloworld_45");
            }
        }

        [LoFu, Test]
        public void alphanumericsUnderscoresHyphens()
        {
            void should_replace_characters_not_alphanumerics_letters_underscores_or_hyphens()
            {
                // arrange
                var input = "Hello World_45";

                // act
                var result = Transformer.Transform(input, "alphanumericsUnderscoresHyphens");

                // assert
                Expect(result).To.Equal("hello-world_45");
            }
        }

        [LoFu, Test]
        public void alphanumericsUnderscoresPeriodsHyphens()
        {
            void should_replace_characters_not_alphanumerics_letters_underscores_periods_or_hyphens()
            {
                // arrange
                var input = "Hallå du, siffror 123 går bra Även _ och -.";

                // act
                var result = Transformer.Transform(
                  input,
                  "alphanumericsUnderscoresPeriodsHyphens"
                );

                // assert
                Expect(result).To.Equal("halla-du-siffror-123-gar-bra-aven-_-och-.");
            }
        }

        [LoFu, Test]
        public void labelsAlphanumericsUnderscoresHyphens()
        {
            void should_format_as_a_domain_name()
            {
                // arrange
                var input = "Klabbet Blog Web";

                // act
                var result = Transformer.Transform(
                  input,
                  "labelsAlphanumericsUnderscoresHyphens"
                );

                // assert
                Expect(result).To.Equal("klabbet.blog.web");
            }
        }
    }
}
