using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace F1_RaceSimulator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Driver> drivers;
        public ObservableCollection<Driver> Drivers
        {
            get { return drivers; }
            set { drivers = value; OnPropertyChanged(); }
        }
        private Driver selectedDriver;
        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set { selectedDriver = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Car> cars;
        public ObservableCollection<Car> Cars
        {
            get { return cars; }
            set { cars = value; OnPropertyChanged(); }
        }
        private Car selectedCar;
        public Car SelectedCar
        {
            get { return selectedCar; }
            set { selectedCar = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedCarImage)); }
        }

        private ObservableCollection<Crew> crews;
        public ObservableCollection<Crew> Crews
        {
            get { return crews; }
            set { crews = value; OnPropertyChanged(); }
        }
        private Crew selectedCrew;
        public Crew SelectedCrew
        {
            get { return selectedCrew; }
            set { selectedCrew = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedCrewImage)); }
        }

        private ObservableCollection<Track> tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; OnPropertyChanged(); }
        }
        private Track selectedTrack;
        public Track SelectedTrack
        {
            get { return selectedTrack; }
            set { selectedTrack = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedTrackImage)); }
        }

        public BitmapImage SelectedCarImage 
        { 
            get 
            { 
                return SelectTeamImage(SelectedCar.Manufacturer); 
            }  
        }
        public BitmapImage SelectedCrewImage
        {
            get
            {
                return SelectTeamImage(SelectedCrew.TeamName);
            }
        }
        public BitmapImage SelectedTrackImage
        {
            get
            {
                return SelectTrackImage(SelectedTrack.TrackName);
            }
        }
        public ICommand AddNewDriverCommand { get; set; }
        public ICommand SimulateRaceCommand { get; set; }

        public event Action<Team> TeamSelected;
        public event Action<Track> TrackSelected;

        public MainViewModel()
        {
            Drivers = new ObservableCollection<Driver>();
            Cars = new ObservableCollection<Car>();
            Crews = new ObservableCollection<Crew>();
            Tracks = new ObservableCollection<Track>();
            

            LoadInitialData();
            SelectedDriver = Drivers.FirstOrDefault();
            SelectedCar = Cars.FirstOrDefault();
            SelectedCrew = Crews.FirstOrDefault();
            SelectedTrack = Tracks.FirstOrDefault();


            AddNewDriverCommand = new RelayCommand(OpenDriverCreationWindow);
            SimulateRaceCommand = new RelayCommand(OpenRaceWindow);
        }

        private void LoadInitialData()
        {
            DefaultDatas data = new DefaultDatas();
            Drivers = data.Drivers;
            Cars = data.Cars;
            Crews = data.Crews;
            Tracks = data.Tracks;
        }

        private void OpenDriverCreationWindow()
        {
            DriverCreatorWindow dcw = new DriverCreatorWindow();

            dcw.DriverSaved += (sender, driver) =>
            {
                Drivers.Add(driver); 
            };

            dcw.ShowDialog();
        }
        private void OpenRaceWindow()
        {
            Team selectedTeam = new Team(SelectedDriver, SelectedCar, SelectedCrew);

            RaceViewModel raceViewModel = new RaceViewModel();

            TeamSelected += raceViewModel.InitializeTeam;
            TrackSelected += raceViewModel.InitializeTrack;

            RaceWindow rw = new RaceWindow
            {
                DataContext = raceViewModel
            };

            raceViewModel.CloseAction = rw.Close;


            TeamSelected?.Invoke(selectedTeam);
            TrackSelected?.Invoke(SelectedTrack);
            raceViewModel.SimulateRace();


            rw.Closed += (s, e) => TeamSelected -= raceViewModel.InitializeTeam;
            rw.Closed += (s, e) => TrackSelected -= raceViewModel.InitializeTrack;

            rw.ShowDialog();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName =null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        private BitmapImage SelectTeamImage(string teamName)
        {
            switch (teamName)
            {
                case "Ferrari":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/ferrari.png"));
                case "McLaren":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/mclaren.png"));
                case "Mercedes":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/mercedes.png"));
                case "Aston Martin":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/astonmartin.png"));
                case "Red Bull":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/redbull.png"));
                default:
                    return null;
            }
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
