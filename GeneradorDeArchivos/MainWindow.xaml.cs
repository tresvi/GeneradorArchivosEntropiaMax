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

namespace GeneradorDeArchivos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Random _random;

        public MainWindow()
        {
            InitializeComponent();
            _random = new Random(DateTime.Now.Millisecond);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog cmdSave = new SaveFileDialog();
            cmdSave.ShowDialog();
            string rutaArchivo = cmdSave.FileName;

            const ulong FILE_SIZE = 1000;

            if (string.IsNullOrWhiteSpace(rutaArchivo)) return;

            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                for (ulong i = 0; i < FILE_SIZE; i++ )
                    sw.Write(GetRandomChar());
            }

            MessageBox.Show("Archivo generado satisfactoriamente", "Generador de archivos", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private char GetRandomChar()
        { 
            return (char)_random.Next(0, 126);
        }

        private void rbEntropiaMinima_Checked(object sender, RoutedEventArgs e)
        {
            txtCaracterFijo.IsEnabled = true;
            
            spJuegoCaracteres.IsEnabled = false;
            rbRango0_126.IsEnabled = false;
            rbRango32_126.IsEnabled = false;
        }

        private void rbEntropiaMaxima_Checked(object sender, RoutedEventArgs e)
        {
            txtCaracterFijo.IsEnabled = false;
            
            spJuegoCaracteres.IsEnabled = true;
            rbRango0_126.IsEnabled = true;
            rbRango32_126.IsEnabled = true;
        }
    }
}
