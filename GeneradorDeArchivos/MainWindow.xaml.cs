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
            if (!ulong.TryParse(txtFileSize.Text, out ulong fileSize))
            {
                MessageBox.Show("Debe ingresar un valor entero para el campo de tamaño del archivo"
                    , "Revise el tamaño de archivo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog cmdSave = new SaveFileDialog();
            cmdSave.ShowDialog();
            string rutaArchivo = cmdSave.FileName;

            if (string.IsNullOrWhiteSpace(rutaArchivo)) return;

            if ((bool)rbBinario.IsChecked)
            {
                GenerateBinaryFile(rutaArchivo, fileSize);
            }
            else
            {
                bool asciiShortRange = (bool)rbRango32_126.IsChecked ? true: false;
                GenerateTextFile(rutaArchivo, fileSize, asciiShortRange);
            }

            MessageBox.Show("Archivo generado satisfactoriamente", "Generador de archivos", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void GenerateTextFile(string rutaArchivo, ulong fileSize, bool asciiShortRange)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                for (ulong i = 0; i < fileSize; i++)
                    sw.Write(GetRandomChar(asciiShortRange));
            }
        }


        static void GenerateBinaryFile(string rutaArchivo, ulong longitud)
        {
            // Utiliza Random para generar bytes aleatorios
            Random random = new Random();

            // Crea un array de bytes con datos aleatorios
            byte[] datosAleatorios = new byte[longitud];
            random.NextBytes(datosAleatorios);

            // Escribe los bytes en el archivo
            File.WriteAllBytes(rutaArchivo, datosAleatorios);
        }

        private char GetRandomChar(bool asciiShortRange)
        { 
            if (asciiShortRange)
                return (char)_random.Next(32, 126);
            else
                return (char)_random.Next(0, 126);
        }

        private void rbEntropiaMinima_Checked(object sender, RoutedEventArgs e)
        {   
            spJuegoCaracteres.IsEnabled = false;
            rbRango0_126.IsEnabled = false;
            rbRango32_126.IsEnabled = false;
        }

        private void rbEntropiaMaxima_Checked(object sender, RoutedEventArgs e)
        {
            spJuegoCaracteres.IsEnabled = true;
            rbRango0_126.IsEnabled = true;
            rbRango32_126.IsEnabled = true;
        }

    }
}
