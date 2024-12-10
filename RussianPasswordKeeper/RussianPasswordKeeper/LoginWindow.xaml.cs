using Microsoft.Win32;
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
using static System.Net.Mime.MediaTypeNames;

namespace TextFileKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        bool isHidden = true;
        private void ShowHidePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isHidden)
            {
                isHidden = false;
                ShowHidePasswordBtn.Background = new ImageBrush(new ImageSourceConverter().ConvertFromString("./icon/hidesecret.png") as ImageSource);
                
                SecretTextBox.Text = SecretPasswordBox.Password;
                SecretPasswordBox.Visibility = Visibility.Hidden;
                ShowHideLabel.Content = "hide";
            }
            else
            {
                isHidden = true;
                ShowHidePasswordBtn.Background = new ImageBrush(new ImageSourceConverter().ConvertFromString("./icon/showsecret.png") as ImageSource);

                SecretPasswordBox.Password = SecretTextBox.Text;
                SecretPasswordBox.Visibility = Visibility.Visible;
                ShowHideLabel.Content = "show";
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ExplorerWindow explorerWindow = new ExplorerWindow();
            explorerWindow.FileName = "[Unsaved File]";
            explorerWindow.Title = explorerWindow.FileName;
            if (isHidden)
            {
                explorerWindow.Password = SecretPasswordBox.Password;  
            }
            else
            {
                explorerWindow.Password = SecretTextBox.Text;
            }
            explorerWindow.Login = Login.Text;
            explorerWindow.Show();
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            ExplorerWindow explorerWindow = new ExplorerWindow();
            if (isHidden)
            {
                explorerWindow.Password = SecretPasswordBox.Password;
            }
            else
            {
                explorerWindow.Password = SecretTextBox.Text;
            }
            explorerWindow.Login = Login.Text;

            var fileDialog = new OpenFileDialog
            {
                Title = "Select the save file full name",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                Multiselect = false
            };

            if (fileDialog.ShowDialog() == true)
            {
                var FileName = fileDialog.FileName;

                explorerWindow.Title = FileName;

                UnicodeEncoding unicode = new UnicodeEncoding();
                byte[] openFileBytes = Cryptographer.Cryptographer.Crypt(explorerWindow.Login + explorerWindow.Password, unicode.GetString(File.ReadAllBytes(FileName)), false);

                explorerWindow.ShowData(unicode.GetString(openFileBytes));

                explorerWindow.Visibility = Visibility.Visible;
            }
        }
    }
}