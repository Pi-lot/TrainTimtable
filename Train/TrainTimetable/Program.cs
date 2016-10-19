using System;

// Bryce Tuton. n9713026
namespace TrainTimetable
{

    enum Stations { Central, Roma_Street, Milton, Auchenflower, Toowong, Taringa, Indooroopilly };

    class Program
    {
        public enum stations { Central, Roma_St, Milton, Auchenflower, Toowong, Taringa, Indoorpilly};
        public const int NUMBER_OF_TRAINS = 76;
        public const int NUMBER_OF_STATIONS = 7;
        static int[,] timeTable = new int[NUMBER_OF_STATIONS, NUMBER_OF_TRAINS];

        static string departFrom = "\n Which station are you leaving from?\n"
                              + "\n1) Central"
                              + "\n2) Roma Street"
                              + "\n3) Milton"
                              + "\n4) Auchenflower"
                              + "\n5) Toowong"
                              + "\n6) Taringa"
                              + "\n\nEnter your option(1-6 or 0 to exit): ";

        static string arriveAt = "\n Which station are you going to?\n"
                           + "\n1) Roma Street"
                           + "\n2) Milton"
                           + "\n3) Auchenflower"
                           + "\n4) Toowong"
                           + "\n5) Taringa"
                           + "\n6) Indooroopilly"
                           + "\n\nEnter your option(1-6 or 0 to exit): ";

        static void Main()
        {

            // ********** Do not remove the following statement ******************
            timeTable = Timetables.CreateTimeTable();

            //*********** Start your code for Main below this line  ******************
            FindTime();

            ExitProgram();
        }// end Main

        /// <summary>
        /// Will exit the project once the user has pressed any key
        /// </summary>
        static void ExitProgram()
        {
            Console.Write("\n\n\t Press any key to terminate program ...");
            Console.ReadKey();
        }//end ExitProgram

        /// <summary>
        /// Method to make sure the minutes is always two digits.
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        static string MinuteStr(int minute)
        {
            int tenMin = 10;
            string min;

            if (minute >= 0 && minute < tenMin)
            {
                return min = $"0{minute}";
            }
            else
            {
                return min = minute.ToString();
            }
        }// end MinuteStr.

        /// <summary>
        /// Method to covert a given 24 hour time into a 12 hour string.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        static string Convert24To12(int hour, int minute)
        {
            int hoursIn12Hour = 12;
            string time;

            // Check if it is am or pm and reduce any number greater than 12 to less than twelve.
            if (hour == hoursIn12Hour)
            {
                return time = $"{hour.ToString()}:{MinuteStr(minute)}pm";
            }
            else if (hour > hoursIn12Hour)
            {
                return time = $"{(hour - 12).ToString()}:{MinuteStr(minute)}pm";
            }
            else
            {
                return time = $"{hour.ToString()}:{MinuteStr(minute)}am";
            }
        }// end Convert24To12.

        static void FindTime()
        {
            int depart, arrive, arriveTime, train = 0, exit = 0, firstStationNumber = 0, lastStationNumber = 6,
                arriveTimeHour, arriveTimeMinute, hoursInDay = 24, minutesInHour = 60, departTimeHour, departTimeMin,
                afternoonTimeLength = 4;
            string arriveTimeStr, departTimeStr;
            string[] hoursAndMinutes;
            bool correctFormat, correctFomartHour;

            // Find where the user wishes to leave from and check it is a number.
            Console.WriteLine(departFrom);
            do
            {
                correctFormat = int.TryParse(Console.ReadLine(), out depart);

                if (!correctFormat || depart > lastStationNumber)
                {
                    Console.WriteLine("Input invalid.\nplease enter a valid input (number 0-6)");
                }
            } while (!correctFormat || depart > lastStationNumber || depart < firstStationNumber); // end while

            // exit if the user enters 0.
            if (depart == exit)
            {
                return;
            }// end if

            // Remove 1 off depart so it equates to the enums.
            depart--;

            // Find where the user wishes to arrive at and check it is a number.
            Console.WriteLine(arriveAt);
            do
            {
                correctFormat = int.TryParse(Console.ReadLine(), out arrive);
                if (!correctFormat || arrive > lastStationNumber)
                {
                    Console.WriteLine("Input invalid.\nplease enter a valid input (number 0-6)");
                }
            } while (!correctFormat || arrive > lastStationNumber || arrive < firstStationNumber); // end while

            // Check if the user wishes to exit
            // Then check the values are within range.
            if (arrive == exit)
            {
                return;
            }
            else if (arrive == depart)
            {
                Console.WriteLine("You have selected {0} as both your depature and arrival station", (stations)arrive);
                Console.WriteLine("this enquiry is cancelled.");
                return;
            }
            else if (arrive < depart)
            {
                Console.WriteLine("You cannot travel from {0} to {1},", (stations)arrive, (stations)depart);
                Console.WriteLine("This enquiry is cancelled.");
                return;
            }// end if

            // Find when the user wishes to be at arrival station
            Console.WriteLine("What time do you wish to be at {0} in 24 hour format <hh:mm>: ", (stations)arrive);

            // Get the values then check they are possible, if not tell the user and ask again.
            do
            {
                // Check the numbers are infact numbers and correct 24 hour numbers.
                do
                {
                    arriveTimeStr = Console.ReadLine();
                    hoursAndMinutes = arriveTimeStr.Split(':');
                    correctFomartHour = int.TryParse(hoursAndMinutes[0], out arriveTimeHour);
                    correctFormat = int.TryParse(hoursAndMinutes[1], out arriveTimeMinute);

                    if (!correctFormat || arriveTimeMinute >= minutesInHour ||
                        !correctFomartHour || arriveTimeHour >= hoursInDay)
                    {
                        Console.WriteLine("That is not in 24-hour format.\nPlease re-enter a time in 24-hour");
                    }
                } while (!correctFormat || arriveTimeMinute >= minutesInHour ||
                !correctFomartHour || arriveTimeHour >= hoursInDay); // end do while

                arriveTime = int.Parse(hoursAndMinutes[0] + hoursAndMinutes[1]);

                if (arriveTime < timeTable[arrive, train])
                {
                    Console.WriteLine("You cannot catch any train to arrive by {0} at {1}", arriveTimeStr, (stations)arrive);
                    Console.WriteLine("You will need to enter a later time.");
                }
            } while (arriveTime < timeTable[arrive, train]);// end while

            // Find the arrival time by finding the greatest value that fits criteria
            for (int i = 0; i < NUMBER_OF_TRAINS; i++)
            {
                if (timeTable[arrive, i] < arriveTime)
                {
                    train = i;
                }
            }

            // Convert times to 12 hour.
            arriveTimeStr = Convert24To12(arriveTimeHour, arriveTimeMinute);

            // Convert departTime to a string and put into correct format to convert to 12 hour.
            // Then convert
            departTimeStr = timeTable[arrive, train].ToString();
            if (departTimeStr.Length == afternoonTimeLength)
            {
                departTimeStr = departTimeStr.Insert(2, ":");
            }
            else
            {
                departTimeStr = departTimeStr.Insert(1, ":");
            }// end if

            hoursAndMinutes = departTimeStr.Split(':');
            departTimeHour = int.Parse(hoursAndMinutes[0]);
            departTimeMin = int.Parse(hoursAndMinutes[1]);

            departTimeStr = Convert24To12(departTimeHour, departTimeMin);


            Console.WriteLine("To arrive at {0} by {1}", (stations)arrive, arriveTimeStr);
            Console.WriteLine("You need to catch the train from {0} at {1}", (stations)depart, departTimeStr);
        }// end FindTime
    }//end class
}//end nameSpace
