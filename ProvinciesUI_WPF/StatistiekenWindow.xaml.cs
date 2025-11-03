using ProvinciesBL.Model;
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
using System.Windows.Shapes;

namespace ProvinciesUI_WPF
{
    /// <summary>
    /// Interaction logic for StatistiekenWindow.xaml
    /// </summary>
    public partial class StatistiekenWindow : Window
    {
        List<string> messages = new();
        public StatistiekenWindow(Statistieken stats,string zipFile)
        {
            
            InitializeComponent();
            TextBoxZip.Text = zipFile;
            messages.AddRange(stats.ProvinciesAantalGemeenten.Select(x=>$"{x.Key}:{x.Value} gemeenten"));
            messages.AddRange(stats.GemeentenAantalStraten.Select(x => $"{x.Key}:{x.Value} straten"));
            ListBoxStatistieken.ItemsSource = messages;
        }

        private void ButtonSluitVenster_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
