using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.Windows.Shapes;

using System.Windows.Threading;
namespace L6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        
        List<Rectangle> itemRemover = new List<Rectangle>();
        
        #region Variable defenition
        bool moveLeft, moveRight;
        bool overheating;

        int enemyCounter = 100;
        int playerSpeed = 10;
        int limit = 50;
        int score = 0;
        int damage = 0;
        int enemySpeed = 10;
        int shootsLimit = 30;
        int persentage = 0;
        int shoots = 0;
        int maxPersentage;
        bool scoreFlag = false;
        bool dificultSet = false;
        Rect playerHitBox;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            
            gameTimer.Interval = TimeSpan.FromMilliseconds(15);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            MyCanvas.Focus();

            MyCanvas.Background = SpriteFactory.getSprite("background").getImageBrush();
            
            player.Fill = SpriteFactory.getSprite("player").getImageBrush();
        }

        
        private void GameLoop(object sender, EventArgs e)
        {
            Menu menu = Owner as Menu;
            if (menu != null && dificultSet == false)
            {
                SetDificultLevel(menu.dificultLevel);
                dificultSet = true;
            }

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            enemyCounter -= 1;

            scoreText.Content = "Score: " + score;
            damageText.Content = "Damage: " + damage;

            if (enemyCounter < 0)
            {
                MyCanvas.Children.Add(SpriteFactory.getSprite("enemy").getRectangle());
                enemyCounter = limit;
            }

            #region Move player sprite
            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            #endregion

            #region  Damage and Score updates
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if(Canvas.GetTop(x) < 40)
                    {
                        itemRemover.Add(x);
                    }

                    foreach(var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            
                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                score++;
                                scoreFlag = true;
                            }
                        }
                    }
                }

                if(x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);
                        damage += 10;
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        itemRemover.Add(x);
                        damage += 5;
                    }
                }
            }
            #endregion

            foreach (Rectangle i in itemRemover)
            {
                MyCanvas.Children.Remove(i);
            }

           if (score % 20 == 0 && scoreFlag)
            {
                limit = limit - 5;
                playerSpeed = playerSpeed + 1;
                enemySpeed = enemySpeed + 1;
                scoreFlag = false;
            }
           
            if (overheating && persentage > 0)
            {
                shootsText.Content = "Coll it down..." + persentage;
                shootsText.Foreground = Brushes.Blue;
                persentage -= 1;
            }
            if (persentage <= 0)
            {
                persentage = maxPersentage;
                overheating = false;
                shoots = 0;
                shootsText.Foreground = Brushes.Black;
                shootsText.Content = "Overgeating: 0%";
            }
            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Damage: 100";
                damageText.Foreground = Brushes.Red;
                MessageBox.Show("You have destroyed " + score + " enemies!" + Environment.NewLine + "Press OK to play again", "Game over");

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            
           
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
        }

        private void OneKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
            if (e.Key == Key.Space)
            {
                if (shoots < shootsLimit && overheating == false)
                {
                    shoots++;
                    overheating = false;
                   Rectangle newBullet = new Rectangle
                    {
                        Tag = "bullet",
                        Height = 20,
                        Width = 5,
                        Fill = Brushes.White,
                        Stroke = Brushes.Red
                    };
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                    Canvas.SetTop(newBullet, Canvas.GetTop(player) - player.Height);

                    MyCanvas.Children.Add(newBullet);
                    shootsText.Content = "Overheating: " + shoots * 100 / shootsLimit + "%";
                }
                if (shoots == shootsLimit)
                {
                    overheating = true;
                }
            }
        }

        private void SetDificultLevel(int dificult)
        { 
            switch(dificult)
            {
                case 0:
                    playerSpeed = 6;
                    maxPersentage = 15;
                    enemySpeed = 5;

                    break;
                case 1:
                    playerSpeed = 7;
                    maxPersentage = 25;
                    enemySpeed = 8;
                    break;
                case 2:
                    playerSpeed = 8;
                    maxPersentage = 35;
                    enemySpeed = 9;
                    break;
                default:
                    playerSpeed = 6;
                    maxPersentage = 20;
                    enemySpeed = 7;
                    
                    break;
            }

        }

        
    }
}
