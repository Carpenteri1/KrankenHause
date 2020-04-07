using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    interface IPatient
    {
        int LastFourDigits { get; set; }
        int? Age { get; set; }
        DateTime SocialSecurityNum { get; set; }
        int? SymtomsLevel { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string ToString();
    }
}
