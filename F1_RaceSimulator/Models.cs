using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace F1_RaceSimulator
{
    public class Driver
    {
        public string DriverName { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public int Speed { get; set; }
        public int RainSkills { get; set; }
        public bool IsReckless { get; set; }

        public Driver(string name, int age, int experience, int speed, bool isReckless, int rainSkills)
        {
            DriverName = name;
            Age = age;
            Experience = experience;
            Speed = speed;
            IsReckless = isReckless;
            RainSkills = rainSkills;
        }
        public Driver()
        {
            DriverName = "Name";
            Age = 0;
            Experience = 0;
            Speed = 0;
            IsReckless = false;
            RainSkills = 0;
        }
    }



    public class Car
    {
        public string Manufacturer { get; set; }
        public int Performance { get; set; }
        public int Reliability { get; set; }
        public int CornerScore { get; set; }

        public Car(string manufacturer, int performance, int reliability, int cornerScore)
        {
            Manufacturer = manufacturer;
            Performance = performance;
            Reliability = reliability;
            CornerScore = cornerScore;
        }
    }


    public class Crew
    {
        public string TeamName { get; set; }
        public int PitstopSpeed { get; set; }
        public int PitstopQuality { get; set; }
        public int Strategy {  get; set; }

        public Crew(int pitStopSpeed, int pitStopQuality, string teamName, int strategy)
        {
            PitstopSpeed = pitStopSpeed;
            PitstopQuality = pitStopQuality;
            TeamName = teamName;
            Strategy = strategy;
        }
    }

    public class Track
    {
        public string TrackName { get; set; }
        public int Difficulty { get; set; }
        public string Weather { get; set; }

        public Track(string trackName, int difficulty, string weather)
        {
            TrackName = trackName;
            Difficulty = difficulty;
            Weather = weather;
        }
    }

    public class Team
    {
        public Driver Driver { get; set; }
        public Car Car { get; set; }
        public Crew Crew { get; set; }
        public int Points {  get; set; }
        public int Position {  get; set; }

        public Team(Driver driver, Car car, Crew crew)
        {
            Driver = driver;
            Car = car;
            Crew = crew;
            Points = 0;
        }
    }
}
