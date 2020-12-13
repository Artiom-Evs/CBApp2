using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;

using CBApp2.Domain.Models;

//namespace CBApp2.Domain.Services
namespace CBApp2
{
    public class Parser
    {
        private string _filePage;
        public Parser(string path)
        {
            this._filePage = path;
        }

        public string GetPageText(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Файл не был загружен.");
            }
        }
        public Task<string> GetPageTextAsync(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return sr.ReadToEndAsync();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Файл не был загружен.");
            }
        }

        public List<CBApp2.Domain.Models.Element> ParsePage(bool isGroup)
        {
            //string PageText = GetPageTextAsync(_filePage).Result;
            string PageText = GetPageText(_filePage);
            List<CBApp2.Domain.Models.Element> groups = new List<CBApp2.Domain.Models.Element>();
            List<string> names = new List<string>();
            List<string> dates = new List<string>();
            List<Task<Week>> tables = new List<Task<Week>>();

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(req => req.Content(PageText)).Result;
            var doc = document.DocumentElement;

            foreach (var elem in doc.GetElementsByTagName("tbody"))
            {
                tables.Add(Task<Week>.Run(() => ParseTable(elem)));
            }
            foreach (var elem in doc.GetElementsByTagName("h2"))
            {
                names.Add(elem.Text());
            }
            foreach (var elem in doc.GetElementsByTagName("h3"))
            {
                dates.Add(elem.Text());

            }
            for (int i = 0; i < names.Count; i++)
            {
                CBApp2.Domain.Models.Element element = new CBApp2.Domain.Models.Element();
                Week week = tables[i].Result;

                element.Name = names[i];
                week.Element = element;
                week.Name = dates[i];
                week.CreateDate = DateTime.Now.ToFileTime();
                element.Week = week;
                element.IsGroup = isGroup;

                groups.Add(element);
            }

            return groups;
        }
        public async Task<List<CBApp2.Domain.Models.Element>> ParsePageAsync(bool isGroup)
        {
            string PageText = await GetPageTextAsync(_filePage);
            List<CBApp2.Domain.Models.Element> groups = new List<CBApp2.Domain.Models.Element>();
            List<string> names = new List<string>();
            List<string> dates = new List<string>();
            List<Task<Week>> tables = new List<Task<Week>>();

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(PageText));
            var doc = document.DocumentElement;

            foreach (var elem in doc.GetElementsByTagName("tbody"))
            {
                tables.Add(Task<Week>.Run(() => ParseTable(elem)));
            }
            foreach (var elem in doc.GetElementsByTagName("h2"))
            {
                names.Add(elem.Text());
            }
            foreach (var elem in doc.GetElementsByTagName("h3"))
            {
                dates.Add(elem.Text());

            }
            for (int i = 0; i < names.Count; i++)
            {
                CBApp2.Domain.Models.Element element = new CBApp2.Domain.Models.Element();
                Week week = tables[i].Result;

                element.Name = names[i];
                week.Element = element;
                week.Name = dates[i];
                week.CreateDate = DateTime.Now.ToFileTime();
                element.Week = week;
                element.IsGroup = isGroup;

                groups.Add(element);
            }

            //Console.WriteLine("Парсинг завершён!");
            return groups;
        }

        public Week ParseTable(IElement table)
        {
            Week week = new Week();
            Lesson lesson = new Lesson();
            byte serialIndex = 0;
            int dayIndex = 0;
            bool flag = true;

            try
            {
                foreach (var row in table.ChildNodes)
                {
                    //пропуск пустых контейнеров от удвоенных ячеек
                    if (row.GetElementCount() == 0) continue;
                    //заполнение заголовков, дней недели
                    else if (row.GetElementCount() == 7 || 
                        row.GetElementCount() == 6)
                    {
                        //пропуск пустой ячейки
                        if (row.Index() == 0) continue;
                        foreach (var elemOfRow in row.ChildNodes)
                        {
                            //пропуск пустых нечётных ячеек
                            if (elemOfRow.Index() % 2 == 0) continue;
                            //пропуск первой ячейки с №
                            if (elemOfRow.Index() == 1) continue;

                            //создание дней по заголовкам столбцов с названиями дней и датами
                            week.Days.Add(new Day()
                            {
                                Name = elemOfRow.Text().TrimEnd().TrimStart(),
                                Number = serialIndex++,
                                Week = week
                            });
                        }

                        //сбросить счётчик после нумерации дней
                        serialIndex = 0;
                        continue;
                    }
                    //пропустить заголовки столбцов: "предмет" и "ауд." 
                    else if (row.GetElementCount() == 12 || 
                        row.GetElementCount() == 10) continue;
                    else
                    {
                        //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

                        //переменная отсчитывающая столбцы
                        //сбрасывается в каждой новой строке с занятиямм
                        dayIndex = 0;
                        //переменная отсчитывает строки для нумерации занятий
                        serialIndex++;

                        foreach (var elemOfRow in row.ChildNodes)
                        {
                            if (elemOfRow.Index() == 1) continue;
                            else if (elemOfRow.Index() % 2 == 0) continue;

                            if (elemOfRow.Text() != "")
                            {
                                if (flag)
                                {
                                    //Console.WriteLine("Предмет: " + elemOfRow.Text().TrimEnd().TrimStart());

                                    lesson = new Lesson()
                                    {
                                        Subject = elemOfRow.Text().TrimEnd().TrimStart(),
                                        Number = serialIndex
                                    };
                                    flag = false;
                                }
                                else
                                {
                                    //Console.WriteLine("Кабинет: " + elemOfRow.Text().TrimEnd().TrimStart());

                                    lesson.Room = elemOfRow.Text().TrimEnd().TrimStart();
                                    week.Days[dayIndex++].Lessons.Add(lesson);
                                    flag = true;
                                }
                            }

                            //Console.Write(elemOfRow.Text());
                        }
                    }

                    //Console.WriteLine("Кол-во в row: " + row.GetElementCount() + ", индекс: " + row.Index());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Исключение: " + e.Message);
            }

            return week;
        }
    }
}