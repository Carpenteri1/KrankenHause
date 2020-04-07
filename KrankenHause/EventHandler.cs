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
        public delegate bool WillRunOrStop(bool willRun);
        public delegate void TimeForSimulation(DateTime time);

        private static event TimeForSimulation timeForSim;
        private static event WillRunOrStop willRun;
        private static event ChangesMadeToDataBase AddedToDatabase;
        private static event LogFile WriteToFile;

        public static DateTime Time { get; set; }

        public static TimeSpan AmmountOfTime { get; set; }

        public static TimeForSimulation TimeForSim { get; set; }
        public static LogFile LogToFile { get; set; }
        public static ChangesMadeToDataBase Call { get; set; }
        public static WillRunOrStop RunOrStop { get; set; }

        public static bool IfEmpty(bool willRun)
        {
            return willRun;
        }


        public static void TimeTaken(DateTime time)
        {
            AmmountOfTime = time.Subtract(Time);
            Console.WriteLine($"Time: {AmmountOfTime.ToString("HH:mm:ss")}");
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
