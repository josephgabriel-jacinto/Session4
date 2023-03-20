using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomationSession4
{
    [TestClass]
    public class CountryInfoTests
    {
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoClient = new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ValidateAscendingOrderByCountryCode()
        {
            var listOfCountry = countryInfoClient.ListOfCountryNamesByCode().ToList();
            var orderedListOfCountry = countryInfoClient.ListOfCountryNamesByCode().ToList();

            //order by ascending by sISOCode
            orderedListOfCountry.Sort((x, y) => x.sISOCode.CompareTo(y.sISOCode));
            
            Assert.IsTrue(Enumerable.SequenceEqual(orderedListOfCountry, listOfCountry, new CountryComparer()), "List of country mismatched");
        }


        [TestMethod]
        public void ValidateInvalidCountryCode()
        {
            var getResponse = countryInfoClient.CountryName("XXX");
            Assert.AreEqual("Country not found in the database", getResponse, "Incorrect response for Invalid Country sISOCode");
        }


        [TestMethod]
        public void ValidateCountryNameByLastCountryCode()
        {
            //get list of countries
            var countryList = countryInfoClient.ListOfCountryNamesByCode();

            //get last entry
            var lastCountry = countryList.Last();
            string lastCountryCode = lastCountry.sISOCode;

            //use last entry code to get country name
            var countryName = countryInfoClient.CountryName(lastCountryCode);

            Assert.AreEqual(lastCountry.sName, countryName, "Country name mismatched");
        }

    }
}
