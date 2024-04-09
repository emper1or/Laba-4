using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



namespace LogicalLayes
{
    
    public class Locgica
    {
        private  Dictionary<int, List<TextDictionary>> dic = new Dictionary<int, List<TextDictionary>>();
        
        

        public int len_c { get; set; }
        public bool ramdom { get; set; }
        public int selectedindex { get; set; }

        public string json { get; set; }
        public void SetAll(int c, bool r, int select)
        {
            len_c = c;
            ramdom = r;
            selectedindex = select;
            json = JsonConvert.SerializeObject(dic, Formatting.Indented);
        }

        public Locgica()
        {
            List<TextDictionary> td = new List<TextDictionary>();

            // Для первоначального заполнения файла

            TextDictionary td_1 = new TextDictionary { Text = "Привет", Number = 1 };
            TextDictionary td_2 = new TextDictionary { Text = "так как,", Number = 2 };
            TextDictionary td_3 = new TextDictionary { Text = "поэтому", Number = 3 };
            TextDictionary td_4 = new TextDictionary { Text = "Досвидания", Number = 4 };
            TextDictionary td_5 = new TextDictionary { Text = "здравствуйте", Number = 1 };

            td.Add(td_1);
            td.Add(td_2);
            td.Add(td_3);
            td.Add(td_4);
            td.Add(td_5);

            foreach (var textDictionary in td)
            {
                if (dic.ContainsKey(textDictionary.Number))
                {
                    dic[textDictionary.Number].Add(textDictionary);
                }

                else
                {
                    dic.Add(textDictionary.Number, new List<TextDictionary>());
                    dic[textDictionary.Number].Add(textDictionary);
                }
            }

            // Загрузка данных из файла (для  первого заполнения файла как и все строка до этого)
            //json = JsonConvert.SerializeObject(dic, Formatting.Indented);
            //File.WriteAllText("data.txt", json);

            json = File.ReadAllText("data.txt");
            var loadedDic = JsonConvert.DeserializeObject<Dictionary<int, List<TextDictionary>>>(json);

            dic = DeepCopy(loadedDic);

        }

        //функция для глубокого копирывая словарей

        static Dictionary<int, List<TextDictionary>> DeepCopy(Dictionary<int, List<TextDictionary>> original)
        {
            string json = JsonConvert.SerializeObject(original);
            return JsonConvert.DeserializeObject<Dictionary<int, List<TextDictionary>>>(json);
        }

        //функция для отображения текста из словаря под номером i 

        public string ShowThisDic(int i)
        {
            string answer = "";

            foreach (var textDictionary in dic)
            {
                if (textDictionary.Key == i)
                {
                    foreach (var td in textDictionary.Value)
                    {
                        answer += td.Text + "\n";
                    }
                }
            }

            return answer;
        }

        //функция для обновления записи словаря ( выполняется после нажатия кнопки сохранить данные на форме 2) 

        public void UploadThisDic(int i, string str)
        {
            dic[i] = new List<TextDictionary>();
            foreach (var h in str.Split('\n'))
            {
                if (h != "")
                {
                    dic[i].Add(new TextDictionary { Text = h, Number = i });
                }
            }

        }

        //функция для обновления файла псле обновления словаря ( выполняется после UploadThisDic на форме 2) 

        public void UploadAll()
        {
            json = JsonConvert.SerializeObject(dic, Formatting.Indented);
            File.WriteAllText("data.txt", json);
        }

        //функция для обновления словаря перед каждой генерацией текста (используется при нажатии кнопки Генерировать текст в форме 1)

        public void UpdateDic()
        {
            json = File.ReadAllText("data.txt");
            var loadedDic = JsonConvert.DeserializeObject<Dictionary<int, List<TextDictionary>>>(json);

            dic = DeepCopy(loadedDic);
        }

        // функция для создания строки по критериям пользователя

        public string CreateString()
        {
            foreach (var textdic in dic)
            {
                if (textdic.Value.Count == 0)
                {
                    return "Error Dic"; //ошибка заполнения словаря
                }
            }

            if (len_c <= 0)
            {
                return "Error Len"; //ошибка некорректной длины предложений
            }
           
            Random rndRandom = new Random();

            string answer = "";

            if (ramdom == false) // проверка рандомного задания предложения
            {
                for (int i = 0; i < len_c; i++) //количество предложений
                {
                    for (int j = 0; j < dic.Count; j++) // количесво слов в предложении
                    {
                        int this_rnd = rndRandom.Next(0, dic[j + 1].Count);

                        if (selectedindex == 0)
                        {
                            answer += dic[j + 1][this_rnd].Text.ToLower() + " ";
                        }


                        else if (selectedindex == 1)
                        {

                            answer += char.ToUpper(dic[j + 1][this_rnd].Text[0]) +
                                      dic[j + 1][this_rnd].Text.Substring(1, dic[j + 1][this_rnd].Text.Length - 1) +
                                      " ";
                        }

                        else
                        {
                            if (j == 0)
                            {
                                answer += char.ToUpper(dic[j + 1][this_rnd].Text[0]) +
                                          dic[j + 1][this_rnd].Text.Substring(1, dic[j + 1][this_rnd].Text.Length - 1) +
                                          " ";
                            }
                            else
                            {
                                answer += dic[j + 1][this_rnd].Text.ToLower() + " ";
                            }
                        }
                    }

                    answer += "\n";
                }

                return answer;
            }
            else
            {
                for (int i = 0; i < len_c; i++) //количество предложений
                {
                    for (int j = 0; j < dic.Count; j++) // количесво слов в предложении
                    {

                        int jplus = rndRandom.Next(0, dic.Count);

                        int this_rnd = rndRandom.Next(0, dic[jplus + 1].Count);

                        if (selectedindex == 0)
                        {
                            answer += dic[jplus + 1][this_rnd].Text.ToLower() + " ";
                        }


                        else if (selectedindex == 1)
                        {

                            answer += char.ToUpper(dic[jplus + 1][this_rnd].Text[0]) +
                                      dic[jplus + 1][this_rnd].Text.Substring(1, dic[jplus + 1][this_rnd].Text.Length - 1) +
                                      " ";
                        }

                        else
                        {
                            if (j == 0)
                            {
                                answer += char.ToUpper(dic[j + 1][this_rnd].Text[0]) +
                                          dic[jplus + 1][this_rnd].Text.Substring(1, dic[jplus + 1][this_rnd].Text.Length - 1) +
                                          " ";
                            }
                            else
                            {
                                answer += dic[jplus + 1][this_rnd].Text.ToLower() + " ";
                            }
                        }
                    }

                    answer += "\n";
                }

                return answer;
            }
        }

        //функция для подсчета количества символов в набранном тексте
        //используется во второй вкладке формы 1

        public string CountOfSymbols(string s)
        {
            int count = 0;

            int count_n = 0;

            foreach (var ss in s)
            {
                if (ss == '\n')
                { count_n++; }
            }

            string[] s_n = s.Split('\n');

            foreach (var s1 in s_n)
            {
                count+= s1.Length;
            }

            return (count - count_n).ToString();
        }

        //функция для подсчета количества слов в набранном тексте
        //используется во второй вкладке формы 1

        public string CountOfWords(string s)
        {
            return s.Split(new char[] { ' ', ',', '.', ':', ';', '!', '?', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length.ToString();
        }

        //функция для подсчета количества уникальных слов в набранном тексте
        //используется во второй вкладке формы 1

        public string CountOfUnicye(string s)
        {
            List<string> unic = new List<string>();

            s = s.ToLower();

            // Разделим текст на отдельные слова
            string[] words = s.Split(new char[] { ' ', ',', '.', ':', ';', '!', '?', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (!(unic.Contains(word)))
                {
                    unic.Add(word);
                }
            }

            return unic.Count.ToString();
        }
    }
    
    // класс для создания образца текста для заполнения словаря
    public class TextDictionary
    {
        public int Number { get; set; }
        public string Text { get; set; }
    }
}
