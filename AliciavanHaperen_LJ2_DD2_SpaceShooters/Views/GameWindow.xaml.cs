using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models;
using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Enemy;
using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading; // For the timer

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Fields


        private bool leftKeyPressed;
        private bool rightKeyPressed;
        private bool upKeyPressed;
        private bool downKeyPressed;
        private bool spaceKeyPressed;
        private readonly double enemySpawnRate = 350;
        private readonly Random random = new Random();

        #endregion

        #region Properties

        private ObservableCollection<PlayerModel> players = new();

        public ObservableCollection<PlayerModel> Players
        {
            get { return players; }
            set { players = value; OnPropertyChanged(); }

        }

        public GameModel GameModel { get; set; }
        private PlayerShip playerShip { get; set; }
        #endregion

        private readonly long startTime;

        private readonly DispatcherTimer gameTimer = new();

        private readonly Stopwatch enemyStopWatch = new();

        private readonly Stopwatch bulletStopWatch = new();


        public GameWindow(PlayerModel playerModel)
        {
            InitializeComponent();

            DataContext = this;

            #region Initialisatie
            GameModel = new() { PlayerModel = playerModel, HealthPoints = 200 };

            playerShip = new(fireRate: 100, friction: 0.92f, speed: 1.75f);


            Canvas.SetLeft(PlayerShape, SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetBottom(PlayerShape, PlayerShape.Height);

            GameCanvas.Focus();

            #endregion


            #region Verwerking
            startTime = Stopwatch.GetTimestamp();

            gameTimer.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            enemyStopWatch.Start();
            bulletStopWatch.Start();

            #endregion
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            GameModel.ElapsedTime = Stopwatch.GetElapsedTime(startTime);

            List<Rectangle> enemies =
            GameCanvas.Children.OfType<Rectangle>()
            .Where(r => r.Tag is EnemyShip).ToList();
            List<Rectangle> bullets =
            GameCanvas.Children.OfType<Rectangle>()
            .Where(r => r.Tag is string && r.Tag.ToString() == "bullet").ToList();

            DrawEnemy();

            //DrawBullet();

            ProcesPlayerInteraction();

            UpdateEnemy(enemies, bullets);

            CheckGameOver();

            Reset(enemies, bullets);
        }

        private void Reset(List<Rectangle> enemies, List<Rectangle> bullets)
        {
            foreach (Rectangle enemy in enemies)
            {
                EnemyShip currentEnemy = (EnemyShip)enemy.Tag;
                if (enemy.Height < bottomBorder - 60)
                {
                    GameCanvas.Children.Remove(enemy);
                }
                else
                {
                    double enemyPosition = Canvas.GetBottom(enemy);
                    Canvas.SetBottom(enemy, enemyPosition - currentEnemy.Speed);
                }
            }

            foreach (Rectangle bullet in bullets)
            {
                if (bullet.Height > topBorder + 60)
                {
                    GameCanvas.Children.Remove(bullet);
                }
                else
                {
                    double bulletPosition = Canvas.GetBottom(bullet);
                    Canvas.SetBottom(bullet, bulletPosition + 20);
                }
            }

        }

        private void CheckGameOver()
        {
            
        }

        private void UpdateEnemy(List<Rectangle> enemies, List<Rectangle> bullets)
        {
            foreach (Rectangle enemy in enemies)
            {
                Rect enemyHitBox = new(
                    Canvas.GetLeft(enemy),
                    Canvas.GetBottom(enemy),
                    enemy.Width, enemy.Height);
                Rect playerHitBox = new(
                    Canvas.GetLeft(PlayerShape),
                    Canvas.GetBottom(PlayerShape),
                    PlayerShape.Width, PlayerShape.Height);

                if (playerHitBox.IntersectsWith(enemyHitBox))
                {
                    EnemyShip currentEnemy = (EnemyShip)enemy.Tag;
                    GameModel.HealthPoints -= currentEnemy.AttackDamage;
                    GameCanvas.Children.Remove(enemy);
                }

                foreach (Rectangle bullet in bullets)
                {
                    Rect bulletHitBox = new(Canvas.GetLeft(bullet), Canvas.GetBottom(bullet),
                        bullet.Width, bullet.Height);

                    if (enemyHitBox.IntersectsWith(bulletHitBox))
                    {
                        GameCanvas.Children.Remove(enemy);
                        GameCanvas.Children.Remove(bullet);
                        GameModel.Score += 1;
                    }
                }
            }

        }

        private void ProcesPlayerInteraction()
        {
            if (leftKeyPressed && Canvas.GetLeft(PlayerShape) > leftBorder)
            {
                playerShip.MoveLeft();
            }
            if (rightKeyPressed && Canvas.GetLeft(PlayerShape) < rightBorder)
            {
                playerShip.MoveRight();
            }
            if (upKeyPressed && Canvas.GetBottom(PlayerShape) < topBorder)
            {
                playerShip.MoveUp();
            }
            if (downKeyPressed && Canvas.GetBottom(PlayerShape) > bottomBorder)
            {
                playerShip.MoveDown();
            }
            if (spaceKeyPressed)
            {
                DrawBullet();
            }

            if (Canvas.GetLeft(PlayerShape) < leftBorder)
            {
                Canvas.SetLeft(PlayerShape, leftBorder);
                playerShip.MoveRight();
            }
            if (Canvas.GetLeft(PlayerShape) > rightBorder)
            {
                Canvas.SetLeft(PlayerShape, rightBorder);
                playerShip.MoveLeft();
            }
            if (Canvas.GetBottom(PlayerShape) < bottomBorder)
            {
                Canvas.SetBottom(PlayerShape, bottomBorder);
                playerShip.MoveUp();
            }
            if (Canvas.GetBottom(PlayerShape) > topBorder)
            {
                Canvas.SetBottom(PlayerShape, topBorder);
                playerShip.MoveDown();
            }
            playerShip.UpdateSpeed();
            Canvas.SetLeft(PlayerShape, Canvas.GetLeft(PlayerShape) - playerShip.XSpeed);
            Canvas.SetBottom(PlayerShape, Canvas.GetBottom(PlayerShape) - playerShip.YSpeed);

        }

        private void DrawEnemy()
        {
            double deltaTime = enemyStopWatch.ElapsedMilliseconds;
            if (deltaTime > enemySpawnRate)
            {
                int randomEnemyIndex = random.Next(enemyShips.Count);

                EnemyShip randomEnemy = enemyShips[randomEnemyIndex];

                ImageBrush enemySprite = randomEnemy.SpaceShipImage;

                Rectangle enemy = new()
                {
                    Tag = randomEnemy,
                    Height = 60,
                    Width = 60,
                    Fill = enemySprite
                };

                Canvas.SetBottom(enemy, topBorder + 200);
                Canvas.SetLeft(enemy, random.Next(leftBorder, rightBorder));
                GameCanvas.Children.Add(enemy);

                enemyStopWatch.Restart();
            }


        }
        private void DrawBullet()
        {
            double deltaTime = bulletStopWatch.ElapsedMilliseconds;
            if (deltaTime > playerShip.FireRate)
            {

                Rectangle bullet = new()
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Purple
                };

                Canvas.SetLeft(bullet, Canvas.GetLeft(PlayerShape) + PlayerShape.Width / 2);
                Canvas.SetBottom(bullet, Canvas.GetBottom(PlayerShape) + bullet.Height);
                GameCanvas.Children.Add(bullet);

                bulletStopWatch.Restart();
            }

        }


        #region PlayingField
        private const int boundary = 20;
        private readonly int leftBorder = boundary;
        private readonly int topBorder = (int)(SystemParameters.PrimaryScreenHeight - boundary - 60);
        private readonly int rightBorder = (int)(SystemParameters.PrimaryScreenWidth - boundary - 60);
        private readonly int bottomBorder = boundary;
        #endregion

        #region Enemies
        private const string assetLocation = "pack://application:,,,/assets";
        private readonly List<EnemyShip> enemyShips = new()
        {
            new EnemyGray(attackDamage: 5,
                spaceShipImage : $"{assetLocation}/enemySpriteOne.png", speed: 20),
            new EnemyGray2(attackDamage: 10,
                spaceShipImage : $"{assetLocation}/enemySpriteTwo.png", speed: 10),
            new EnemyGreen(attackDamage: 15,
                spaceShipImage : $"{assetLocation}/enemySpriteThree.png", speed: 5),
            new EnemyOrange(attackDamage: 20,
                spaceShipImage : $"{assetLocation}/enemySpriteFour.png", speed: 10)
        };
        #endregion



        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                leftKeyPressed = true;
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                rightKeyPressed = true;
            }
            else if (e.Key == Key.Up || e.Key == Key.W)
            {
                upKeyPressed = true;
            }
            else if (e.Key == Key.Down || e.Key == Key.S)
            {
                downKeyPressed = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.Space)
            {
                spaceKeyPressed = true;
            }
            GameCanvas.Focus();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                leftKeyPressed = false;
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                rightKeyPressed = false;
            }
            else if (e.Key == Key.Up || e.Key == Key.W)
            {
                upKeyPressed = false;
            }
            else if (e.Key == Key.Down || e.Key == Key.S)
            {
                downKeyPressed = false;
            }
            else if (e.Key == Key.Space || e.Key == Key.Space)
            {
                spaceKeyPressed = false;
            }
            GameCanvas.Focus();
        }


    }
}
