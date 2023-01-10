using System;
using System.Collections.Generic;

namespace SoftSystem_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RoomsNumber roomNumber = new RoomsNumber();
            roomNumber.runExercise();

            PerfectNumber perfectNumber = new PerfectNumber();
            perfectNumber.runExercise();
        }
    }

    /// <summary>
    /// This method return random numbers. Based on https://en.wikipedia.org/wiki/Linear_congruential_generator#Sample_code
    /// </summary>
    public class RandomNumberGenerator
    {
        private const long m = 4294967296; 
        private const long a = 1664525;
        private const long c = 1013904223;
        private long _last;

        public RandomNumberGenerator()
        {
            _last = DateTime.Now.Ticks % m;
        }

        public RandomNumberGenerator(long seed)
        {
            _last = seed;
        }

        public long Next()
        {
            _last = ((a * _last) + c) % m;

            return _last;
        }

        public long Next(long maxValue)
        {
            return Next() % maxValue;
        }
    }

    public class RoomsNumber
    {
        private int getNumebrOfRooms(List<ScheduleTime> scheduleTimes)
        {
            int endTimeIndex = 0;
            int rooms = 0;
            int maxRooms = 0;

            List<int> startTimes = getListOfTimes(scheduleTimes, (var) => var.startTime);
            List<int> endTimes = getListOfTimes(scheduleTimes, (var) => var.endTime);

            startTimes.Sort();
            endTimes.Sort();

            foreach (var item in startTimes)
            {
                if (item < endTimes[endTimeIndex])
                {
                    rooms += 1;
                    maxRooms = rooms > maxRooms ? rooms : maxRooms;
                }
                else
                {
                    rooms -= 1;
                    endTimeIndex += 1;
                }
            }

            return maxRooms;
        }

        private List<int> getListOfTimes(List<ScheduleTime> scheduleTimes, Func<ScheduleTime, int> getter)
        {
            List<int> result = new List<int>();

            foreach (var item in scheduleTimes)
            {
                result.Add(getter.Invoke(item));
            }

            return result;
        }

        public void runExercise()
        {
            List<ScheduleTime> scheduleTimes = new List<ScheduleTime>
            {
                new ScheduleTime(8,10),
                new ScheduleTime(8,11),
                new ScheduleTime(9,11),
                new ScheduleTime(9,11),
                new ScheduleTime(12,14),
                new ScheduleTime(12,13),
                new ScheduleTime(11,13),
                new ScheduleTime(8,11),
                new ScheduleTime(12,13),
            };

            int result = getNumebrOfRooms(scheduleTimes);

            Console.WriteLine("Number of class rooms: " + result);
        }
    }

    public class PerfectNumber
    {
        private bool isPerfectNumber(long number)
        {
            int result = 0;

            for (int i = 1; i < number; i++)
            {
                result = number % i == 0 ? result + i : result;
            }

            return result == number;
        }

        public double getPercentForArray(long[] randomNumbers)
        {
            double percent = 0;

            foreach (var number in randomNumbers)
            {
                percent = isPerfectNumber(number) ? percent + 1 : percent;
            }

            return (percent / randomNumbers.Length) * 100;
        }

        public void runExercise()
        {
            RandomNumberGenerator rnd = new RandomNumberGenerator();

            long[] randomNumbers = new long[]
            {
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(),
                rnd.Next(), 
            };

            var result = getPercentForArray(randomNumbers);

            Console.WriteLine("Percent of perfect numbers in array: " + result);
        }
    }
}
