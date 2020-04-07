using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KrankenHause
{
    class Context : DbContext
    {
        public DbSet<InLine> InLine{ get; set; }
        public DbSet<IVA> IVA { get; set; }
        public DbSet<Sanatorium> Sanatorium { get; set; }
        public DbSet<Recovered> Recovereds { get; set; }
        public DbSet<AfterLife> AfterLives { get; set; }
        
        /// <summary>
        /// Simple push theList to the database with foreach loop;
        /// </summary>
        /// <param name="theList"></param>
        /// <param name="sendTo"></param>
        /// <returns></returns>
        public static List<IPatient> PushToDataBase (List<IPatient> theList,IPatient sendTo)
        {
            if (sendTo.GetType() == typeof(InLine))
            {
                using (Context db = new Context())
                {
                    foreach (var s in theList)
                    {
                        db.InLine.Add((InLine)s);
                        db.SaveChanges();
                    }
                }
            }
            else if (sendTo.GetType() == typeof(IVA))//check the type of sendTo
            {
                using (Context db = new Context())
                {
                    foreach (var s in theList)
                    {
                        IVA convertPatient = new IVA
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            LastFourDigits = s.LastFourDigits,
                            SocialSecurityNum = s.SocialSecurityNum,
                            Age = s.Age,
                            SymtomsLevel = s.SymtomsLevel,
                        };
                        db.IVA.Add(convertPatient);
                        db.SaveChanges();
                    }
                }
            }
            else if (sendTo.GetType() == typeof(Sanatorium))
            {
                using(Context db = new Context())
                {
                    foreach (var s in theList)
                    {
                        Sanatorium convertPatient = new Sanatorium
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            LastFourDigits = s.LastFourDigits,
                            SocialSecurityNum = s.SocialSecurityNum,
                            Age = s.Age,
                            SymtomsLevel = s.SymtomsLevel,
                        };
                        db.Sanatorium.Add(convertPatient);
                        db.SaveChanges();
                    }
                }
               
            }
            else if (sendTo.GetType() == typeof(Recovered))
            {
                using (Context db = new Context())
                {
                    foreach (var s in theList)
                    {
                        Recovered convertPatient = new Recovered
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            LastFourDigits = s.LastFourDigits,
                            SocialSecurityNum = s.SocialSecurityNum,
                            Age = s.Age,
                            SymtomsLevel = s.SymtomsLevel,
                        };
                        db.Recovereds.Add(convertPatient);
                        db.SaveChanges();
                    }
                }

            }
            else if (sendTo.GetType() == typeof(AfterLife))
            {
                using (Context db = new Context())
                {
                    foreach (var s in theList)
                    {
                        AfterLife convertPatient = new AfterLife
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            LastFourDigits = s.LastFourDigits,
                            SocialSecurityNum = s.SocialSecurityNum,
                            Age = s.Age,
                            SymtomsLevel = s.SymtomsLevel,
                        };
                        db.AfterLives.Add(convertPatient);
                        db.SaveChanges();
                    }
                }

            }
            return theList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theList"></param>
        /// <param name="removeFrom"></param>
        /// <returns></returns>
        public static List<IPatient> RemoveFromDataBase(List<IPatient> theList, IPatient removeFrom)
        {
            using (Context db = new Context())
            {

                if (removeFrom.GetType() == typeof(InLine))
                {

                    foreach (var s in theList)
                    {
                       db.InLine.Attach((InLine)s);
                       db.InLine.Remove((InLine)s);
                       db.SaveChanges();
                    }
                }
                else if (removeFrom.GetType() == typeof(IVA))
                {
                    foreach (var s in theList)
                    {
                        db.IVA.Attach((IVA)s);
                        db.IVA.Remove((IVA)s);
                        db.SaveChanges();
                    }
                }
                else if (removeFrom.GetType() == typeof(Sanatorium))
                {

                    foreach (var s in theList)
                    {
                       db.Sanatorium.Attach((Sanatorium)s);
                       db.Sanatorium.Remove((Sanatorium)s);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch(DbUpdateConcurrencyException e)
                        {
                            e.Entries.Single().Reload();
                            db.SaveChanges();
                        }
                        finally
                        {

                        }
                     
                    }
                }
                else if (removeFrom.GetType() == typeof(AfterLife))
                {
                    foreach (var s in theList)
                    {
                        db.AfterLives.Attach((AfterLife)s);
                        db.AfterLives.Remove((AfterLife)s);
                        db.SaveChanges();
                    }
                }
                else if (removeFrom.GetType() == typeof(Recovered))
                {
                    foreach (var s in theList)
                    {
                        db.Recovereds.Attach((Recovered)s);
                        db.Recovereds.Remove((Recovered)s);
                        db.SaveChanges();
                    }
                }
                return theList;
            }
        }


        public static List<IPatient> GetHealthiestPatients(IPatient fromTable)
        {

            List<IPatient> listOfPatient = new List<IPatient>();
            using (Context db = new Context())
            {
                if (fromTable.GetType() == typeof(InLine))
                {
                    var minSymtom = db.InLine.DefaultIfEmpty().Min(min => min.SymtomsLevel);

                    var getSickes = from patiant in db.InLine
                                    where patiant.SymtomsLevel == minSymtom
                                    select patiant;
                    foreach (var getPatient in getSickes.ToList())
                    {
                            listOfPatient.Add(getPatient);
                    }
                }
                else if (fromTable.GetType() == typeof(Sanatorium))
                {
                    var minSymtom = db.Sanatorium.DefaultIfEmpty().Min(min => min.SymtomsLevel);

                    var getSickes = from patiant in db.Sanatorium
                                    where patiant.SymtomsLevel == minSymtom
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                            listOfPatient.Add(getPatient);
                    }

                }
                else if (fromTable.GetType() == typeof(IVA))
                {
                    var minSymtom = db.IVA.DefaultIfEmpty().Min(min => min.SymtomsLevel);


                    var getSickes = from patiant in db.IVA
                                    where patiant.SymtomsLevel == minSymtom
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                            listOfPatient.Add(getPatient);
                    }
                }
                return listOfPatient;
            }

        }


        public static List<IPatient> GetSickesPatientFromTwoTables(List<IPatient> listOne,List<IPatient> listTwo,int ammount, int allowedValue)
        {
            List<IPatient> listOfPatient = new List<IPatient>();
            int allowed = 5;

                    
            foreach(var s in listOne)
            {
                        
                foreach(var i in listTwo)       
                {
                            
                    if(s.SymtomsLevel > i.SymtomsLevel)
                    {      
                        if(ammount < allowed)
                        {
                            listOfPatient.Add(s);
                            ammount++;
                        }   
                    }
                    
                    else if(s.SymtomsLevel < i.SymtomsLevel)
                    {        
                        if(ammount < allowed)
                        {
                            listOfPatient.Add(i);
                            ammount++;
                        }    
                    }
                        
                }
                    
            }
            return listOfPatient;
        }


        /// <summary>
        /// Grab the sickest patient and add it to a list
        /// </summary>
        /// <param name="fromTable"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public static List<IPatient> GetSickestPatients(IPatient fromTable, int ammount,int allowedValue)
        {
            List<IPatient> listOfPatient = new List<IPatient>();
            using(Context db = new Context())
            {
                if (fromTable.GetType() == typeof(InLine))
                {

                    var maxSymtom = db.InLine.DefaultIfEmpty().Max(max => max.SymtomsLevel);

                    var getSickes = from patiant in db.InLine
                                    where patiant.SymtomsLevel == maxSymtom
                                    select patiant;
                    foreach (var getPatient in getSickes.ToList())
                    {
                        if(ammount < allowedValue)
                        {
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                        
                    }
                }
                else if (fromTable.GetType() == typeof(Sanatorium))
                {
                    var maxSymtom = db.Sanatorium.DefaultIfEmpty().Max(max => max.SymtomsLevel);
         
                    var getSickes = from patiant in db.Sanatorium
                                    where patiant.SymtomsLevel == maxSymtom
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                        //checks how meny we can grab, if its bellow 1 we cant grab anymore patients
                        if (ammount < allowedValue)
                        {
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                    }

                }
               else if (fromTable.GetType() == typeof(IVA))
                {

                    var maxSymtom = db.IVA.DefaultIfEmpty().Max(max => max.SymtomsLevel);

                    var getSickes = from patiant in db.IVA
                                    where patiant.SymtomsLevel == maxSymtom
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                        if (ammount < allowedValue)
                        {
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                          
                    }
                }
                return listOfPatient;
            }
           
        }

        /// <summary>
        /// Grabs the oldest patient and add them to a list that we return
        /// </summary>
        /// <param name="fromTable"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public static List<IPatient> GetOldestPatients(IPatient fromTable, int ammount,int allowedValue)
        {
            List<IPatient> listOfPatient = new List<IPatient>();
            using (Context db = new Context())
            {
                if (fromTable.GetType() == typeof(InLine))
                {

                    var maxAge = db.InLine.DefaultIfEmpty().Max(max => max.Age);

                    var getSickes = from patiant in db.InLine
                                    where patiant.Age == maxAge 
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                        //checks how meny we can grab, if its bellow 1 we cant grab anymore patients
                        if (ammount < allowedValue)
                        { 
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                    }
                }
                else if (fromTable.GetType() == typeof(Sanatorium))
                {
                    var maxAge = db.Sanatorium.DefaultIfEmpty().Max(max => max.Age);

                    var getSickes = from patiant in db.Sanatorium
                                    where patiant.Age == maxAge
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                        if (ammount < allowedValue)
                        {
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                    }

                }
                else if (fromTable.GetType() == typeof(IVA))
                {
                    var maxAge = db.IVA.DefaultIfEmpty().Max(max => max.Age);


                    var getSickes = from patiant in db.IVA
                                    where patiant.Age == maxAge
                                    select patiant;

                    foreach (var getPatient in getSickes.ToList())
                    {
                        if (ammount < allowedValue)
                        {
                            listOfPatient.Add(getPatient);
                            ammount++;
                        }
                    }
                }
                return listOfPatient;
            }

        }



        /// <summary
        /// Checks if any of the tables is empty, depending on what type patient is.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool TableEmpty(IPatient whatType)//just wanned to try generic
        {
            using (Context db = new Context())
            {
                if (whatType.GetType() == typeof(InLine))
                {
                    var grab = from patient in db.InLine
                               select patient;
                    if (grab.Count() > 0)
                        return false;
                }
                else if (whatType.GetType() == typeof(IVA))
                {
                    var grab = from patient in db.IVA
                               select patient;
                    if (grab.Count() > 0)
                        return false;
                }
                else if (whatType.GetType() == typeof(Sanatorium))
                {
                    var grab = from patient in db.Sanatorium
                               select patient;
                    if (grab.Count() > 0)
                        return false;
                }

                return true;

            }
        }
        /// <summary>
        /// Return the number of rows a table have in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int TableRows<T>(T t)//just wanned to try generic
        {
            using (Context db = new Context())
            {
                if (t.GetType() == typeof(InLine))
                {
                    var grab = from patient in db.InLine
                               select patient;
                    if (grab.Count() > 0)//if count higher then 0 we return rows else we return 0;
                        return grab.ToList().Count();
                }
                else if (t.GetType() == typeof(IVA))
                {
                    var grab = from patient in db.IVA
                               select patient;
                    if (grab.Count() > 0)
                        return grab.ToList().Count();
                }
                else if (t.GetType() == typeof(Sanatorium))
                {
                    var grab = from patient in db.Sanatorium
                               select patient;
                    if (grab.Count() > 0)
                        return grab.ToList().Count();
                }

                return 0;

            }
        }


        public static List<IPatient> SendToRecovery(IPatient whatType)
        {
            List<IPatient> recover = new List<IPatient>();
            using (Context db = new Context())
            {
                if (whatType.GetType() == typeof(IVA))
                {
                    var toRecover = db.IVA.Where(sym => sym.SymtomsLevel < 1).ToList();
                    foreach (var s in toRecover)
                    {
                        recover.Add(s);
                    }
                }
                if (whatType.GetType() == typeof(Sanatorium))
                {
                    var toRecover = db.Sanatorium.Where(sym => sym.SymtomsLevel < 1).ToList();
                    foreach (var s in toRecover)
                    {
                        recover.Add(s);
                    }
                }

                return recover;
            }

        }

        public static List<IPatient> SendToAfterLife(IPatient whatType)
        {
            List<IPatient> AfterLife = new List<IPatient>();
            using(Context db = new Context())
            {
                if (whatType.GetType() == typeof(IVA))
                {
                    var toAfterLife = db.IVA.Where(sym => sym.SymtomsLevel > 9).ToList();
                    foreach(var s in toAfterLife)
                    {
                        AfterLife.Add(s);
                    }
                }
                if (whatType.GetType() == typeof(Sanatorium))
                {
                    var toAfterLife = db.Sanatorium.Where(sym => sym.SymtomsLevel > 9).ToList();
                    foreach (var s in toAfterLife)
                    {
                        AfterLife.Add(s);
                    }
                }
            }

            return AfterLife;
           
        }

    }
}
