using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    class Sanatorium : IPatient
    {
        [Key]
        public int SanatoriumId { get; set; }
        public int LastFourDigits { get; set; }
        public int? Age { get; set; }
        [Column(TypeName = "date")]
        public DateTime SocialSecurityNum { get; set; }
        public int? SymtomsLevel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
