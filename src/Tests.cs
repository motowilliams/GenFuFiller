using System;
using GenFu;
using NUnit.Framework;
using GenFu.ValueGenerators.Internet;

namespace GenFuFiller
{
    public class CityFillerTest
    {
        [Test]
        public void should_return_city()
        {
            A.Reset();
            A.Default().FillerManager.RegisterFiller(new CityNameFiller());

            var loanDocument = A.New<Document>();

            Assert.AreEqual(loanDocument.TitleCity, "Kalamazoo");
        }

        [Test]
        public void should_return_uri()
        {
            A.Reset();
            A.Default().FillerManager.RegisterFiller(new WebAddressFiller());

            var loanDocument = A.New<Document>();

            Assert.True(loanDocument.Website.StartsWith("https://www."));
        }
    }

    public class Document
    {
        public string TitleCity { get; set; }
        public string Website { get; set; }
    }

    public class CityNameFiller : PropertyFiller<string>
    {
        public CityNameFiller()
            : base(
                    new[] { "object" },
                    new[] { "TitleCity" })
        {
        }

        public override object GetValue(object instance)
        {
            return "Kalamazoo";
        }
    }
    public class WebAddressFiller : PropertyFiller<string>
    {
        public WebAddressFiller()
            : base(
                    new[] { "object" },
                    new[] { "website", "web", "webaddress" })
        {
        }

        public override object GetValue(object instance)
        {
            var domain = Domains.DomainName();

            return $"https://www.{domain}";
        }
    }
}
