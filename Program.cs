using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Сторевая
{

    public class Program
    {
        //public static int findingPercent(int quantityNumbers, int percent)
        //{
        //    int quantity = quantityNumbers * percent / 100;
        //    return quantity;
        //}
        /// <summary>
        /// Заполнения массива итоговых оценок
        /// </summary>
        /// <param name="scoreAndNumber"> массив с оценкой и количесво этой оценки в ведомости</param>
        /// <param name="array">исходный массив с фамилиями</param>
        /// <param name="vip">число которое исключает, к примеру, первых 10 человек на получение двойки,
        /// по умолчанию для всех оценок оно равно "0", кроме оценки "2"</param>
        /// <returns></returns>
        public static int[] fillingArray(int[] scoreAndNumber, int[] array, int vip = 0)
        {
            int symbol = scoreAndNumber[0];
            int numberOfSymbol = scoreAndNumber[1];
            if (scoreAndNumber.Length > 2 && symbol == 2)
            {
                for (int i = 2; i < scoreAndNumber.Length; i++)
                {
                    int position = scoreAndNumber[i];
                    array[position - 1] = symbol;
                }
            }
            else
            {
                int execute = 0;        //число которое будет исключаться из списка после присвоения
                for (int i = 0; i < numberOfSymbol; i++)
                {
                    int test = 0;
                    while (true)
                    {
                        test = sortingRandom(vip, array.Length, execute);    //случайное число переданное из функции ниже
                        if (array[test] == 0)
                        {
                            array[test] = symbol;
                            break;
                        }
                        else execute = test;
                    }
                }
            }
            return array;
        }
        /// <summary>
        /// случайная генерация чисел с учетом не повторения уже выданных чмсел
        /// </summary>
        /// <param name="range">диапазон максимально генерируемого числа</param>
        /// <param name="executable">массив использованных чисел</param>
        /// <returns></returns>
        public static int sortingRandom(int rangeMin, int rangeMax, params int[] executable)
        {
            Random r = new Random();
            int[] validValues = Enumerable.Range(0, rangeMax).Except(executable).ToArray();
            return validValues[r.Next(rangeMin, validValues.Length)];
        }
        /// <summary>
        ///метод Math.Round округляет значения от 0.6 (3.6 = 4, но 3.5 = 3), при этом если среднее значение равно "4" или "2",
        ///или "3", то образуются спорные моменты типа "2 2 4" - среднее равно "3". С точки зрения преподавателя
        ///это не оценка "3" это "2". Чтоб исключить такие моменты идет проверка разницы значения получаемого Math.Round
        ///и числа при обычном вычеслении среднего значения, и ЕСЛИ оно <= 0.3 то такие комбинации цифр исключаются.
        ///Для "5" разница выше 0.6 не выходит, а значит комбинаций результатов небудет кроме "5 5 5", соотвественно
        ///поверять на <=0.3 не надо. Если дана оценка "5", делаем проверку на равенство знаения Math.Round "5" и сразу записываем кобинации,
        ///остальные оценки проверяются через <= 0.3
        /// </summary>
        /// <param name="score">передаваемая оценка(среденее), для генерации группы оценок</param>
        /// <returns>массив с генерируемыми оценками исходя из принятого параметра SCORE</returns>
        public static string[] generetanigPrakticalAnswers(int score, int column)
        {
            string[] answers = new string[500];
            double[] values = { 5, 4, 3, 2 };
            int next = 0;
            if ((score == 5) || (score == 4))           //
                for (int i = 0; i < values.Length - 1; i++)  //фильтр генерации оценок только из 5 4 3. Length -1 уберает
                                                             //2, чтоб не было оценок вида 5 5 5 2 5 итог 4
                {                                           //
                    for (int j = 0; j < values.Length - 1; j++)
                    {
                        for (int k = 0; k < values.Length - 1; k++)
                        {
                            switch (column) //коичество столбцов
                            {
                                case 3:

                                    if ((score == 5) && (Math.Round((values[i] + values[j] + values[k]) / 3) == score))
                                    {
                                        answers[next] = ($"{values[i]} {values[j]} {values[k]}").ToString();
                                        //Console.WriteLine(answers[next]);
                                        next++;
                                    }
                                    else
                                    {
                                        double average = (values[i] + values[j] + values[k]) / 3;
                                        double averageMath = Math.Round((values[i] + values[j] + values[k]) / 3);
                                        if (((averageMath - average) <= 0.3) && (averageMath == score))
                                        {
                                            answers[next] = ($"{values[i]} {values[j]} {values[k]}").ToString();
                                            //Console.WriteLine($"{next}\t{answers[next]}");
                                            next++;
                                        }
                                    }
                                    break;

                                case 5:
                                    for (int l = 0; l < values.Length - 1; l++)
                                    {
                                        for (int m = 0; m < values.Length - 1; m++)
                                        {
                                            double average = (values[i] + values[j] + values[k] + values[l] + values[m]) / 5;
                                            double averageMath = Math.Round((values[i] + values[j] + values[k] + values[l] + values[m]) / 5);
                                            if (((averageMath - average) <= 0.3) && (averageMath == score))
                                            {
                                                answers[next] = ($"{values[i]} {values[j]} {values[k]} {values[l]} {values[m]}").ToString();
                                                //Console.WriteLine($"{next}\t{answers[next]}");
                                                next++;
                                            }
                                        }
                                    }
                                    break;

                            }
                        }
                    }
                }
            else
            {
                for (int i = 1; i < values.Length; i++)
                {
                    for (int j = 1; j < values.Length; j++)
                    {
                        for (int k = 1; k < values.Length; k++)
                        {
                            switch (column)
                            {
                                case 3:
                                    double average = (values[i] + values[j] + values[k]) / 3;
                                    double averageMath = Math.Round((values[i] + values[j] + values[k]) / 3);
                                    if (((averageMath - average) <= 0.3) && (averageMath == score))
                                    {
                                        answers[next] = ($"{values[i]} {values[j]} {values[k]}").ToString();
                                        //Console.WriteLine($"{next}\t{answers[next]}\t{average}\tMath  {averageMath} ");
                                        next++;
                                    }
                                    break;

                                case 5:
                                    for (int l = 1; l < values.Length; l++)
                                    {
                                        for (int m = 1; m < values.Length; m++)
                                        {
                                            average = (values[i] + values[j] + values[k] + values[l] + values[m]) / 5;
                                            averageMath = Math.Round((values[i] + values[j] + values[k] + values[l] + values[m]) / 5);
                                            if (((averageMath - average) <= 0.3) && (averageMath == score))
                                            {
                                                answers[next] = ($"{values[i]} {values[j]} {values[k]} {values[l]} {values[m]}").ToString();
                                                next++;
                                            }
                                        }
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
            Array.Sort(answers);
            int zero = Array.LastIndexOf(answers, null);
            string[] answers2 = new string[answers.Length - (zero + 1)];
            Array.Copy(answers, zero + 1, answers2, 0, answers2.Length);
            return answers2;
        }
        public static void Main()
        {
            string[] fileScore = File.ReadAllLines("оценки.txt");
            char[] pattern = new char[] { ' ', '-', '(', ')', ',' };
            int[] five = fileScore[0].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            int[] four = fileScore[1].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            int[] three = fileScore[2].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            int[] two = fileScore[3].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            // vip - количество контрактников которым нельзя ставить "2"            
            int[] vip = fileScore[4].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            Random r = new Random();

            int random = 0;
            string[] fio = File.ReadAllLines("ФИО.txt");
            int[] temp = new int[fio.Length];
            int[] overallScore = fillingArray(five, fillingArray(four, fillingArray(three, fillingArray(two, temp, (vip[0])), (vip[0]))));
            int[] practicalScore = new int[fio.Length];
            Array.Copy(overallScore, practicalScore, fio.Length);

            string[] score5 = generetanigPrakticalAnswers(5, 3);
            string[] score4 = generetanigPrakticalAnswers(4, 3);
            string[] score3 = generetanigPrakticalAnswers(3, 3);
            string[] score2 = generetanigPrakticalAnswers(2, 3);

            string[] practicalAnswer = new string[fio.Length];
            for (int i = 0; i < practicalAnswer.Length; i++)
            {
                switch (practicalScore[i])
                {
                    case 5:
                        random = r.Next(0, score5.Length);
                        practicalAnswer[i] = score5[random];
                        break;
                    case 4:
                        random = r.Next(0, score4.Length);
                        practicalAnswer[i] = score4[random];
                        break;
                    case 3:
                        random = r.Next(0, score3.Length);
                        practicalAnswer[i] = score3[random];
                        break;
                    case 2:
                        random = r.Next(0, score2.Length);
                        practicalAnswer[i] = score2[random];
                        break;
                }
            }

            int[] theoryScore = new int[fio.Length];
            for (int j = 0; j < theoryScore.Length; j++)
            {
                switch (practicalScore[j])
                {
                    case 5:
                        random = r.Next(4, 6);
                        theoryScore[j] = random;
                        break;
                    case 4:
                        random = r.Next(4, 6);      // изменил 3 на 4 чтоб было поменьше оценок 54245
                        theoryScore[j] = random;
                        break;
                    case 3:
                        random = r.Next(3, 5);
                        theoryScore[j] = random;
                        break;
                    case 2:
                        random = r.Next(2, 4);
                        theoryScore[j] = random;
                        break;
                }
            }
            score5 = generetanigPrakticalAnswers(5, 5);
            score4 = generetanigPrakticalAnswers(4, 5);
            score3 = generetanigPrakticalAnswers(3, 5);
            score2 = generetanigPrakticalAnswers(2, 5);

            string[] theoryAnswer = new string[fio.Length];
            for (int i = 0; i < practicalAnswer.Length; i++)
            {
                switch (theoryScore[i])
                {
                    case 5:
                        random = r.Next(0, score5.Length);
                        theoryAnswer[i] = score5[random];
                        break;
                    case 4:
                        random = r.Next(0, score4.Length);
                        theoryAnswer[i] = score4[random];
                        break;
                    case 3:
                        random = r.Next(0, score3.Length);
                        theoryAnswer[i] = score3[random];
                        break;
                    case 2:
                        random = r.Next(0, score2.Length);
                        theoryAnswer[i] = score2[random];
                        break;
                }
            }

            //Console.SetWindowSize(120, 30);
            //Console.WriteLine("ФИО                   итог за теорию    оценки за практику    итог практика   итоговая");
            using (StreamWriter end = new StreamWriter("ведомость.txt"))
            {
                end.WriteLine("Звание\tФамилия\tТеоритическая подготовка\tОценка\tПрактическая подготовка\tОценка\tОбщая оценка");
                for (int i = 0; i < fio.Length; i++)
                {
                    end.WriteLine(($"{fio[i],10}\t{theoryAnswer[i],10}\t{theoryScore[i]}\t{practicalAnswer[i]}\t{practicalScore[i]}\t{overallScore[i]}"));
                }
            }

            Console.WriteLine("Программа завершена :-)) ");
            Thread.Sleep(1000);
        }
    }
}
