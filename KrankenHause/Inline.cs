using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    class InLine : IPatient
    {
        [Key]
        public int InlineId { get; set;}
        public int LastFourDigits { get; set; }
        public int? Age
        {
            get
            {
                int years;
                int now = DateTime.Now.Year;
                int socialNum = SocialSecurityNum.Year;
                years = now - socialNum;
                return years;
            }
            set {; }
        }
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
