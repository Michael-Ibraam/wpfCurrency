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

namespace currency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> lessThan20List = new List<string> { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        List<string> tensList = new List<string> { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string finalResult = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            finalResult = "";
            convertIntegerAndDecimal(input.Text.Trim());
            output.Text = finalResult;
        }

        public void convertIntegerAndDecimal(string inputString)
        {
            string[] inputArray = { };
            // Separate the integer part from the decimal part
            inputArray = inputString.Split(',');

            // Make sure number is valid format
            if (!inputArray[0].All(char.IsDigit))
            {
                invalidNumber();
                return;
            }

            // Special case for 0 dollars, to avoid "50" => Fifty Zero
            if (Int32.Parse(inputArray[0]) == 0)
            {
                finalResult = "Zero dollars";
            }
            else
            {
                //Special case for "1 dollar" to remove 's' from dollars
                if (Int32.Parse(inputArray[0]) == 1)
                {
                    finalResult = "One dollar";
                }
                else
                {   //Convert Integer part of the number
                    finalResult = convertInteger(Int32.Parse(inputArray[0])) + " dollars";
                }
            }

            // Check if the input is decimal/double
            if (inputArray.Length > 1)
            {
                // Make sure number is valid format
                if (!inputArray[1].All(char.IsDigit))
                {
                    invalidNumber();
                    return;
                }
                finalResult += " and " + convertDecimal(inputArray[1]);
            }
        }

        private void invalidNumber()
        {
            finalResult = "The number is not in correct fromat";
        }

        private string convertInteger(int inputNumber)
        {
            string IntegerResult = "";
            int numberOfDigits = (inputNumber + "").Length;
            // Will go through each digit and convert it
            switch (numberOfDigits)
            {
                case 0:
                    finalResult = "No valid number found";
                    break;

                case 1:
                    if (inputNumber == 0)
                    {
                        IntegerResult += "";
                    }
                    else
                    {
                        IntegerResult += convertOneDigit(inputNumber);
                    }
                    break;

                case 2:
                    if (inputNumber < 20)
                    {
                        IntegerResult += convertOneDigit(inputNumber);
                    }
                    else
                    {
                        IntegerResult += convertTwoDigits(inputNumber);
                    }
                    break;

                case 3:
                    IntegerResult += convertThreeDigits(inputNumber);
                    break;

                case 4:
                    IntegerResult += convertFourDigits(inputNumber);
                    break;

                case 5:
                    IntegerResult += convertFiveDigits(inputNumber);
                    break;

                case 6:
                    IntegerResult += convertSixDigits(inputNumber);
                    break;

                case 7:
                    IntegerResult += convertSevenDigits(inputNumber);
                    break;

                case 8:
                    IntegerResult += convertEightDigits(inputNumber);
                    break;

                case 9:
                    IntegerResult += convertNinetDigits(inputNumber);
                    break;

                default:
                    IntegerResult += "Empty";
                    break;
            }
            return IntegerResult;
        }

        private string convertDecimal(string centString)
        {
            string centResult = "";
            // Case 5.00
            if (Int32.Parse(centString) == 0)
            {
                centResult = "Zero";
            }
            // Case 5.05
            if (centString[0] == 0)
            {
                centResult += convertOneDigit(Int32.Parse(centString));
            }
            else
            {
                // Case 5.1, 5.9
                if (centString.Length == 1)
                {
                    centResult += convertTwoDigits(Int32.Parse(centString) * 10);

                }
                // Length has 2 digits, case 5.10, 5.99
                else
                {
                    centResult += convertTwoDigits(Int32.Parse(centString));
                }
            }
            centResult += " cents";

            return centResult;
        }

        private string convertOneDigit(int inputNumber)
        {
            // to avoid "50" => "Fifty Zero"
            if (inputNumber != 0)
            {
                return lessThan20List[inputNumber];
            }
            return "";
        }
        private string convertTwoDigits(int inputNumber)
        {
            if (inputNumber < 20)
            {
                return convertInteger(inputNumber);
            }
            string unitString = convertInteger(inputNumber % 10);
            // If unitString is not empty for example 21, add '-' between units and tens, else for example :20, 50, 60 add nothing
            return tensList[(inputNumber / 10) % 10] +(unitString != "" ? "-" + unitString : "") ;
        }
        private string convertThreeDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                return lessThan20List[(inputNumber / 100) % 10] + " Hundred " + convertInteger(inputNumber % 100);
            }
            return "";
        }
        private string convertFourDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                return lessThan20List[(inputNumber / 1000) % 10] + " Thousand " + convertInteger(inputNumber % 1000);
            }
            return "";
        }
        private string convertFiveDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                int firstTwoDigits = (inputNumber / 1000) % 100;
                return convertInteger(firstTwoDigits) + " Thousand " + convertInteger(inputNumber - firstTwoDigits * 1000);
            }
            return "";
        }
        private string convertSixDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                int firstThreeDigits = (inputNumber / 1000) % 1000;
                return convertInteger(firstThreeDigits) + " Thousand " + convertInteger(inputNumber - firstThreeDigits * 1000);
            }
            return "";
        }
        private string convertSevenDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                int firstDigit = (inputNumber / 1000000) % 1000000;
                return convertInteger(firstDigit) + " Million " + convertInteger(inputNumber - firstDigit * 1000000);
            }
            return "";
        }
        private string convertEightDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                int firstTwoDigits = (inputNumber / 1000000) % 1000000;
                return convertInteger(firstTwoDigits) + " Million " + convertInteger(inputNumber - firstTwoDigits * 1000000);
            }
            return "";
        }
        private string convertNinetDigits(int inputNumber)
        {
            if (inputNumber != 0)
            {
                int firstThreeDigits = (inputNumber / 1000000) % 1000000;
                return convertInteger(firstThreeDigits) + " Million " + convertInteger(inputNumber - firstThreeDigits * 1000000);
            }
            return "";
        }
    }
}
