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
        // INotifyPropertyChanged
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        // Naam van de player
        #region PlayerName

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

        // Verwijst naar de GameWindow
        private void BtnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            // Controleert de naam invoer
            #region Controleer invoer gebruiker
            // naam moet gevuld zijn
            if (PlayerName == "")
            {
                MessageBox.Show("Please choose a player name.");
                return;
            }
            #endregion

            // Gegevens die wordt meegegeven naar de andere window voor de player
            #region Gegevens Player, verwijzing naar andere window
            PlayerModel playerModel = new(200, PlayerName);
            GameWindow gameWindow = new(playerModel);
            gameWindow.Show();
            this.Close();
            #endregion
        }

        // Sluit dit scherm of blijft
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            //Confirmation
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to leave?", "Exit", MessageBoxButton.YesNo);
            //Ja of nee knoppen en hun functies
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("See you soon, Captain!", "Exit");
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Ok!", "Exit");
                    break;

            }
        }
    }
}
