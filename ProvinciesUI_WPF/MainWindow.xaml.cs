using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using ProvinciesBL.Beheerders;
using ProvinciesUtil;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProvinciesUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog fileDialog=new OpenFileDialog();
        private OpenFolderDialog folderDialog=new OpenFolderDialog();
        private ProvincieBeheerder provincieBeheerder;
        private string connectionString;
        private List<string> bestandsnamen = new();
        private string initFolderZip;
        private string initFolderOutput;

        private void SetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            connectionString = config.GetConnectionString("SQLserver");
            initFolderZip = config.GetSection("AppSettings")["initFolderZip"];
            initFolderOutput = config.GetSection("AppSettings")["initFolderOutput"];
            bestandsnamen.Add(config.GetSection("AppSettings")["ProvincieIDsVlaanderen"]);
            bestandsnamen.Add(config.GetSection("AppSettings")["ProvincieInfo"]);
            bestandsnamen.Add(config.GetSection("AppSettings")["Gemeentenaam"]);
            bestandsnamen.Add(config.GetSection("AppSettings")["StraatnaamID_gemeenteID"]);
            bestandsnamen.Add(config.GetSection("AppSettings")["straatnamen"]);
        }

        public MainWindow()
        {
            InitializeComponent();
            SetConfig();
            provincieBeheerder= new ProvincieBeheerder(ProvincieRepositoryFactory.GeefRepository(connectionString), ProvincieBestandslezerFactory.GeefBestandslezer());
            fileDialog.DefaultExt = "*.zip";
            fileDialog.Filter = "Zip files (.zip)|*.zip";
            fileDialog.Multiselect = false;
            fileDialog.InitialDirectory = initFolderZip;
            folderDialog.InitialDirectory =initFolderOutput;
        }

        private void ButtonZipFile_Click(object sender, RoutedEventArgs e)
        {
            bool? result=fileDialog.ShowDialog();
            if (result == true && !string.IsNullOrWhiteSpace(fileDialog.FileName)) 
            {
                TextBoxZipFile.Text = fileDialog.FileName;
                List<string> zipInhoud=provincieBeheerder.GeefInhoudZip(fileDialog.FileName);
                ListBoxZipFiles.ItemsSource= zipInhoud;
            }
        }

        private void ButtonOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            bool? result = folderDialog.ShowDialog();
            if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
            {
                TextBoxOutputFolder.Text = folderDialog.FolderName;
            }
        }
    }
}