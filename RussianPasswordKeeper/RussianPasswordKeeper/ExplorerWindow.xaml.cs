using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
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
using static System.Net.Mime.MediaTypeNames;

namespace TextFileKeeper
{
    /// <summary>
    /// Логика взаимодействия для ExplorerWindow.xaml
    /// </summary>
    public partial class ExplorerWindow : Window
    {
        

        public ExplorerWindow()
        {
            InitializeComponent();
        }

        public void ShowData(string data)
        {
            Texttb.Text = data;
        }

        public string? FileName { get; set; }
        public string? Login { get ; set; }
        public string? Password { get; set; }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                Title = "Select the save file full name",
                InitialDirectory = String.IsNullOrEmpty(FileName) ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : FileName,
            };

            if (fileDialog.ShowDialog() == true)
            {
                var FileName = fileDialog.FileName;

                this.Title = FileName;

                byte[] savedFileBytes = Cryptographer.Cryptographer.Crypt(Login + Password, Texttb.Text, true);

                // Save file
                ByteArrayToFile(FileName, savedFileBytes);

                MessageBox.Show($"Data is saved in ${FileName}!");
                
            }
        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception caught in process: {ex}");
                return false;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            Texttb.FontSize = slider.Value;
        }
    }
}
