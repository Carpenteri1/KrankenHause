﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*Author: Niclas Timle
 *Date: 07-04-2020
 *Campus Varberg Sut-19 
 */
namespace KrankenHause
{
    class Program
    {
        ThreadClass threads = new ThreadClass();
        Program()
        {
            //kolla tid och stoppa loopen, 

            #region listener to event added
            EventHandler.PrintToScreen +=EventHandler.PatientAdded;
            EventHandler.LogToFile += EventHandler.StartToLog;
            EventHandler.GrabTime += EventHandler.TimeTaken;
            EventHandler.StopSimulation += EventHandler.StopOrRun;
            #endregion

            #region creating first thread and run it
            Thread firstthread = new Thread(threads.CreatePatients);
            firstthread.Name = "FirstThread";
            firstthread.Start();
            Console.WriteLine($"{firstthread.Name} iSAlive {firstthread.IsAlive}  ");
            firstthread.Join();
            firstthread.Abort();
            Console.WriteLine($"{firstthread.Name} IsAlive {firstthread.IsAlive}  ");
            #endregion first thread ended

            #region creating second thread and run it
            Thread SecondThread = new Thread(threads.SortPatientsInDatabase);
            SecondThread.Name = "SecondThread";
            SecondThread.Start();
            Console.WriteLine($"{SecondThread.Name} isAlive {SecondThread.IsAlive}");
            #endregion

            #region creating third thread and run it
            Thread thirdThread = new Thread(threads.UpdateSymtomsLevel);
            thirdThread.Name = "ThirdThread";
            thirdThread.Start();
            Console.WriteLine($"{thirdThread.Name} isAlive {thirdThread.IsAlive}  ");
            #endregion

            //ska bygga
            #region creating fourth thread and run it
            Thread fourthThread = new Thread(threads.SortToAfterLifeOrRecovered);
            fourthThread.Name = "FourthThread";
            fourthThread.Start();
            Console.WriteLine($"{fourthThread.Name} isAlive {fourthThread.IsAlive}  ");
            #endregion

        }
        static void Main(string[] args)
        {
            new Program();

        }
    }
}
