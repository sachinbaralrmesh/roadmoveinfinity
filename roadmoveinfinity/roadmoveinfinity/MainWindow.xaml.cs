using Microsoft.Win32;
using roadmoveinfinity.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace roadmoveinfinity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum signal_color { red, green, yellow, none };
        DispatcherTimer car_timer = new DispatcherTimer();
        DispatcherTimer signal_timer = new DispatcherTimer();

        bool space_key = false, up_key = true, down_key = false, right_key = false, left_key = false, zibra = false, zibra_signal = false, pedestrian_signal = false, pedestrian_zibra = false;
        int acceleration = 0, speed = 0, count = 0, i = 1;
        Random random = new Random();
        bool change_signals = true, car_collision_result = true, pedestrian_move_flag = false;
        int scor;
        TimeSpan ts = new TimeSpan(0, 0, 0, 0);
        SolidColorBrush get_signal_color = new SolidColorBrush();
        SolidColorBrush solidcolorbrush_red = new SolidColorBrush(Colors.Red);
        //var thread = new Thread(move_track,null);
        SolidColorBrush solidcolorbrush_yellow = new SolidColorBrush(Colors.Yellow);
        double trackline_1, trackline_2, image_left, image_right;

        DispatcherTimer stopwatch_timmer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        List<Score> ls_score = new List<Score>();
        string player_name = string.Empty;
        string notify = "";

        public MainWindow(string pname)
        {
            InitializeComponent();
            player_name = pname;
            intializesizecomponent();

        }
        //initializing the all components position in UI
        private void intializesizecomponent()
        {
            ls_score = new List<Score>();
            
            Canvas.SetTop(img_rct_road_side_right, -SystemParameters.PrimaryScreenHeight / 2);
            Canvas.SetTop(img_rct_road_side_left, -SystemParameters.PrimaryScreenHeight / 2);
            img_rct_road_side_right.Height = SystemParameters.PrimaryScreenHeight * 1.6;
            img_rct_road_side_right.Width = SystemParameters.PrimaryScreenWidth / 3;
            img_rct_road_side_left.Height = SystemParameters.PrimaryScreenHeight * 1.6;
            img_rct_road_side_left.Width = SystemParameters.PrimaryScreenWidth / 3;
            //Canvas.SetTop(rct_road_side_left, 0);

            Canvas.SetTop(image_car, SystemParameters.PrimaryScreenHeight - image_car.Height * 2);
            Canvas.SetLeft(image_car, SystemParameters.PrimaryScreenWidth / 2 + 20);

            Canvas.SetLeft(line_track2, SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetLeft(line_track1, SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetTop(line_track1, 20);
            Canvas.SetTop(line_track2, -SystemParameters.PrimaryScreenHeight - 20);

            rct_road.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
            rct_road.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            Canvas.SetTop(rct_road, 0);
            Canvas.SetLeft(rct_road, SystemParameters.PrimaryScreenWidth / 3);
            Canvas.SetLeft(img_rct_road_side_right, (SystemParameters.PrimaryScreenWidth / 3) * 2);
            Canvas.SetLeft(sp_signal, (img_rct_road_side_left.Width * 2) + 50);
            Canvas.SetLeft(sp_zibra, Canvas.GetLeft(rct_road));
            sp_zibra.Width = rct_road.Height;
            Canvas.SetLeft(sp_pedestrian_signal, (System.Windows.SystemParameters.PrimaryScreenWidth / 3.5));

            line_track1.Height = rct_road.Height;
            line_track2.Height = rct_road.Height;
            line_track1.Y2 = rct_road.Height;
            line_track2.Y2 = rct_road.Height;

            Canvas.SetTop(image_dog, -image_dog.Height);
            Canvas.SetLeft(image_dog, Canvas.GetLeft(sp_pedestrian_signal) + 20);
        }

        //setting the timmer to game
        private void stopwatch_timmer_tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours, ts.Minutes, ts.Seconds);
                ClockTextBlock.Text = currentTime;
            }
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                // ...
                case SessionSwitchReason.SessionLock:
                    if (stopWatch.IsRunning)
                        stopWatch.Stop();
                    break;
                case SessionSwitchReason.SessionUnlock:
                    stopWatch.Start();
                    stopwatch_timmer.Start();
                    break;
                    //case SessionSwitchReason.SessionLogoff:
                    //    MessageBox.Show("Total amount of time the system logged on:" + ClockTextBlock.Text);
                    //    break;
                    // ...
            }
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (car_timer.IsEnabled)
                {
                    stop_evrything();
                    space_key = true;
                }
                else
                {
                    start_everyting();
                    space_key = false;
                }
            }
            if (!space_key)
            {
                if (e.Key == Key.Up)
                {

                    right_key = false;
                    left_key = false;
                    up_key = true;
                    down_key = false;
                    if (acceleration < 10)
                    {
                        acceleration++;
                        speed = acceleration * 10;
                        speedTextBlock.Text = speed.ToString();
                    }
                    if (!car_timer.IsEnabled)
                    {
                        car_timer.Start();
                        signal_timer.Start();
                        stopwatch_timmer.Start();
                        stopWatch.Start();
                    }

                }
                if (e.Key == Key.Down)
                {
                    up_key = false;
                    left_key = false;
                    right_key = false;
                    down_key = true;
                    if (acceleration > 0)
                    {
                        acceleration--;
                        speed = acceleration * 10;
                        speedTextBlock.Text = speed.ToString();

                    }
                }
                if (e.Key == Key.Right)
                {
                    right_key = true;
                    left_key = false;
                }
                if (e.Key == Key.Left)
                {
                    right_key = false;
                    left_key = true;
                }
            }


            if (e.Key == Key.Escape)
            {
                stop_evrything();
                if (MessageBox.Show("ARE YOU WANT TO QUIT !", "waring", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    this.Close();
                   // Owner.Show();
                }
                else
                {
                    start_everyting();
                }
            }
            if (e.Key == Key.R)
            {
                if (MessageBox.Show("Do u want restart !", "waring", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    reset_all();

                }
                else
                {
                    this.Close();
                    //Owner.Show();
                }

            }

        }

        // re initializing all variables
        private void reset_all()
        {
            stop_evrything();
            Canvas.SetTop(sp_signal, -sp_signal.Height);
            Canvas.SetTop(sp_pedestrian_signal, -sp_pedestrian_signal.Height);
            Canvas.SetLeft(image_dog, Canvas.GetLeft(sp_pedestrian_signal) + 20);
            Canvas.SetTop(image_dog, -image_dog.Height);
            Canvas.SetTop(sp_zibra, -sp_zibra.Height);
            acceleration = 0;
            stopWatch.Reset();
            change_signals = true;
            count = 0;
            i = 1;
            speed = 0;
            scor = 0;
            car_collision_result = true;
        }

        //starting timmers
        private void start_everyting()
        {
            stopWatch.Start();
            stopwatch_timmer.Start();
            car_timer.Start();
            signal_timer.Start();
        }

        //stoping timmers
        private void stop_evrything()
        {
            stopWatch.Stop();
            stopwatch_timmer.Stop();
            car_timer.Stop();
            signal_timer.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // reset_all();
            settimmer();
        }

        //initializing the diapatchers with time
        private void settimmer()
        {
            signal_timer.Interval = TimeSpan.FromMilliseconds(1);
            signal_timer.Tick += new EventHandler(move_signals);
            car_timer.Interval = TimeSpan.FromMilliseconds(1);
            car_timer.Tick += new EventHandler(move_track);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            stopwatch_timmer.Tick += new EventHandler(stopwatch_timmer_tick);
            stopwatch_timmer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        //calling by timmer every miliseconds to move the car
        private void move_track(object sender, EventArgs e)
        {
            if (car_collision_detection())
            {
                if (up_key || down_key)
                {
                    scor = scor + acceleration;
                    tb_score_value.Text = ((int)(scor*0.01)).ToString();
                    trackline_1 = Canvas.GetTop(line_track1);
                    trackline_2 = Canvas.GetTop(line_track2);
                    settop(trackline_1, trackline_2);
                    if (trackline_1 < rct_road.Height)
                    {
                        Canvas.SetTop(line_track1, Canvas.GetTop(line_track1) + 1 * acceleration);
                    }
                    if (trackline_2 < rct_road.Height)
                    {
                        Canvas.SetTop(line_track2, Canvas.GetTop(line_track2) + 1 * acceleration);
                    }
                    //images moving
                    image_left = Canvas.GetTop(img_rct_road_side_left);
                    image_right = Canvas.GetTop(img_rct_road_side_left);
                    if (image_left < -30 && image_right < -30)
                    {
                        Canvas.SetTop(img_rct_road_side_left, Canvas.GetTop(img_rct_road_side_left) + 1 * acceleration);
                        Canvas.SetTop(img_rct_road_side_right, Canvas.GetTop(img_rct_road_side_right) + 1 * acceleration);

                    }
                    else
                    {
                        Canvas.SetTop(img_rct_road_side_left, -SystemParameters.PrimaryScreenHeight / 2);
                        Canvas.SetTop(img_rct_road_side_right, -SystemParameters.PrimaryScreenHeight / 2);
                    }

                }
                if (right_key)
                {

                    if (Canvas.GetLeft(image_car) + 55 <= Canvas.GetLeft(img_rct_road_side_right))
                    {

                        Canvas.SetLeft(image_car, Canvas.GetLeft(image_car) + 1 * 1);
                    }
                    else
                    {
                        notify = "Game Over Because you went to right pedestrian path";
                        gameover(notify);
                    }
                }
                if (left_key)
                {
                    if (Canvas.GetLeft(image_car) +5 >= Canvas.GetLeft(rct_road))
                    {
                        Canvas.SetLeft(image_car, Canvas.GetLeft(image_car) - 1 * 1);
                    }
                    else
                    {
                        notify = "Game Over Because you went to left pedestrian path";
                        gameover(notify);
                    }
                }

            }
            if (pedestrian_move_flag)
            {
                pedestrian_move();
            }

        }

        private void move_signals(object sender, EventArgs e)
        {

            set_type_traffic();
            if (zibra_signal)
            {
                if (Canvas.GetTop(sp_zibra) >= rct_road.Height)
                {
                    Canvas.SetTop(sp_signal, -sp_signal.Height);
                    Canvas.SetTop(sp_zibra, -sp_zibra.Height);
                    Canvas.SetTop(image_dog, -image_dog.Height);
                    zibra_signal = false;
                }
                else
                {
                    Canvas.SetTop(image_dog, Canvas.GetTop(image_dog) + (1 * acceleration));
                    Canvas.SetTop(sp_signal, (Canvas.GetTop(sp_signal) + (1 * acceleration)));
                    Canvas.SetTop(sp_zibra, (Canvas.GetTop(sp_zibra) + (1 * acceleration)));
                }

            }
            else if (zibra)
            {
                if (Canvas.GetTop(sp_zibra) >= rct_road.Height)
                {

                    Canvas.SetTop(sp_zibra, -sp_zibra.Height);
                    zibra = false;
                }
                else
                {
                    Canvas.SetTop(sp_zibra, (Canvas.GetTop(sp_zibra) + (1 * acceleration)));
                }
            }
            else if (pedestrian_signal)
            {
                if (Canvas.GetTop(sp_zibra) >= rct_road.Height)
                {
                    Canvas.SetTop(sp_pedestrian_signal, -sp_pedestrian_signal.Height);
                    Canvas.SetTop(sp_zibra, -sp_zibra.Height);
                    Canvas.SetTop(image_dog, -image_dog.Height);
                    pedestrian_signal = false;
                }
                else
                {
                    Canvas.SetTop(image_dog, Canvas.GetTop(image_dog) + (1 * acceleration));
                    Canvas.SetTop(sp_pedestrian_signal, (Canvas.GetTop(sp_pedestrian_signal) + (1 * acceleration)));
                    Canvas.SetTop(sp_zibra, (Canvas.GetTop(sp_zibra) + (1 * acceleration)));
                }
            }
            else if (pedestrian_zibra)
            {
                if (Canvas.GetTop(sp_zibra) >= rct_road.Height)
                {
                    Canvas.SetTop(image_dog, -image_dog.Height);
                    Canvas.SetTop(sp_zibra, -sp_zibra.Height);
                    pedestrian_zibra = false;
                }
                else
                {
                    Canvas.SetTop(image_dog, Canvas.GetTop(image_dog) + (1 * acceleration));
                    Canvas.SetTop(sp_zibra, (Canvas.GetTop(sp_zibra) + (1 * acceleration)));

                }
            }

        }

        //Randomly choicing the up coming signal 
        private void set_type_traffic()
        {
            count++;
            
            if (count == 100 * i)
            {    
                i++;
                if (change_signals)
                {
                    change_signal();
                    int number = random.Next(1, 5);
                    switch (number)
                    {
                        case 1:
                            if (Canvas.GetTop(sp_zibra) == -sp_zibra.Height)
                            {
                                zibra = true;
                            }
                            break;
                        case 2:
                            if (Canvas.GetTop(sp_zibra) == -sp_zibra.Height)
                            {
                                zibra_signal = true;
                                set_General_signal();
                            }
                            break;
                        case 3:
                            if (Canvas.GetTop(sp_zibra) == -sp_zibra.Height)
                            {
                                pedestrian_zibra = true;
                            }
                            break;
                        case 4:
                            if (Canvas.GetTop(sp_zibra) == -sp_zibra.Height)
                            {
                                pedestrian_signal = true;
                                set_pedestrian_signal();
                            }
                            break;

                    }

                }
            }
        }

        //Changing the signal based on signal set to red or yellow)
        public async void change_signal()
        {
            if (Canvas.GetTop(sp_pedestrian_signal) > 0)
            {
                if (get_signal_color.Color.ScA == solidcolorbrush_red.Color.ScA && get_signal_color.Color.ScR == solidcolorbrush_red.Color.ScR && get_signal_color.Color.ScG == solidcolorbrush_red.Color.ScG && get_signal_color.Color.ScB == solidcolorbrush_red.Color.ScB)
                //if(solidcolorbrush.Equals(get_signal_color))
                {
                        change_signals = false;
                        pedestrian_move_flag = true;
                        await Task.Delay(5000);
                        ellipse_ped_notpass.Fill = new SolidColorBrush(Colors.Black);
                        get_signal_color = new SolidColorBrush(Colors.Green);
                        ellipse_ped_pass.Fill = get_signal_color;
                        change_signals = true;
                        pedestrian_move_flag = false;
                }
            }
            if (Canvas.GetTop(sp_signal) > 0)
            {
               
                if (get_signal_color.Color.ScA == solidcolorbrush_red.Color.ScA && get_signal_color.Color.ScR == solidcolorbrush_red.Color.ScR && get_signal_color.Color.ScG == solidcolorbrush_red.Color.ScG && get_signal_color.Color.ScB == solidcolorbrush_red.Color.ScB)
                {
                    pedestrian_move_flag = true;
                    change_signals = false;
                     await Task.Delay(5000);
                        ellipse_red.Fill = new SolidColorBrush(Colors.Black);
                        get_signal_color = new SolidColorBrush(Colors.Yellow);
                        ellipse_yellow.Fill = get_signal_color;
                        //pedestrian_move();
                        change_signals = true;
                        pedestrian_move_flag = false;
                }
                if (get_signal_color.Color.ScA == solidcolorbrush_yellow.Color.ScA && get_signal_color.Color.ScR == solidcolorbrush_yellow.Color.ScR && get_signal_color.Color.ScG == solidcolorbrush_yellow.Color.ScG && get_signal_color.Color.ScB == solidcolorbrush_yellow.Color.ScB)
                {
                    get_signal_color = new SolidColorBrush(Colors.Green);
                    change_signals = false;
                    await Task.Delay(200);
                    ellipse_green.Fill = get_signal_color;
                    ellipse_yellow.Fill = new SolidColorBrush(Colors.Black);
                    change_signals = true;
                  //  pedestrian_move_flag = false;
                }
            }
        }

        //Randomly genrating pedestraian signal
        private void set_pedestrian_signal()
        {
            if (Canvas.GetTop(sp_zibra) >= rct_road.Height || Canvas.GetTop(sp_zibra) <= Canvas.GetTop(image_car))
            {
                int color_num = random.Next(1, 3);
                switch (color_num)
                {
                    case 1:
                        get_signal_color = new SolidColorBrush(Colors.Red);
                        ellipse_ped_notpass.Fill = get_signal_color;
                        ellipse_ped_pass.Fill = new SolidColorBrush(Colors.Black);
                        // pedestrian_move();
                        break;
                    case 2:
                        get_signal_color = new SolidColorBrush(Colors.Green);
                        ellipse_ped_notpass.Fill = new SolidColorBrush(Colors.Black);
                        ellipse_ped_pass.Fill = get_signal_color;

                        break;
                }
            }
        }

        // Randomly genrating genral signal 
        private void set_General_signal()
        {
            int color_num = random.Next(1, 4);

            switch (color_num)
            {
                case 1:
                    get_signal_color = new SolidColorBrush(Colors.Red);
                    ellipse_red.Fill = get_signal_color;
                    ellipse_green.Fill = new SolidColorBrush(Colors.Black);
                    ellipse_yellow.Fill = new SolidColorBrush(Colors.Black);
                    // pedestrian_move();
                    break;
                case 2:
                    get_signal_color = new SolidColorBrush(Colors.Green);
                    ellipse_red.Fill = new SolidColorBrush(Colors.Black);
                    ellipse_green.Fill = get_signal_color;
                    ellipse_yellow.Fill = new SolidColorBrush(Colors.Black);
                    break;
                case 3:
                    get_signal_color = new SolidColorBrush(Colors.Yellow);
                    ellipse_red.Fill = new SolidColorBrush(Colors.Black);
                    ellipse_yellow.Fill = get_signal_color;
                    ellipse_green.Fill = new SolidColorBrush(Colors.Black);
                    break;
            }

        }

        //setting the trackline position on top for moving
        private void settop(double trackline_1, double trackline_2)
        {
            if (trackline_1 >= rct_road.Height)
            {
                Canvas.SetTop(line_track1, -rct_road.Height - 20);
            }
            else if (trackline_2 >= rct_road.Height)
            {
                Canvas.SetTop(line_track2, -rct_road.Height - 20);
            }
        }

        private void start_people_move()
        {

        }
        // 
        private void start_signal()
        {
            // int sig = (int)signal_color(random.Next(0, 4));
        }

        //checking the car collision with zibra and when signals is on
        private bool car_collision_detection()
        {

            solidcolorbrush_red = new SolidColorBrush(Colors.Red);
            // MessageBox.Show("" + Canvas.GetTop(sp_zibra) + ">=" + Canvas.GetTop(image_car) + "" + SolidColorBrush + "==" + ellipse_red.Fill);
            if (get_signal_color.Color.ScA == solidcolorbrush_red.Color.ScA && get_signal_color.Color.ScR == solidcolorbrush_red.Color.ScR && get_signal_color.Color.ScG == solidcolorbrush_red.Color.ScG && get_signal_color.Color.ScB == solidcolorbrush_red.Color.ScB)
            //if(solidcolorbrush.Equals(get_signal_color))
            {
               // car_collision_result = false;
                notify = "signal is red ,but u crossed zibra cross so game over";
                checking_cross(notify);

            }
            if (get_signal_color.Color.ScA == solidcolorbrush_yellow.Color.ScA && get_signal_color.Color.ScR == solidcolorbrush_yellow.Color.ScR && get_signal_color.Color.ScG == solidcolorbrush_yellow.Color.ScG && get_signal_color.Color.ScB == solidcolorbrush_yellow.Color.ScB)
            {
                //car_collision_result = false;
                notify = "signal is yellow ,but u crossed zibra cross so game over";
                checking_cross(notify);
            }

            return car_collision_result;
        }

        //checking weather a car crossed the zibra line
        private void checking_cross(string notify)
        {
            if ((Canvas.GetTop(sp_zibra) + sp_zibra.Height) >= Canvas.GetTop(image_car))
            {
                gameover(notify);
            }

        }

        //game over and notification
        private void gameover(string notify)
        {
            car_collision_result = false;
            ls_score = MyStorage.readXml<List<Score>>("fun_traffic_score.xml");
            Score storescore = new Score() { playername = player_name, score =(int)( scor*0.01), whygameover = notify };
            ls_score.Add(storescore);
            MyStorage.storeXml<List<Score>>(ls_score, "fun_traffic_score.xml");
            stop_evrything();
            if (MessageBox.Show("Game Over your Score:" + tb_score_value.Text + "\n" + notify, "GameOver", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                //Mess = false;

                if (MessageBox.Show("Do you want to play", "GameOver", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    reset_all();

                }
                else
                {
                    //reset_all();
                    this.Close();
                    //Owner.Show();
                }
            }
            else
            {
                this.Close();
            }
        }

        //crossing the object through the zibra cross
        private void pedestrian_move()
        {
            if (Canvas.GetLeft(image_dog) < Canvas.GetLeft(img_rct_road_side_right)+20)
            {
                Canvas.SetLeft(image_dog, Canvas.GetLeft(image_dog) + 1.8);
            }
            else
            {
                pedestrian_move_flag = false;
                Canvas.SetLeft(image_dog, Canvas.GetLeft(sp_pedestrian_signal) + 20);
                // image_dog.Visibility = Visibility.Hidden;
            }
        }

        //public int dependenncyPropertyScore
        //{
        //    get { return (int)GetValue(dependenncyPropertyScoreProperty); }
        //    set { SetValue(dependenncyPropertyScoreProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for dependenncyPropertyScore.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty dependenncyPropertyScoreProperty =
        //    DependencyProperty.Register("dependenncyPropertyScore", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

    }
}
