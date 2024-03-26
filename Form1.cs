using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LogicalLayes;



namespace Laba_4
{
    public partial class Form1 : Form
    {
        private Locgica Logical;
        private Form2 form2;


        public Form1()
        {
            InitializeComponent();
            Logical = new Locgica();
            
        }

        // кнопка для вызова формы 2

        public void button2_Click_1(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Form1 = this;
            form2.Show();
        }


        //кнопка для генерации текста и отоборажения статистики

        private void button1_Click(object sender, EventArgs e)
        {
            Logical.UpdateDic();

            if (int.TryParse(textBox1.Text, out int numericValue))
            {
                Logical.SetAll(Convert.ToInt32(textBox1.Text), checkBox1.Checked, comboBox1.SelectedIndex);

                string cur_str = Logical.CreateString();

                if (cur_str != "Error Dic" && cur_str != "Error Len")
                {
                    richTextBox1.Text = cur_str;

                    RefreshGraph(cur_str);


                    label5.Text = $"Общее количество символов: {Logical.CountOfSymbols(cur_str)}";
                    label6.Text = $"Общее количество слов: {Logical.CountOfWords(cur_str)}";
                    label7.Text = $"Общее количество уникальных слов: {Logical.CountOfUnicye(cur_str)}";
                }

                else if(cur_str == "Error Dic")
                {
                    MessageBox.Show("Словарь данных пустой! Загрузите данные!");
                }

                else
                {
                    MessageBox.Show("Введите корректную длинну текста в предложениях!");
                }

            }

            else
            {
                MessageBox.Show("Введите корректные данные!");
            }
        }

        //функция для отрисовки диограммы

        private void RefreshGraph(string usingString)
        {

            chart1.Series.Clear();

            chart1.Series.Add("Часто встречающиеся слова");

            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;


            string text = usingString;

            // Приведем текст к нижнему регистру, чтобы учитывать слова независимо от регистра
            text = text.ToLower();

            // Разделим текст на отдельные слова
            string[] words = text.Split(new char[] { ' ', ',', '.', ':', ';', '!', '?', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Создадим словарь для подсчета количества вхождений каждого слова
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            // Подсчитаем количество вхождений каждого слова
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

            // Отсортируем слова по количеству вхождений в обратном порядке
            var sortedWords = wordCount.OrderByDescending(pair => pair.Value);

            // Выведем самые часто встречающихся слова
            int count = 0;
            foreach (var pair in sortedWords)
            {
                if (count < 5)
                {
                    chart1.Series[0].Points.AddXY(pair.Key, pair.Value);
                    count++;
                }
                else
                {
                    break;
                }
            }
        }

    }
}
