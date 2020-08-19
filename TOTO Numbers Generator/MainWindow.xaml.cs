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

namespace TOTO_Numbers_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OneYearButton_Click(object sender, RoutedEventArgs e)
        {
            GetNumbers("../../Draws/TOTO 2019.txt");
        }

        private void FiveYearButton_Click(object sender, RoutedEventArgs e)
        {
            GetNumbers("../../Draws/TOTO last 5 years.txt");
        }

        private void TenYearButton_Click(object sender, RoutedEventArgs e)
        {
            GetNumbers("../../Draws/TOTO last 10 years.txt");
        }

        private void AllTimeButton_Click(object sender, RoutedEventArgs e)
        {
            GetNumbers("../../Draws/TOTO alltime.txt");
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> randomSixNumbers = new List<int>();
            GenerateRandomNumbers(randomSixNumbers);
            randomSixNumbers.Sort();
            DisplayTopSixNumbers(randomSixNumbers);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var message = "The '1 Year' button shows the most drawn numbers for last year.\n" +
                "The '5 Years' button shows the most drawn numbers for the previous 5 years.\n" +
                "The '10 Years' button shows the most drawn numbers for the previous 10 years.\n" +
                "The 'All Time' button shows the most drawn numbers since the beginning of the lottary in 1958.\n" +
                "The 'Random Six' button shows random six numbers. :)";
            var caption = "How to use:";
            var button = MessageBoxButton.OK;
            var icon = MessageBoxImage.Information;

            MessageBox.Show(message, caption, button, icon);
        }

        private void GetNumbers(string filePath)
        {
            string[] lines = ReadPath(filePath);

            List<int> numbers = new List<int>();

            AddNumbersToList(lines, numbers);

            List<int> topSixNumbers = new List<int>();

            GetTheTopSixNumbers(numbers, topSixNumbers);

            topSixNumbers.Sort();

            DisplayTopSixNumbers(topSixNumbers);
        }

        private void DisplayTopSixNumbers(List<int> topSixNumbers)
        {
            firstNumber.Text = Convert.ToString(topSixNumbers[0]);
            secondNumber.Text = Convert.ToString(topSixNumbers[1]);
            thirdNumber.Text = Convert.ToString(topSixNumbers[2]);
            fourthNumber.Text = Convert.ToString(topSixNumbers[3]);
            fifthNumber.Text = Convert.ToString(topSixNumbers[4]);
            sixthNumber.Text = Convert.ToString(topSixNumbers[5]);
        }

        private void GetTheTopSixNumbers(List<int> numbers, List<int> topSixNumbers)
        {
            for (int i = 0; i < 6; i++)
            {
                int topNumber = 0;
                int highestCount = 0;

                for (int j = 1; j < 50; j++)
                {
                    int currentNumberCount = 0;

                    if (topSixNumbers.Contains(j))
                    {
                        continue;
                    }

                    foreach (var item in numbers)
                    {
                        if (item == j)
                        {
                            currentNumberCount++;
                        }
                    }

                    if (currentNumberCount > highestCount)
                    {
                        highestCount = currentNumberCount;
                        topNumber = j;
                    }
                }

                if (!topSixNumbers.Contains(topNumber))
                {
                    topSixNumbers.Add(topNumber);
                }
            }
        }

        private void AddNumbersToList(string[] lines, List<int> numbers)
        {
            foreach (var item in lines)
            {
                int[] line = item.Split(',').Select(int.Parse).ToArray();

                for (int i = 0; i < 6; i++)
                {
                    numbers.Add(line[i]);
                }
            }
        }

        private string[] ReadPath(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }

        private void GenerateRandomNumbers(List<int> arr)
        {
            Random rnd = new Random();

            while (arr.Count < 6)
            {
                int number = rnd.Next(1, 50);

                if (arr.Contains(number))
                {
                    continue;
                }
                else
                {
                    arr.Add(number);
                }
            }
        }
    }
}
