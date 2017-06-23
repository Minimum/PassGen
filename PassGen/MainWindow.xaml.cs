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

namespace PassGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Generator = new PasswordGenerator();

            WarningTextOn = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            WarningTextOff = new SolidColorBrush(Color.FromArgb(0, 255, 0 , 0));

            WarningBoxOn = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            WarningBoxOff = new SolidColorBrush(Color.FromArgb(255, 94, 94, 94));

            InitializeComponent();
        }

        private readonly PasswordGenerator Generator;

        private readonly SolidColorBrush WarningTextOn;
        private readonly SolidColorBrush WarningTextOff;

        private readonly SolidColorBrush WarningBoxOn;
        private readonly SolidColorBrush WarningBoxOff;

        public bool CheckCharacterUsage()
        {
            bool valid = Generator.UseUppercaseLetters ||
                         Generator.UseLowercaseLetters ||
                         Generator.UseNumbers ||
                         Generator.UseSpecialCharacters;

            if (CharacterWarning != null)
            {
                CharacterWarning.Foreground = valid ? WarningTextOff : WarningTextOn;
            }

            return valid;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            int length = 0;

            if (CheckCharacterUsage() && Int32.TryParse(LengthField.Text, out length))
            {
                PasswordField.Text = Generator.Generate(length);
            }
        }

        private void UppercaseField_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UseUppercaseLetters = true;

            CheckCharacterUsage();
        }

        private void UppercaseField_Unchecked(object sender, RoutedEventArgs e)
        {
            Generator.UseUppercaseLetters = false;

            CheckCharacterUsage();
        }

        private void LowercaseField_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UseLowercaseLetters = true;

            CheckCharacterUsage();
        }

        private void LowercaseField_Unchecked(object sender, RoutedEventArgs e)
        {
            Generator.UseLowercaseLetters = false;

            CheckCharacterUsage();
        }

        private void NumberField_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UseNumbers = true;

            CheckCharacterUsage();
        }

        private void NumberField_Unchecked(object sender, RoutedEventArgs e)
        {
            Generator.UseNumbers = false;

            CheckCharacterUsage();
        }

        private void SpecialField_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UseSpecialCharacters = true;

            CheckCharacterUsage();
        }

        private void SpecialField_Unchecked(object sender, RoutedEventArgs e)
        {
            Generator.UseSpecialCharacters = false;

            CheckCharacterUsage();
        }

        private void WhitespaceField_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UseWhitespace = true;
        }

        private void WhitespaceField_Unchecked(object sender, RoutedEventArgs e)
        {
            Generator.UseWhitespace = false;
        }

        private void LengthField_TextChanged(object sender, TextChangedEventArgs e)
        {
            int length = 0;

            if (LengthWarning != null)
            {
                if(!Int32.TryParse(LengthField.Text, out length) || length < 1)
                {
                    LengthField.BorderBrush = WarningBoxOn;

                    LengthWarning.Foreground = WarningTextOn;
                }
                else
                {
                    LengthField.BorderBrush = WarningBoxOff;

                    LengthWarning.Foreground = WarningTextOff;
                }
            }
        }
    }
}
