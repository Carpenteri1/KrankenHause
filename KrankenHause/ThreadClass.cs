using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KrankenHause
{
    class ThreadClass
    {
        private const int MaxAllowedIVARows = 5;
        private const int MaxAllowedSanatoriumRows = 10;
        private const int ExpectedToBeAlot = 30;
        /// <summary>
        /// Create a new instance of Patient and then push it to the database,the inline table
        /// sex is determent by the number, can it be divided by 2 or not ?
        /// </summary>
        public void CreatePatients()
        { 
            IPatient[] arrayOfPatient = new InLine[30];
            IPatient patient;
            for (int i = 0; i < arrayOfPatient.Length; i++)
            {
                patient = new InLine();
                patient.LastFourDigits = GenRandValue.LastFourDigits();
                patient.SocialSecurityNum = GenRandValue.SocialSecurityNum();
                patient.SymtomsLevel = GenRandValue.SymtomLevel();
                patient.LastName = GenRandValue.LastName();

                if (GenRandValue.GetSex() == SetSex.Female)
                {
                    patient.FirstName = GenRandValue.FirstNameFemale();
                }
                else
                {
                    patient.FirstName = GenRandValue.FirstNameMale();
                }
                Thread.Sleep(200);//multiple patiens with same values will be created without sleep 
                arrayOfPatient[i] = patient;
            }
            Context.PushToDataBase(arrayOfPatient.ToList(), new InLine());//<<--push it to the inline table
            //tells the user how meny patients where added and what table, the method can be found in the EventHandler class
            EventHandler.Call?.Invoke(arrayOfPatient.Length, new InLine());
        }

        /// <summary>
        /// Simple sort the patients, sickes got to IVA, oldest get to sanatorium
        /// </summary>

        public void SortPatientsInDatabase()
        {
            while (EventHandler.IfEmpty(true))
            {
                Thread.Sleep(5000);
                lock (Thread.CurrentThread)//locks the thread
                {
                    if (!Context.TableEmpty(new InLine()) && !Context.TableEmpty(new Sanatorium()))
                    {

                        //get sickes patient from inline and sanatorum and push to IVA 
                        if(Context.TableRows(new IVA()) < 5)
                        {
                            #region collect the sickest patient data from Inline and Sanatorium table
                            List<IPatient> getSickestInLine = Context.GetSickestPatients(new InLine(),Context.TableRows(new IVA()),MaxAllowedIVARows);
                            List<IPatient> getSickestInSanatorium = Context.GetSickestPatients(new Sanatorium(), getSickestInLine.Count()-Context.TableRows(new IVA()),MaxAllowedIVARows);
                            List<IPatient> sickestPatient = Context.GetSickesPatientFromTwoTables(getSickestInLine, getSickestInSanatorium,Context.TableRows(new IVA()),MaxAllowedIVARows);
                            #endregion

                            #region remove from tables and push
                            Context.RemoveFromDataBase(getSickestInSanatorium, new Sanatorium());
                            Context.RemoveFromDataBase(getSickestInLine,new InLine());
                            Context.PushToDataBase(sickestPatient, new IVA());
                            #endregion
                            EventHandler.Call?.Invoke(sickestPatient.Count(), new IVA());//<<--- shows how meny patient where moved to IVA table
                        }
                        if(Context.TableRows(new Sanatorium()) < 10)
                        {
                            List<IPatient> getOldestPatientList = Context.GetOldestPatients(new InLine(),Context.TableRows(new Sanatorium()),MaxAllowedSanatoriumRows);
                            Context.PushToDataBase(getOldestPatientList, new Sanatorium());
                            Context.RemoveFromDataBase(getOldestPatientList, new InLine());
                            EventHandler.Call?.Invoke(getOldestPatientList.Count(), new Sanatorium());
                        }
                    }
                    else if (!Context.TableEmpty(new Sanatorium()))
                    {
                        //get sickes patient from Sanatorum push to IVA
                         if(Context.TableRows(new IVA()) < 5)
                        {
                            List<IPatient> sickPatientList = Context.GetSickestPatients(new Sanatorium(),Context.TableRows(new IVA()),MaxAllowedIVARows);
                            Context.PushToDataBase(sickPatientList, new IVA());
                            Context.RemoveFromDataBase(sickPatientList, new Sanatorium());
                            EventHandler.Call?.Invoke(sickPatientList.Count(), new IVA());
                        }
                        
                    }
                    else if (!Context.TableEmpty(new InLine()))
                    {
                        if (Context.TableRows(new IVA()) < 5)
                        {
                            //get sickes patients from Inline push to IVA
                            List<IPatient> sickPatientList = Context.GetSickestPatients(new InLine(),Context.TableRows(new IVA()),MaxAllowedIVARows);
                            Context.RemoveFromDataBase(sickPatientList, new InLine());
                            Context.PushToDataBase(sickPatientList, new IVA());
                            EventHandler.Call?.Invoke(sickPatientList.Count(), new IVA());
                        }
                        if (Context.TableRows(new Sanatorium()) < 10)
                        {
                            //get oldest patients from inline push it to sanatorium
                            List<IPatient> getOldestPatientList = Context.GetOldestPatients(new InLine(),Context.TableRows(new Sanatorium()),MaxAllowedSanatoriumRows);
                            Context.PushToDataBase(getOldestPatientList, new Sanatorium());
                            Context.RemoveFromDataBase(getOldestPatientList, new InLine());
                            EventHandler.Call?.Invoke(getOldestPatientList.Count(),new Sanatorium());
                        }
                    }
                    else
                    {
                        //if sanatorium, iva and Inline are empty we go here
                        //EventHandler.RunOrStop?.Invoke(false);
                        //EventHandler.TimeForSim?.Invoke(DateTime.Now);
                    }
                }
               
            }
          
        }

        /// <summary>
        /// Update symtomlevel by calling the generate symtom class and its method
        /// Also checks if the table is empty
        /// </summary>
        public void UpdateSymtomsLevel()
        {
            while (EventHandler.IfEmpty(true))
            {
                Thread.Sleep(3000);
                lock (Thread.CurrentThread)
                {
                    if (!Context.TableEmpty(new InLine()))//if inline table is not empty
                    {
                        GenerateSymtomChanges.InLineSymtomChange();
                    }
                    if (!Context.TableEmpty(new IVA()))//if table not empty
                    {
                        GenerateSymtomChanges.IVASymtomChange();
                    }
                    if (!Context.TableEmpty(new Sanatorium()))//if table not empty
                    {
                        GenerateSymtomChanges.SanatoriumSymtomChange();
                    }
                }

            }
        }
        
        public void SortToAfterLifeOrRecovered()
        {
            while (EventHandler.IfEmpty(true))
            {
                Thread.Sleep(5000);
                lock (Thread.CurrentThread)
                {
                    if (Context.TableRows(new IVA()) > 0)
                    {
                        List<IPatient> AfterLife = Context.SendToAfterLife(new IVA());
                        List<IPatient> Recovery = Context.SendToRecovery(new IVA());

                        #region Remove from old tables and push to afterlife and recovery
                        Context.RemoveFromDataBase(Recovery,new IVA());
                        Context.RemoveFromDataBase(AfterLife, new IVA());
                        Context.PushToDataBase(Recovery, new Recovered());
                        Context.PushToDataBase(AfterLife, new AfterLife());
                        #endregion

                        #region write to file and show how ment patients where added
                        if (AfterLife.Count() > 0)
                        {
                            EventHandler.Call?.Invoke(AfterLife.Count(), new AfterLife());
                            EventHandler.LogToFile?.Invoke(AfterLife, new AfterLife());
                        }
                        if (Recovery.Count() > 0)
                        {
                            EventHandler.Call?.Invoke(Recovery.Count(), new Recovered());
                            EventHandler.LogToFile?.Invoke(Recovery, new Recovered());
                        }
                        #endregion
                    }
                    if (Context.TableRows(new Sanatorium()) > 0)
                    {
                        List<IPatient> AfterLife = Context.SendToAfterLife(new Sanatorium());
                        List<IPatient> Recovery = Context.SendToRecovery(new Sanatorium());


                        #region write to file and show how ment patients where added and remove and add
                        if(AfterLife.Count () > 0)
                        {
                            Context.RemoveFromDataBase(AfterLife, new Sanatorium());
                            Context.PushToDataBase(AfterLife, new AfterLife());
                            EventHandler.Call?.Invoke(AfterLife.Count(), new AfterLife());
                            EventHandler.LogToFile?.Invoke(AfterLife, new AfterLife());
                        }
                        if(Recovery.Count() > 0)
                        {
                            Context.RemoveFromDataBase(Recovery, new Sanatorium());
                            Context.PushToDataBase(Recovery, new Recovered());
                            EventHandler.Call?.Invoke(Recovery.Count(), new Recovered());
                            EventHandler.LogToFile?.Invoke(Recovery, new Recovered());
                        }
                        #endregion
                    }
                }
            }
        }
    }
}
