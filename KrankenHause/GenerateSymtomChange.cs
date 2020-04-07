using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    class GenerateSymtomChanges
    {
        public static void InLineSymtomChange()
        {
            Random gen = new Random();
            int perc;

            using (var db = new Context())
            {
                var grabPatient = from patient in db.InLine
                                  select patient;

                foreach (var s in grabPatient.ToList())
                {

                    perc = gen.Next(0, 101);
                    if (perc <= 50)//50%          
                    {
                        s.SymtomsLevel = s.SymtomsLevel;
                    }
                    else if (perc <= 10)//10%
                    {
                        s.SymtomsLevel -= 1;
                    }
                    else if (perc >= 60 && perc <= 80)//30%
                    {
                        s.SymtomsLevel += 1;
                    }
                    else if (perc >= 90)//10%
                    {
                        s.SymtomsLevel += 3;
                    }
                }
                try
                {
                    db.SaveChanges();
                }catch(DbUpdateConcurrencyException e)
                {
                    e.Entries.Single().Reload();
                }
                finally
                {

                }


            }
        }

        public static void IVASymtomChange()
        {
            Random gen = new Random();
            int perc;

            using (var db = new Context())
            {
                var grabPatient = from patient in db.IVA
                                  select patient;

                foreach (var s in grabPatient.ToList())
                {
                    perc = gen.Next(0, 101);
                    if (perc <= 60)//60%
                    {
                        s.SymtomsLevel -= 3;
                    }
                    else if (perc >= 60 && perc <= 80)//20%
                    {
                        s.SymtomsLevel = s.SymtomsLevel;
                    }
                    else if (perc >= 80 && perc <= 90)//10%
                    {
                        s.SymtomsLevel += 1;
                    }
                    else if (perc >= 90)//10%
                    {
                        s.SymtomsLevel += 2;
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        e.Entries.Single().Reload();
                    }
                    finally
                    {

                    }
                }
            }
        }


        public static void SanatoriumSymtomChange()
        {
            Random gen = new Random();
            int perc;

            using (var db = new Context())
            {
                var grabPatient = from patient in db.Sanatorium
                                  select patient;

                foreach (var s in grabPatient.ToList())
                {
                    perc = gen.Next(0, 101);
                    if (perc <= 65)//60%
                    {
                        s.SymtomsLevel = s.SymtomsLevel;
                    }
                    else if (perc >= 65 && perc <= 85)//20%
                    {
                        s.SymtomsLevel -= 1;
                    }
                    else if (perc >= 85 && perc <= 95)//10%
                    {
                        s.SymtomsLevel += 1;
                    }
                    else if (perc >= 95)//10%
                    {
                        s.SymtomsLevel += 3;
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        e.Entries.Single().Reload();
                    }
                    finally
                    {

                    }
                }
            }
        }
    }
}