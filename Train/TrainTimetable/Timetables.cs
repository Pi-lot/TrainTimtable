using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainTimetable {


    /// <summary>
    /// 
    /// DO NOT CHANGE THE CODE IN THIS CLASS
    /// 
    /// Creates a simplified train timetable for trains departing from Central Station
    /// to Indooroopilly Station between 04:58 and 23:43
    /// 
    /// Author:  Mike Roggenkamp
    /// Date: March 2014
    /// </summary>
    class Timetables {

        const int NUMBER_OF_TRAINS = Program.NUMBER_OF_TRAINS;
        const int NUMBER_OF_STATIONS = Program.NUMBER_OF_STATIONS;

        /// <summary>
        /// DO NOT CHANGE THE CODE IN THIS METHOD
        /// 
        /// Ensures that the time for the next train is stored correctly.
        /// 
        /// It handles change of hour if required.
        /// 
        /// Author:  Mike Roggenkamp
        /// Date: March 2014
        /// </summary>
        /// <param name="time">the time of the train arriving at a station </param>
        /// <returns> time in 24 hour format without the : as a 3 or 4 digit integer
        ///               between 458 and 2355</returns>
        static int ReturnCorrectTime(int time) {
            const int HUNDRED = 100;
            const int SIXTY = 60;
            int minutes = 0;
            int hour = 0;
            int correctTime = time;

            minutes = time % HUNDRED;

            if (minutes >= SIXTY) {
                hour = (time / HUNDRED);
                hour++;
                minutes = minutes - SIXTY;
                correctTime = hour * HUNDRED + minutes;
            }//end if 

            return correctTime;
        }//end returnCorrectTime

        /// <summary>
        /// DO NOT CHANGE THE CODE IN THIS METHOD
        /// 
        /// Creates a simplified train timetable for trains departing from Central Station
        /// to Indooroopilly Station between 04:58 and 23:43
        /// 
        /// A train departs every 15 minutes from Central and arrives and departs 
        /// from the next station every 2 minutes
        /// 
        /// The time is stored as an integer in 24 hour format without ":".
        /// So 04:58 is stored as 0458 and 23:43 is stored as 2343
        /// 
        /// 
        /// Author:  Mike Roggenkamp
        /// Date: March 2014
        /// </summary>
        public static int[,] CreateTimeTable() {
            int[,] timetable = new int[NUMBER_OF_STATIONS, NUMBER_OF_TRAINS];

            const int TIME_TO_NEXT_STATION = 2;
            const int NEXT_TRAIN_LEAVES_CENTRAL = 15;
            const int FIRST_TRAIN = 0458;

            int time = FIRST_TRAIN;
            int nextDeparture = FIRST_TRAIN;

            for (int aTrain = 0; aTrain < NUMBER_OF_TRAINS; aTrain++) {
                time = nextDeparture;
                for (Stations station = Stations.Central; station <= Stations.Indooroopilly; station++) {
                    timetable[(int)station, aTrain] = time;
                    time = ReturnCorrectTime(time + TIME_TO_NEXT_STATION);
                }//for Stations

                nextDeparture = ReturnCorrectTime(nextDeparture + NEXT_TRAIN_LEAVES_CENTRAL);
            }// for aTrain

            return timetable;

        }// end CreateTimeTable



    }
}
