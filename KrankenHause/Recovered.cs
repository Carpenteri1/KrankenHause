using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    class Recovered : IPatient
    {
        [Key]
        public int RecoverdId { get; set; }
        public int LastFourDigits { get; set; }
        public int? Age { get; set; }
        [Column(TypeName = "date")]
        public DateTime SocialSecurityNum { get; set; }
        public int? SymtomsLevel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"Firstname: {FirstName} LastName: {LastName} Age: {Age} ID{SocialSecurityNum.ToString("yyyy/MM/dd")}-{SocialSecurityNum} SymtomLevel: {SymtomsLevel}";
        }
    }
}
