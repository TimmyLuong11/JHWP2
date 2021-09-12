using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace JHWP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, List<Data>> Data = new Dictionary<string, List<Data>>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileBt_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads"; //This will get the path to their downloads directory
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path;
            ofd.Filter = "Comma Seperated Values document (.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                PopulatedData(ofd.FileName);
                PopulateListBox("Male", LstM);
                PopulateListBox("Female", LstF);
                PopulateListBox("Both", MFbox);
                PopulateListBoxforMeanGreaterThan();
            }
        }
        private void PopulateListBox(string gender, ListBox Lst)
        {
            double maxMean = 0;
            foreach (var item in Data.Keys)
            {
                foreach (var gend in Data[item])
                {
                    if (gender.ToLower() == gend.Gender.ToLower())
                    {
                        if (gend.Mean > maxMean)
                        {
                            maxMean = gend.Mean;
                        }
                    }
                }
            }
            foreach (var state in Data.Keys)
            {
                foreach (var gend in Data[state])
                {
                    if (gender.ToLower() == gend.Gender.ToLower())
                    {
                        if (gend.Mean == maxMean)
                        {
                            if (gender.ToLower() == "male")
                            {
                                Lst.Items.Add(state);
                            }
                            else if (gender.ToLower() == "female")
                            {
                                Lst.Items.Add(state);
                            }
                            else if (gender.ToLower() == "both")
                            {
                                Lst.Items.Add(state);
                            }
                        }
                    }
                }
            }
        }
        private void PopulateListBoxforMeanGreaterThan()
        {
            double mean = 8;
            foreach (var state in Data.Keys)
            {
                foreach (var gend in Data[state])
                {
                    if ("both".ToLower() == gend.Gender.ToLower())
                    {
                        if (gend.Mean >= mean)
                        {
                            GreatBox.Items.Add(state);
                        }
                    }
                }
            }
        }
        private void PopulatedData(string file)
        {
            var lines = File.ReadAllLines(file);
            string state = "";
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var pieces = line.Split(',');
                if (string.IsNullOrWhiteSpace(pieces[0]) == false)
                {
                    state = pieces[0];
                }
                double mean;
                int n;
                if (double.TryParse(pieces[2], out mean) == false)
                {
                    continue;
                }
                if (int.TryParse(pieces[3], out n) == false)
                {
                    continue;
                }
                if (Data.ContainsKey(state) == true)
                {
                    Data[state].Add(new Data()
                    {
                        State = state,
                        Gender = pieces[1],
                        Mean = mean,
                        N = n
                    });
                }
                else
                {
                    Data.Add(state, new List<Data>());
                    Data[state].Add(new Data()
                    {
                        State = state,
                        Gender = pieces[1],
                        Mean = mean,
                        N = n
                    });
                }
            }
        }
    }
}
