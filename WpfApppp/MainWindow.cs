using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApppp
{
    public partial class MainWindow
    {
        public int tomatos = 0;
        private DateTime dateForWeek = DateTime.Today;
        private DateTime dateForMonth = DateTime.Today;
        private DateTime dateForYear = DateTime.Today;
        private  DateTime date2;
        private int tomatForWeeks = 0;
        private int tomatForMonth = 0;
        private int tomatForYear = 0;

        public int exp = 0;
        public string[] rank = new string[] { "Новичок", "Эксперт", "Профессионал", "Магистр", "Рыцарь", "Король", "Властелин" };

        public int TomatosForWeek
        {
            get { return tomatForWeeks; }
            set { tomatForWeeks = value; }
        }
        public int TomatosForMonth
        {
            get { return tomatForMonth; }
            set { tomatForMonth = value; }
        }
        public int TomatosForYear
        {
            get { return tomatForYear; }
            set { tomatForYear = value; }
        }
        public int Tomatos
        {
            get { return tomatos; }
            set { tomatos = value; }
        }

        #region Реализация контроля дат
        private void DateForWeek()
        {
            int currentDay = DateChanging.Day;
            int passedDay = ADateForWeek.Day;
            if (Math.Abs(currentDay-passedDay) == 7)
            {
                TomatosForWeek = 0;
                ADateForWeek = DateTime.Today;
            }
        }
        private void DateForMonth()
        {
            int currentMonth = DateChanging.Month;
            int passedMonth = ADateForMonth.Month;
            if (currentMonth != passedMonth)
            {
                TomatosForMonth = 0;
                ADateForMonth = DateTime.Today;
            }
        }
        private void DateForYear()
        {
            int currentYear = DateChanging.Year;
            int passedYear = ADateForYear.Year;
            if (currentYear != passedYear)
            {
                TomatosForYear = 0;
                ADateForYear = DateTime.Today;
            }
        }
        public DateTime ADateForWeek
        {
            get { return dateForWeek;}
            set { dateForWeek = value; }
        }
        public DateTime ADateForMonth
        {
            get { return dateForMonth; }
            set { dateForMonth = value; }
        }
        public DateTime ADateForYear
        {
            get { return dateForYear; }
            set { dateForYear = value; }
        }
        public DateTime DateChanging
        {
            get { return date2; }
            set { date2 = value; }
        }
        #endregion

        #region Логика опыта
        private void Exp()//логика опыта
        {
            Random rand = new Random();
            int a = rand.Next(100);
            if(a >= 0 && a < 41)
            {
                exp += rand.Next(15, 22);
            }
            else if(a >= 41 && a < 71)
            {
                exp += rand.Next(23, 29);
            }
            else if (a >= 71 && a < 91)
            {
                exp += rand.Next(30, 40);
            }
            else
            {
                exp += rand.Next(40, 71);
            }
        }
        
        private void RankAndExp()// ранк и прогресс бар
        {
            if (exp>=0 && exp <= 100)
            {
                taskWindow.rankName.Content = rank[0];
                taskWindow.Progress.Minimum = 0;
                taskWindow.Progress.Maximum = 100;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/100");
            }
            else if (exp >= 100 && exp <= 400)
            {
                taskWindow.rankName.Content  = rank[1];
                taskWindow.Progress.Minimum = 100;
                taskWindow.Progress.Maximum = 400;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/400");
            }
            else if (exp >= 400 && exp <= 1000)
            {
                taskWindow.rankName.Content = rank[2];
                taskWindow.Progress.Minimum = 400;
                taskWindow.Progress.Maximum = 1000;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/1000");
            }
            else if (exp >= 1000 && exp <= 2000)
            {
                taskWindow.rankName.Content = rank[3];
                taskWindow.Progress.Minimum = 1000;
                taskWindow.Progress.Maximum = 2000;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/2000");
            }
            else if (exp >= 2000 && exp <= 3500)
            {
                taskWindow.rankName.Content = rank[4];
                taskWindow.Progress.Minimum = 2000;
                taskWindow.Progress.Maximum = 3500;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/3500");
            }
            else if (exp >= 3500 && exp <= 5000)
            {
                taskWindow.rankName.Content = rank[5];
                taskWindow.Progress.Minimum = 3500;
                taskWindow.Progress.Maximum = 5000;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/5000");
            }
            else if (exp >= 5000 && exp <= 7000)
            {
                taskWindow.rankName.Content = rank[6];
                taskWindow.Progress.Minimum = 5000;
                taskWindow.Progress.Maximum = 7000;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/7000");
            }
            else
            {
                taskWindow.rankName.Content = rank[7];
                taskWindow.Progress.Minimum = 7000;
                taskWindow.Progress.Maximum = 10000;
                taskWindow.Progress.Value = exp;
                taskWindow.ExpVis.Content = String.Format($"{exp}/10000");
            }

        }
        #endregion
    }
}
