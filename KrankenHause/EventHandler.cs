using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrankenHause
{
    class EventHandler : EventArgs
    {
        public delegate void ChangesMadeToDataBase(int ammount, IPatient toTable);
        public delegate void LogFile(List<IPatient> patients, IPatient type);
        public delegate void StopOrRunSim(bool willRun);
        public delegate void ShowTime(DateTime time);

        private static event ChangesMadeToDataBase AddedToDatabase;
        private static event LogFile WriteToFile;
        private static event ShowTime TimeOfSimulation;
        private static StopOrRunSim Stop; 

        public static TimeSpan TimeSpan { get; set; }
        public static DateTime SimStarts { get; set; }
        public static bool CancelLoop { get; set; }
        public static StopOrRunSim StopSimulation { get; set; }
        public static ChangesMadeToDataBase PrintToScreen { get; set; }
        public static ShowTime GrabTime { get; set; }

        public static LogFile LogToFile { get; set; }

        public static void TimeTaken(DateTime time)
        {
            TimeSpan = DateTime.Now - SimStarts;
            Console.WriteLine($"Simulation ended!" +
                $"\nTime: {TimeSpan.ToString(@"hh\:mm\:ss")}");
        }

        public static void StopOrRun(bool willRun)
        {
           CancelLoop = willRun;
        }

        public static void StartToLog(List<IPatient>thelist,IPatient whatType)
        {
            if (whatType.GetType() == typeof(AfterLife))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\log_AfterLife.txt", true))
                {
                    foreach (var s in thelist)
                    {
                        file.WriteLine($"AfterLife: {DateTime.Now} {s.ToString()}");
                    }
                }
            }
            else if (whatType.GetType() == typeof(Recovered))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\log_Recovered.txt", true))
                {
                    foreach (var s in thelist)
                    {
                        file.WriteLine($"Recovered: {DateTime.Now} {s.ToString()}");
                    }
                }

            }
        }


        public static void PatientAdded(int ammount,IPatient toTable)
        {
            string type = string.Empty;
            if (toTable.GetType() == typeof(InLine))
            {
                type = "Inline";
            }
            else if (toTable.GetType() == typeof(IVA))
            {
                type = "IVA";
            }
            else if (toTable.GetType() == typeof(Sanatorium))
            {
                type = "Sanatorium";
            }
            else if (toTable.GetType() == typeof(AfterLife))
            {
                type = "AfterLife";
            }
            else if (toTable.GetType() == typeof(Recovered))
            {
                type = "Recovered";
            }

            Console.WriteLine($"{ammount} Patients where added to " +
                   $"{type} Table");
        }
    }
}
