using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WeatherReport
{
    struct Weather
    {
        public string town;
        public string time;
        public string wind; // 3digits=direction last 2 digits= strength
        public int temperature;

        public Weather(string town, string time, string wind, int temperature)
        {
            this.town = town;
            this.time = time;
            this.wind = wind;
            this.temperature = temperature;
        }
    }
    class WeatherReport
    {

        /*
     // twn hhmm wind  temp

         BP 0300 32007 21   // 3digits=direction last 2 digits= strength
         PA 0315 35010 19 
         PR 0315 32009 19 
         SM 0315 01015 20 
         DC 0315 VRB01 21 
         SN 0315 00000 21
         */
        static List<Weather> sweatherRecords = new List<Weather>();

        //Task 1: Read and store the data in the tavirathu13.txt
        static void Task1()
        {
            StreamReader sr = new StreamReader("tavirathu13.txt");

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split();
                string town = line[0];
                string time = line[1];
                string wind = line[2];
                int temperature = int.Parse(line[3]);

                Weather item = new Weather(town, time, wind, temperature);
                sweatherRecords.Add(item);
            }

            sr.Close();
        }

        //Task 2: Ask the user for a town. Print when the last record was recorder of that town.
        static void Task2()
        {
            Console.WriteLine("Task 2");
            Console.Write("\tTwon: ");
            string kod = Console.ReadLine();
            int h = 0;
            int m = 0;

            foreach (Weather item in sweatherRecords)
            {
                if (item.town == kod)
                {
                    int hour = int.Parse(item.time.Substring(0, 2));
                    int minute = int.Parse(item.time.Substring(2));
                    h = hour;
                    m = minute;
                }

            }
            Console.WriteLine($"\tLast record was made at {h}:{m}.");
        }

        //Task 3: Print when they recorded the coldest, and when they recorded hotest temperature
        static void Task3()
        {
            Console.WriteLine("Task 3");
            string townOfMax = "";
            string townOfMin = "";
            int maxTemperature = 0;
            int minTemperature = 60;
            string timeOfMax = "";
            string timeOfMin = "";

            foreach (Weather item in sweatherRecords)
            {
                if (item.temperature > maxTemperature)
                {
                    maxTemperature = item.temperature;
                    townOfMax = item.town;
                    timeOfMax = item.time;
                }

                if (item.temperature < minTemperature)
                {
                    minTemperature = item.temperature;
                    townOfMin = item.town;
                    timeOfMin = item.time;
                }

            }
            Console.WriteLine($"\tColdest: {townOfMin} {timeOfMin.Substring(0, 2)}:{timeOfMin.Substring(2)} {minTemperature} Celsius-degree.");
            Console.WriteLine($"\tHottest: {townOfMax} {timeOfMax.Substring(0, 2)}:{timeOfMax.Substring(2)} {maxTemperature} Celsius-degree.");
        }

        //Task 4: Print the towns and the time where and when there was no wind (Task sheets stated 00000 = no wind)
        static void Task4()
        {
            Console.WriteLine("Task 4");
            bool alwaysWind = true;
            foreach (Weather item in sweatherRecords)
            {
                if (item.wind == "00000")
                {
                    Console.WriteLine($"\t" + item.town + " " + item.time.Substring(0, 2) + ":" + item.time.Substring(2));
                    alwaysWind = false;
                }
            }
            if (alwaysWind)
            {
                Console.WriteLine($"\tThere was wind everywhere and everytime");
            }

        }

        //Task 5: Print the average temperature, and the temperature fluctuation.
        //        Task sheet states that average temperature is calculated of the records recorded at hour 1, 7, 13, 19
        //        If there is no record at any of those hours write NA
        static List<string> towns = new List<string>();
        static void Task5()
        {
            Console.WriteLine("Task 5");

            foreach (Weather item in sweatherRecords)
            {
                if (!towns.Contains(item.town))
                    towns.Add(item.town);
            }
            foreach (string t in towns)
            {
                int average = 0;
                int counter = 0;
                int max = 0;
                int min = 60;
                bool h1 = false;
                bool h7 = false;
                bool h13 = false;
                bool h19 = false;
                foreach (Weather item in sweatherRecords)
                {
                    int h = int.Parse(item.time.Substring(0, 2));
                    int m = int.Parse(item.time.Substring(2));
                    if (item.town == t)
                    {
                        if (h == 1)
                        {
                            average += item.temperature;
                            counter++;
                            h1 = true;
                        }
                        if (h == 7)
                        {
                            average += item.temperature;
                            counter++;
                            h7 = true;
                        }
                        if (h == 13)
                        {
                            average += item.temperature;
                            counter++;
                            h13 = true;
                        }
                        if (h == 19)
                        {
                            average += item.temperature;
                            counter++;
                            h19 = true;
                        }
                    }
                    if (item.town == t)
                    {
                        if (item.temperature > max)
                            max = item.temperature;
                        if (item.temperature < min)
                            min = item.temperature;
                    }

                }
                if (h1 && h13 && h7 && h19)
                {
                    Console.WriteLine($"\t{t} Average: {average / counter}; Temperature fluctuation: {max - min}");
                }
                else
                {
                    Console.WriteLine($"\t{t} Average: NA; Temperature fluctuation: {max - min}");
                }
            }

        }

        //Task 6: Make a txt file for each town and list the time of the records and illustrate the wind strength with # signs
        // 3digits=direction last 2 digits= strength
        static void Task6()
        {
            Console.WriteLine("Task 6");
            foreach (string t in towns)
            {
                StreamWriter sw = new StreamWriter($"{t}.txt");
                sw.WriteLine(t);

                foreach (Weather item in sweatherRecords)
                {
                    if (item.town == t)
                    {
                        int S = int.Parse(item.wind.Substring(4));
                        sw.Write($"{item.time.Substring(0, 2)}:{item.time.Substring(2)} ");
                        for (int i = 0; i < S; i++)
                        {
                            sw.Write("#");
                        }
                        sw.WriteLine();
                    }

                }

                sw.Flush();
                sw.Close();
            }
            Console.WriteLine("\tThe txt files are done");

        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Console.WriteLine();
            Task3();
            Console.WriteLine();
            Task4();
            Console.WriteLine();
            Task5();
            Console.WriteLine();
            Task6();
            Console.ReadKey();
        }
    }
}
