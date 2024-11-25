using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace F1_RaceSimulator
{
    class RaceViewModel
    {
        private static Random rand = new Random();

        public List<Team> DNFTeams { get; set; }
        public Track Track { get; set; }
        public List<Team> Grid { get; set; }



        public BitmapImage TrackImage
        {
            get
            {
                return SelectTrackImage(Track.TrackName);
            }
        }
        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }



        public ICommand CloseWindowCommand { get; set; }
        public Action CloseAction { get; set; }



        public RaceViewModel()
        {
            DNFTeams = new List<Team>();
            CloseWindowCommand = new RelayCommand(CloseWindow);
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            DefaultTeams defaultTeams = new DefaultTeams();
            Grid = defaultTeams.Teams.ToList();
        }

        public void InitializeTeam(Team team)
        {
            string playersDriverName = team.Driver.DriverName;
            Team remove = null;
            foreach (Team t in Grid)
            {
                if (t.Driver.DriverName == playersDriverName)
                {
                    remove = t;
                }
            }
            if (remove != null) 
            { 
                Grid.Remove(remove);
            }

            Grid.Add(team);
        }

        public void InitializeTrack(Track track) 
        { 
            Track = track;
        }

        private void CloseWindow()
        {
            CloseAction?.Invoke();
        }


        public void SimulateRace()
        {
            var sortedTeams = new List<Team>();
            foreach (var team in Grid)
            {
                if (CheckDNF(team))
                {
                    DNFTeams.Add(team);
                    continue;
                }

                team.Points = (int)Math.Round(CalculatePoints(team, Track));
                sortedTeams.Add(team);
                
            }

            sortedTeams = sortedTeams.OrderByDescending(t => t.Points).ToList();
            for (int i = 0; i < sortedTeams.Count; i++)
            {
                sortedTeams[i].Position = i+1;
            }


            Grid = new List<Team>(sortedTeams);
            
        }



        public bool CheckDNF(Team team)
        {
            double DNFChance = CalculateDNFChance(team);

            int roll = rand.Next(0,100);

            return roll < DNFChance;
        }

        public double CalculatePoints(Team team, Track track)
        {
            double score = 0;
            double bigMultiplier = rand.Next(20, 40) * 0.01;
            double lowMultiplier = rand.Next(10, 16) * 0.01;
            double avgMultiplier = rand.Next(20, 26) * 0.01;

            score += team.Driver.Speed * bigMultiplier;
            score += team.Driver.Experience * lowMultiplier;
            if (track.Weather == "Rain")
            {
                score += team.Driver.RainSkills * avgMultiplier;
            }
            if (team.Driver.IsReckless)
            {
                score += team.Driver.Speed * 0.05;
            }


            score += team.Car.Performance * avgMultiplier;
            score += team.Car.CornerScore * lowMultiplier;

            score += team.Crew.PitstopSpeed * lowMultiplier;
            score += team.Crew.Strategy * lowMultiplier;

            if (track.Difficulty >= 70)
            {
                score += team.Driver.Experience *avgMultiplier;
            }
            if (track.Difficulty >= 50)
            {
                score += team.Driver.Experience * lowMultiplier;
            }

            return score;
        }

        public double CalculateDNFChance(Team team)
        {
            double DNFChance = 10;

            double lowMultiplier = rand.Next(10, 21) * 0.01;
            double bigMultiplier = rand.Next(30, 41) * 0.01;

            if (team.Driver.IsReckless)
            {
                DNFChance += rand.Next(5,11);
            }


            DNFChance -= team.Driver.Experience * lowMultiplier;


            if (team.Car.Reliability < 60)
            {
                DNFChance += (60 - team.Car.Reliability) * bigMultiplier; 
            }
            else if (team.Car.Reliability > 75)
            {
                DNFChance -= (team.Car.Reliability - 80) * bigMultiplier;
            }


            if (team.Crew.PitstopQuality < 65)
            {
                DNFChance += 5; 
            }
            else if (team.Crew.PitstopQuality >= 70)
            {
                DNFChance -= 5; 
            }


            DNFChance = Math.Clamp(DNFChance, 0, 100);

            return DNFChance;
        }
        private BitmapImage SelectTrackImage(string trackName)
        {
            switch (trackName)
            {
                case "Monza":

                    return new BitmapImage(new Uri("pack://application:,,,/Images/monza.png"));
                case "Hungary":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/hungaroring.png"));
                case "Japan":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/japan.png"));
                case "Austria":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/austria.png"));
                case "Brazil":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/brazil.png"));
                default:
                    return null;
            }
        }
    }
}
