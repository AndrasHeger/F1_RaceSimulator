using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace F1_RaceSimulator
{
    public class DriverCreatorViewModel : INotifyPropertyChanged
    {
        public int AbilityPoints { get; set; }
        public List<int> AgeList { get; set; }

        private bool isRecklessCheck;
        public bool IsRecklessCheck
        {
            get { return isRecklessCheck; }
            set { isRecklessCheck = value; OnPropertyChanged(); ChB_Checked(); }
        }


        private int selectedAge;
        public int SelectedAge 
        {
            get { return selectedAge; }
            set { selectedAge = value; OnPropertyChanged(); SetDriverAttributes(); }
        }
        private int initialExperience;
        private int initialTrackSpeed;
        private int initialRainSkills;

        private Driver customDriver;
        public Driver CustomDriver 
        {
            get { return customDriver; }
            set { customDriver = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseExperienceCommand { get; }
        public ICommand DecreaseExperienceCommand { get; }
        public ICommand IncreaseTrackSpeedCommand { get; }
        public ICommand DecreaseTrackSpeedCommand { get; }
        public ICommand IncreaseRainDrivingCommand { get; }
        public ICommand DecreaseRainDrivingCommand { get; }
        public ICommand SaveDriverCommand { get; }
        public event EventHandler<Driver> OnSaveDriver;


        public DriverCreatorViewModel()
        {
            selectedAge = 15;
            isRecklessCheck = false;
            CustomDriver = new Driver();
            AgeList = new List<int>();
            for (int i = 15; i < 55; i++)
            {
                AgeList.Add(i);
            }
            SetDriverAttributes();

            IncreaseExperienceCommand = new RelayCommand(IncreaseExperience);
            DecreaseExperienceCommand = new RelayCommand(DecreaseExperience);
            IncreaseTrackSpeedCommand = new RelayCommand(IncreaseTrackSpeed);
            DecreaseTrackSpeedCommand = new RelayCommand(DecreaseTrackSpeed);
            IncreaseRainDrivingCommand = new RelayCommand(IncreaseRainDriving);
            DecreaseRainDrivingCommand = new RelayCommand(DecreaseRainDriving);
            SaveDriverCommand = new RelayCommand(SaveDriver);
        }

        private void SaveDriver()
        {
            OnSaveDriver?.Invoke(this, CustomDriver);
        }

        private void DecreaseRainDriving()
        {
            if (initialRainSkills < CustomDriver.RainSkills) 
            {
                AbilityPoints++;
                CustomDriver.RainSkills -= 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        private void IncreaseRainDriving()
        {
            if (AbilityPoints>0 && CustomDriver.RainSkills < 100)
            {
                AbilityPoints--;
                CustomDriver.RainSkills += 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        private void DecreaseTrackSpeed()
        {
            if (initialTrackSpeed < CustomDriver.Speed && isRecklessCheck == false)
            {
                AbilityPoints++;
                CustomDriver.Speed -= 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        private void IncreaseTrackSpeed()
        {
            if (AbilityPoints > 0 && CustomDriver.Speed < 100)
            {
                AbilityPoints--;
                CustomDriver.Speed += 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));

        }

        private void DecreaseExperience()
        {
            if (initialExperience < CustomDriver.Experience)
            {
                AbilityPoints++;
                CustomDriver.Experience -= 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        private void IncreaseExperience()
        {
            
            if (AbilityPoints > 0 && CustomDriver.Experience < 100)
            {
                AbilityPoints--;
                CustomDriver.Experience += 5;
            }
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetDriverAttributes()
        {
            if (SelectedAge < 25)
            {
                AbilityPoints = 15;
                CustomDriver.Experience = 30;
                CustomDriver.Speed = 50;
                CustomDriver.RainSkills = 35;

                initialExperience = CustomDriver.Experience;
                initialRainSkills = CustomDriver.RainSkills;
                initialTrackSpeed = CustomDriver.Speed;
            }
            else if (SelectedAge <= 35)
            {
                AbilityPoints = 12;
                CustomDriver.Experience = 45;
                CustomDriver.Speed = 50;
                CustomDriver.RainSkills = 35;

                initialExperience = CustomDriver.Experience;
                initialRainSkills = CustomDriver.RainSkills;
                initialTrackSpeed = CustomDriver.Speed;
            }
            else 
            {
                AbilityPoints = 10;
                CustomDriver.Experience = 60;
                CustomDriver.Speed = 45;
                CustomDriver.RainSkills = 35;

                initialExperience = CustomDriver.Experience;
                initialRainSkills = CustomDriver.RainSkills;
                initialTrackSpeed = CustomDriver.Speed;
            }

            CustomDriver.Age = SelectedAge;
            OnPropertyChanged(nameof(CustomDriver));
            OnPropertyChanged(nameof(AbilityPoints));
        }

        private void ChB_Checked()
        {
            if (IsRecklessCheck && CustomDriver.Speed < 100)
            {
                if (CustomDriver.Speed <= 90) 
                {
                    CustomDriver.Speed += 10;
                }
                else
                {
                    CustomDriver.Speed += 5;
                }
            }
            else if (initialTrackSpeed <= CustomDriver.Speed - 10 && CustomDriver.Speed != 100)
            {
                CustomDriver.Speed -= 10;
            }
            else if (!IsRecklessCheck && CustomDriver.Speed == 100)
            {
                CustomDriver.Speed -= 10;
            }

            CustomDriver.IsReckless = IsRecklessCheck;
            OnPropertyChanged(nameof(CustomDriver));
        }
    }
}
