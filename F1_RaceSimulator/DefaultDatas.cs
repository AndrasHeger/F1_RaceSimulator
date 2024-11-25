using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1_RaceSimulator
{
    public class DefaultDatas
    {
        public ObservableCollection<Driver> Drivers { get; set; }
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Crew> Crews { get; set; }
        public ObservableCollection<Track> Tracks { get; set; }
        public DefaultDatas() 
        {
            Drivers = new ObservableCollection<Driver>();
            Crews = new ObservableCollection<Crew>();
            Cars = new ObservableCollection<Car>();
            Tracks = new ObservableCollection<Track>();

            LoadData();
        }

        private void LoadData()
        {
            Drivers.Add(new Driver("Charles Leclerc", 27, 60, 90, true, 50));
            Drivers.Add(new Driver("Lewis Hamilton", 39, 75, 75, false, 50));
            Drivers.Add(new Driver("Fernando Alonso", 43, 80, 65, false, 65));
            Drivers.Add(new Driver("Oscar Piastri", 23, 45, 75, false, 50));
            Drivers.Add(new Driver("Max Verstappen", 27, 60, 95, true, 45));

            Cars.Add(new Car("Ferrari", 80, 60, 80));
            Cars.Add(new Car("Mercedes", 60, 80, 70));
            Cars.Add(new Car("Aston Martin", 55, 70, 60));
            Cars.Add(new Car("McLaren", 80, 60, 70));
            Cars.Add(new Car("Red Bull", 85, 70, 65));

            Crews.Add(new Crew(75, 55, "Ferrari", 70));
            Crews.Add(new Crew(60, 75, "Mercedes", 65));
            Crews.Add(new Crew(60, 70, "Aston Martin", 60));
            Crews.Add(new Crew(65, 60, "McLaren", 70));
            Crews.Add(new Crew(85, 60, "Red Bull", 75));

            Tracks.Add(new Track("Monza", 50, "Sunny"));
            Tracks.Add(new Track("Hungary", 90, "Sunny"));
            Tracks.Add(new Track("Japan", 80, "Rain"));
            Tracks.Add(new Track("Austria", 20, "Sunny"));
            Tracks.Add(new Track("Brazil", 60, "Rain"));
        }
    }
    public class DefaultTeams
    {
        public ObservableCollection<Team> Teams { get; set; }

        public DefaultTeams()
        {
            DefaultDatas data = new DefaultDatas();
            Teams = new ObservableCollection<Team>();

            for (int i = 0; i < data.Drivers.Count; i++) 
            {
                Driver driver = data.Drivers[i];
                Car car = data.Cars[i];
                Crew crew = data.Crews[i];
                Teams.Add(new Team(driver, car, crew));
            }
        }
    }
}
