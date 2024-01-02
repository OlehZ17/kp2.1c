using System;
using System.Windows.Forms;
using System.Threading;

namespace SumCalculation
{
    public class MainApp
    {
        [STAThread]
        static void Main()
        {
            // Створення головного вікна
            Form form = new Form();
            form.Text = "Обчислення суми ряду";

            // Створення компонентів для введення виразу, кнопки та відображення результату
            TextBox expressionBox = new TextBox();
            expressionBox.Width = 200;
            Button startButton = new Button();
            startButton.Text = "Почати обчислення";
            TextBox resultBox = new TextBox();
            resultBox.Width = 200;
            resultBox.ReadOnly = true;

            // Розташування компонентів на формі
            form.Controls.Add(expressionBox);
            form.Controls.Add(startButton);
            form.Controls.Add(resultBox);

            expressionBox.Location = new System.Drawing.Point(100, 30);
            startButton.Location = new System.Drawing.Point(150, 70);
            resultBox.Location = new System.Drawing.Point(100, 110);

            // Додавання події до кнопки
            startButton.Click += (sender, e) =>
            {
                string expression = expressionBox.Text;
                Thread calculationThread = new Thread(() => Calculation(expression, resultBox));
                calculationThread.Start();
            };

            // Налаштування параметрів форми
            form.Size = new System.Drawing.Size(400, 200);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormClosing += (sender, e) => Application.Exit();

            // Відображення форми
            Application.Run(form);
        }

        // Метод для обчислення суми в окремому потоці
        static void Calculation(string expression, TextBox resultBox)
        {
            // Отримання суми за виразом
            double sum = CalculateSum();
            // Виведення результату в поле
            resultBox.Invoke((MethodInvoker)delegate
            {
                resultBox.Text = sum.ToString();
            });
        }

        // Обчислення суми ряду з заданою точністю
        static double CalculateSum()
        {
            double sum = 0.0;
            double term = 1.0;
            int n = 0;
            double e = 1e-5;

            // Додаткове обчислення суми ряду з точністю e
            while (Math.Abs(term) > e)
            {
                sum += term;
                n++;
                term = 1.0 / Math.Pow(2, n);
            }

            return sum;
        }
    }
}
