using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Views
{
    /// <summary>
    /// Interaction logic for GameMenuWindow.xaml
    /// </summary>
    public partial class GameMenuWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties

        private ObservableCollection<PlayerModel> players = new();

        public ObservableCollection<PlayerModel> Players
        {
            get { return players; }
            set { players = value; OnPropertyChanged(); }
            
        }

        public string PlayerName { get; set; } = string.Empty;

        #endregion

        public GameMenuWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Windows_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            #region Controleer invoer gebruiker
            // naam moet gevuld zijn
            if (PlayerName == "")
            {
                MessageBox.Show("Please choose a player name.");
                return;
            }
            #endregion

            PlayerModel playerModel = new(200, PlayerName);
            GameWindow gameWindow = new(playerModel);
            gameWindow.Show();
            this.Close();
        }
    }
}
