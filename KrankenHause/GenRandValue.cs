using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomNameGenerator;
namespace KrankenHause
{
    /// <summary>
    /// Class for generating random values , using RandomNameGenerator taken from NuGetPackageManager to generate names.
    /// </summary>
    class GenRandValue
    {
        private static Random rand;
        private static int maleOrFemaleNum;
        public static string FirstNameMale()
        {
            string name = string.Empty;
            name = NameGenerator.GenerateFirstName(Gender.Male);
            return name;
        }

        public static string FirstNameFemale()
        {
            string name = string.Empty;
            name = NameGenerator.GenerateFirstName(Gender.Female);
            return name;
        }

        public static string LastName()
        {
            string name = string.Empty;
            name = NameGenerator.GenerateLastName();
            return name;
        }

        public static DateTime SocialSecurityNum()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }

        public static int SymtomLevel()
        {
            rand = new Random();
            int lvl = rand.Next(1, 11);
            return lvl;
        }

        public static SetSex GetSex()
        {
            if (maleOrFemaleNum % 2 == 0)
            {
                
                return SetSex.Female;
            }
            return SetSex.Male;
        }

        public static int LastFourDigits()
        {
            rand = new Random();
            string value = string.Empty;
            maleOrFemaleNum = rand.Next(0, 10);
            int birthPlace = rand.Next(10, 99);
            int controlNum = rand.Next(0, 10);




            value += birthPlace.ToString();
            value += maleOrFemaleNum.ToString();
            value += controlNum.ToString();
            int digits = int.Parse(value);

            while (LastFourDigitAlreadyExcist(digits))//will loop untill a uniqe id is found
            {
                LastFourDigits();
            }

            return digits;
        }

        private static bool LastFourDigitAlreadyExcist(int digits)
        {
            using (var db = new Context())
            {
                var checkDigit = from patient in db.InLine
                                 where patient.LastFourDigits == digits
                                 select patient;

                if (checkDigit.Count() == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
