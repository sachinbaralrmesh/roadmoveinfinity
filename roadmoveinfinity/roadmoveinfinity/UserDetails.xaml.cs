using roadmoveinfinity.Class;
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
using System.Xml.Linq;

namespace roadmoveinfinity
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        MainWindow mainwindow;
        public List<Score> ls_score = new List<Score>();
       // bool result = true;
        List<Score> top_five_score = new List<Score>();
        public UserDetails()
        {
            top_five();
            InitializeComponent();
            listBox_topscorelist.ItemsSource = top_five_score;
        }

        private void top_five()
        {
            ls_score = MyStorage.readXml<List<Score>>("fun_traffic_score.xml");
            List<int> ls = new List<int>();
            //int k = 0;
            for (int i = 0; i < ls_score.Count; i++)
            {
                ls.Add(ls_score[i].score);
            }
            ls.Sort();
            int top, pre,count=0;
            for (int i = ls.Count - 1; i > -1; i--)
            {
                top = ls[i];
                for (int j = 0; j < ls_score.Count; j++)
                {
                    pre = ls_score[j].score;
                    if (top == pre)
                    {
                        if (top_five_score.Count == 0)
                        {
                            top_five_score.Add(ls_score[j]);
                        }
                        else
                        {
                            for (int p = 0; p < top_five_score.Count; p++)
                            {
                                if (top_five_score[p].playername != ls_score[i].playername && top == ls_score[j].score)
                                {
                                    count = 1;
                                }
                                else if (top_five_score[p].playername == ls_score[i].playername && top == ls_score[j].score)
                                {
                                    count = 1;
                                }
                                else
                                {
                                    count = 0;
                                }
                            }
                            if (count == 1)
                            {
                                if (top_five_score.Count < 5)
                                {
                                    top_five_score.Add(ls_score[j]);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //verifiying the textbox player name
            if (txt_playername.Text == "")
            {
                //result = false;
                MessageBox.Show("please enter the player name!");
                
            }
            else
            {
                mainwindow = new MainWindow(txt_playername.Text);
                mainwindow.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            top_five();
        }
    }
}
