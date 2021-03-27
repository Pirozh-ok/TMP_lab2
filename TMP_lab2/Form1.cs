using System;
using System.Drawing;
using System.Windows.Forms;
using Tmp_lab2;

namespace TMP_lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyMenu menu = new MyMenu(@"C:\Users\ivan-\Desktop\Проекты\C#\TMP_lab2\date.txt");
            menu.ReadFile(); //считываем строки из файла

            /* создаём массив, содержащий все пункты меню и инициализируем их названиями из файла*/
            ToolStripMenuItem[] arrayPuncts = new ToolStripMenuItem[menu.ArrayDates.Length];

            try
            {
                for (int i = 0; menu.ArrayDates[i] != null; i++)
                {
                    ToolStripMenuItem ob = new ToolStripMenuItem(menu.ArrayDates[i].NameUzel);
                    arrayPuncts[i] = ob;

                    arrayPuncts[i].Name = menu.ArrayDates[i].NameMethod; // имени кнопки присваиваем имя вызываемого метода

                    if (menu.ArrayDates[i].Status == 1)
                        arrayPuncts[i].Enabled = false;

                    if (menu.ArrayDates[i].Status == 2)
                        arrayPuncts[i].Visible = false;
                }
                /*-------------------------------------------*/

                int numberParent = 0;
                menuStrip.Items.Add(arrayPuncts[0]);

                /* идём по каждой записе в файле и проверяем их уровень и расставляем соответственно ему*/
                for (int i = 1; menu.ArrayDates[i] != null; i++)
                {
                    DateString Data = menu.ArrayDates[i];
                    DateString lastData = menu.ArrayDates[i - 1];

                    // если уровень текущего элемента больше уровня прошлого на 1, то записываем его вниз
                    if (Data.Level - lastData.Level == 1)
                    {
                        numberParent = i - 1; //запоминаем номер родителя
                        menu.ArrayDates[numberParent].IsParent = true; // устанавливаем флаг, что у него есть дети и метод не вызывается
                        arrayPuncts[numberParent].DropDownItems.Add(arrayPuncts[i]); // добавляем этот элемент вниз
                    }
                    else
                    {
                        // если уровень текущего элемента = 0,  его как новую вкладку, родителем становится текущий элемент
                        if (Data.Level == 0)
                        {
                            numberParent = i;
                            menuStrip.Items.Add(arrayPuncts[i]);
                        }
                        else
                        {
                            // если уровни совпадают, то записываем их в одном родителе и он не изменяется
                            if (Data.Level == lastData.Level)
                                arrayPuncts[numberParent].DropDownItems.Add(arrayPuncts[i]);
                            else throw new Exception("Ошибка в данных файла");
                        }
                    }
                }
                /*----------------------------------------------*/

                /* если у кнопки нет детей, то добавляем на неё обработчик события*/
                for (int i = 0; arrayPuncts[i]!= null; i++)
                    if(!menu.ArrayDates[i].IsParent)
                        arrayPuncts[i].Click += UserOnClick;
            }
            catch
            {
                MessageBox.Show("Ошибка в данных файла. Измените данные на корректные и повторите попытку");
            }
        }

        private void UserOnClick(object sender, EventArgs eventArgs)
        {
            ToolStripMenuItem ob = (ToolStripMenuItem)sender;

            string methodName = ob.Name;

            // если имя не является пустой строкой, то с помощью рефлексии вызываем нужный метод
            if (methodName != "")
            {
                var method = typeof(Form1).GetMethod(methodName);
                if (method != null)
                    method.Invoke(this, null);
                else MessageBox.Show("Метода с таким именем не существует");
            }
        }

        public void Others()
        {
            MessageBox.Show("Работает Others");
        }

        public void Stuff()
        {
            this.BackColor = Color.Red;
        }

        public void Orders()
        {
            System.Drawing.Size size = new System.Drawing.Size(500, 500);
            this.Size = size; 
        }

        public void Docs()
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(100,100);
            textBox.Text = "Вызван метод";
            this.Controls.Add(textBox);
        }

        public void Departs()
        {
            Button button = new Button();
            button.Text = "Кнопка";
            button.Location = new Point(200, 50);
            this.Controls.Add(button);
        }

        public void Towns()
        {
            this.BackColor = Color.SteelBlue;
        }

        public void Posts()
        {
            MessageBox.Show("Работает метод Posts");
        }

        public void Window()
        {
            Form1 form = new Form1();
            form.Show();
        } 

        public void Help()
        {
            Environment.Exit(0);
        }

    }
}
