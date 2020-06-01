using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace WpfApppp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        private DateTime _startCountdown; // время запуска таймера
        private TimeSpan _startTimeSpan = TimeSpan.FromSeconds(5);
        private TimeSpan _startTimeSpan2 = TimeSpan.FromSeconds(3);// начальное время до окончания таймера
        private TimeSpan _startTimeSpan3 = TimeSpan.FromSeconds(7);
        private TimeSpan _timeToEnd; // время до окончания таймера. Меняется когда таймер запущен
        private TimeSpan _interval = TimeSpan.FromMilliseconds(300); // интервал таймера
        private DateTime _pauseTime;
        public int whatistimer = 0;// 0 work,1 break

        public TaskWindow taskWindow;

        private DispatcherTimer _timer;
        private DispatcherTimer _timer2;
        private DispatcherTimer _timer3;

        private MediaPlayer Ew;
        private MediaPlayer Eb;
        private MediaPlayer BreakSound;

        public bool proverkaMusicStart = false;
        public bool proverkaMusicEnd = false;
        public int LongCount = 0;

        //TaskWindow a = new TaskWindow();

        public MainWindow()
        {
            InitializeComponent();
        }
        public void WorkTimer()// таймер работы
        {
            whatistimer = 1;
            _StartTimeSpan = TimeSpan.FromSeconds(5);
            _timer = new DispatcherTimer();
            _timer.Interval = _interval;
            _timer.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(_startCountdown);
                TimeToEnd = _StartTimeSpan.Subtract(elapsed);
                labelTimer.Content = TimeToEnd.ToString(@"mm\:ss");
            };
            LongCount++;
            if(LongCount == 4)
            {
                whatistimer = 2;
                LongCount = 0;
            }
        }
        public void BreakTimer()// таймер перерыва
        {
            whatistimer = 0;
            _StartTimeSpan = TimeSpan.FromSeconds(3);
            _timer2 = new DispatcherTimer();
            _timer2.Interval = _interval;
            _timer2.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(_startCountdown);
                TimeToEnd = _StartTimeSpan.Subtract(elapsed);
                labelTimer.Content = TimeToEnd.ToString(@"mm\:ss");
            };
        }
        public void LongBreakTimer()// таймер перерыва
        {
            whatistimer = 0;
            _StartTimeSpan = TimeSpan.FromSeconds(8);
            _timer3 = new DispatcherTimer();
            _timer3.Interval = _interval;
            _timer3.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(_startCountdown);
                TimeToEnd = _StartTimeSpan.Subtract(elapsed);
                labelTimer.Content = TimeToEnd.ToString(@"mm\:ss");
            };
        }
        public TimeSpan _StartTimeSpan
        {
            get { return _startTimeSpan; }
            set { _startTimeSpan = value; }
        }
        public TimeSpan _StartTimeSpan2
        {
            get { return _startTimeSpan2; }
            set { _startTimeSpan2 = value; }
        }
        public TimeSpan _StartTimeSpan3
        {
            get { return _startTimeSpan3; }
            set { _startTimeSpan3 = value; }
        }
        public DispatcherTimer Timer
        {
            get;
            set;
        }// конструктор таймера
        public DispatcherTimer Timer2
        {
            get;
            set;
        }
        public DispatcherTimer Timer3
        {
            get;
            set;
        }

        public TimeSpan TimeToEnd // логика времени до конца таймера + действия по оканчании таймера
        {
            get
            {
                return _timeToEnd;
            }
            set
            {
                _timeToEnd = value;
                if (value.TotalMilliseconds <= 0)
                {
                    //Действия по естественному окончанию таймера.
                    StopTimer();
                    if (whatistimer == 0)
                    {
                        MusicEndWork();
                    }
                    else MusicEndBreak();
                    Exp();
                    RankAndExp();
                    StartTimer(DateTime.Now); // тестовая шняга
                }
                OnPropertyChanged("StringCountdown");
            }
        }

        public string StringCountdown  // (неиспользуется)логика, что будет по достижении <минуты 
        {
            get
            {
                var frmt = TimeToEnd.Minutes < 1 ? "ss\\.ff" : "mm\\:ss";
                return _timeToEnd.ToString(frmt);
            }
        }

        public bool TimerIsEnabled //проверка работает ли таймер
        {
            get { return _timer.IsEnabled; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateChanging = DateTime.Now;
            // здесь можно делать то что нужно сделать при загрузке программы
            WorkTimer();
            taskWindow = new TaskWindow();
        }
        #region Методы кнопок
        private void EventStopTimer()
        {
            _timer.Stop();
            TimeToEnd = _StartTimeSpan;
            labelTimer.Content = TimeToEnd.ToString(@"mm\:ss");
        }
        private void StopTimer() //Естественная остановка таймера +выбираем новый таймер.
        {
                _timer.Stop();
                WhatIsTimer();
        }
        private void StartTimer(DateTime sDate)
        {
            _startCountdown = sDate;
            _timer.Start();
        }
        private void PauseTimer()
        {
            _timer.Stop();
            _pauseTime = DateTime.Now;
        }
        private void ReleaseTimer()
        {
            var now = DateTime.Now;
            var elapsed = now.Subtract(_pauseTime);
            _startCountdown = _startCountdown.Add(elapsed);
            _timer.Start();
        }
        #endregion
        #region Кнопки
        private void Start_click(object sender, RoutedEventArgs e)
        {
            StartTimer(DateTime.Now);
            StartB.Visibility = Visibility.Hidden;
            PauseB.Visibility = Visibility.Visible;
        }

        private void Stop_click(object sender, RoutedEventArgs e)
        {
            EventStopTimer();
            ReleaseB.Visibility = Visibility.Hidden;
            StopB.Visibility = Visibility.Hidden;
            StartB.Visibility = Visibility.Visible;
        }
        private void Pause_click(object sender, RoutedEventArgs e)
        {
            PauseTimer();
            PauseB.Visibility = Visibility.Hidden;
            ReleaseB.Visibility = Visibility.Visible;
            StopB.Visibility = Visibility.Visible;
        }
        private void Release_click(object sender, RoutedEventArgs e)
        {
            ReleaseTimer();
            ReleaseB.Visibility = Visibility.Hidden;
            StopB.Visibility = Visibility.Hidden;
            PauseB.Visibility = Visibility.Visible;
        }
        private void HideMainWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Cancel_Window(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее приложение.
            Application.Current.Shutdown();
        }// кнопка закрытия приложения
        #endregion
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)// Для перетаскивания окна за любую область.
        {
            DragMove();
        }
        private void CenterScreen(object sender, RoutedEventArgs e)//По умолчанию ставит окно в центр экрана.
        {
            Window wnd = new Window(); //- название твоего окна в WPF
            wnd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void CountTomatos()// общий счётчик томатов
        {
            Tomatos++;
            TomatosForWeek++;
            TomatosForMonth++;
            TomatosForYear++;
            countTomatos.Content = Tomatos;
            taskWindow.ForWeek.Content = TomatosForWeek;
            taskWindow.ForMonth.Content = TomatosForMonth;
            taskWindow.ForYear.Content = TomatosForYear;
            taskWindow.All.Content = Tomatos;
        }

        private void NewWindow(object sender, RoutedEventArgs e)//Профиль
        {
            // назначает владельцем окно main
            taskWindow.Owner = this;
            taskWindow.ForWeek.Content = TomatosForWeek;
            taskWindow.ForMonth.Content = TomatosForMonth;
            taskWindow.ForYear.Content = TomatosForYear;

            //Проверить, узнать как проверить.
            DateForWeek();
            DateForMonth();
            DateForYear();
            if (taskWindow.IsActive == false)
            {
                taskWindow.Show();
            }
        }
        private void WhatIsTimer()//тут переключаются таймеры +считаются томаты.
        {
            //_StartTimeSpan.ToString();
            if (whatistimer == 0)
            {
                WorkTimer();
                TimeToEnd = _startTimeSpan;
            }
            else if(whatistimer == 2)
            {
                CountTomatos();
                LongBreakTimer();
                TimeToEnd = _startTimeSpan3;
            }
            else
            {
                CountTomatos();
                BreakTimer();
                TimeToEnd = _startTimeSpan2;
            }
        }

        private void MusicEndWork()
        {
            if (!proverkaMusicStart)
            {
                Ew = new MediaPlayer();
                Ew.Open(new Uri(@"C:\Users\grrek\source\repos\WpfApppp\WpfApppp\Res\Roses.wav", UriKind.Absolute));
                Ew.Play();
            }
        }

        private void MusicEndBreak()
        {
            if (!proverkaMusicEnd)
            {
                Eb = new MediaPlayer();
                Eb.Open(new Uri(@"C:\Users\grrek\source\repos\WpfApppp\WpfApppp\Res\EndBreakSound.wav", UriKind.Absolute));
                Eb.Play();
            }
        }

        private void StartB_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void StartB_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
