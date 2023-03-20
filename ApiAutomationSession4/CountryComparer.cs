using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomationSession4
{
    public class CountryComparer : IEqualityComparer<ServiceReference1.tCountryCodeAndName>
    {
        public bool Equals(tCountryCodeAndName? x, tCountryCodeAndName? y)
        {
            return x.sISOCode == y.sISOCode;
        }

        public int GetHashCode(tCountryCodeAndName obj)
        {
            return obj.sISOCode.GetHashCode();
        }
    }
}
