using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;                                                                                  //Стандартный набор модулей
using System.Text;                                                                                  //Ничего от себя
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Калькулятор                                                                               //Калькулятор от IGgnom'а, версия 0.4
{
    public partial class Form1 : Form
    {
        double FirstNum, SecondNum;                                                                 //Переменные вводимых чисел
        string Key = "";                                                                            //Переменная определяющая действие
        bool SecondEnter = false;                                                                   //Переменная проверки ввода

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)                                      //Процедура нажатия на кнопки "0"-"9" и ","
        {
            if (SecondEnter == true)                                                                //Проверка на ввод второго числа
            {
                textBox1.Text = "";                                                                 //Если истина, то поле освобождается
                SecondEnter = false;                                                                //И переменная ввода обнуляется
            }
            if (!(textBox1.Text.Contains(",")) && ((sender as Button).Text == ","))                 //Проверка сожержания запятой и ее нажатия
            {
                if (textBox1.Text == "")                                                            //Если поле пустое, то 
                {
                    textBox1.Text = textBox1.Text + "0,";                                           //Вводится "0,"
                }
                else                                                                                
                {
                    textBox1.Text = textBox1.Text + (sender as Button).Text;                        //В противном случае, присваивается Text нажатой кнопки
                }
            }
            else if ((sender as Button).Text != ",")                                                //Если запятая не нажата
            {
                textBox1.Text = textBox1.Text + (sender as Button).Text;                            //Просто присваивание Text нажатой кнопки
            }
        }

        private void button14_Click(object sender, EventArgs e)                                     //Процедура нажатия на кнопки действий
        {
            if ((textBox1.Text == "") && ((sender as Button).Text == "-"))                          //Проверка если поле пустое и нажимается "-"
            {
                textBox1.Text = "-";                                                                //Присваивается полю "-"
            }
            else if ((textBox1.Text != "") && (SecondEnter == true) && ((sender as Button).Text == "-"))    //Если поле не пустое и это второе число и нажимается "-"
            {
                textBox1.Text = "-";                                                                //Присваивается полю "-"
                SecondEnter = false;                                                                //И переменная ввода обнуляется
            }
            else if ((textBox1.Text != "") && (textBox1.Text.Contains("-") && (textBox1.Text.Length == 1))) //Если поле не пустое и содежит только минус
            {
                textBox1.Text = textBox1.Text;                                                      //Ничего не происходит
            }
            else if (textBox1.Text != "")                                                           //Если поле не пустое
            {
                if (textBox1.Text == "Ошибка")
                {
                    FirstNum = 0;
                }
                else
                {
                    FirstNum = Convert.ToDouble(textBox1.Text);                                     //Записывается первое число
                }
                Key = (sender as Button).Text;                                                      //Назначается действие
                SecondEnter = true;                                                                 //Определяется второе вхождение
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)                          //Процедура проверки нажатия клавиши в поле ввода
        {
            if (e.KeyChar.Equals((char)13))                                                         //Если нажат Ввод
            {
                button13_Click(sender, e);                                                          //Вызывается процедура Кнопки "="
            }
            else if ((e.KeyChar.Equals((char)67)) || (e.KeyChar.Equals((char)99)))                  //Если нажата любая версия клавиши "С"
            {
                button10_Click(sender, e);                                                          //Вызывается процедура Кнопки "AC/C"
                e.Handled = true;
            }
            else if ((e.KeyChar == '+') || (e.KeyChar == '-') || (e.KeyChar == '*') || (e.KeyChar == '/'))  //Если нажата клавиша действия
            {
                switch (e.KeyChar)                                                                  //Выбор действия
                {
                    case '+':                                                                       //Плюс 
                        if (textBox1.Text != "")                                                    //Проверка на не пустоту 
                        {
                            Key = "+";                                                              //Назначается действие
                            SecondEnter = true;                                                     //Определяется второк вхождение
                        }
                        break;                                                                      //Выход из выбора
                    case '-':                                                                       //Минус
                        if (textBox1.Text == "")                                                    //Проверка на не пустоту
                        {
                            return;                                                                 //Возвращение знака "-" и занесение его в поле
                        }
                        else if ((textBox1.Text != "") && (SecondEnter == true))                    //Если поле не пустое и это второе число
                        {
                            SecondEnter = false;                                                    //Обнуление переменной ввода
                            textBox1.Text = "";                                                     //Стирание предыдущего минуса и написание нового
                            return;                                                                 //Сделано для того, чтоб курсор не переносился влево
                        }
                        else if ((textBox1.Text != "") && (textBox1.Text.Contains("-") && (textBox1.Text.Length == 1))) //Если поле не пусто и содержит только минус
                        {
                            textBox1.Text = textBox1.Text;                                          //Ничего не происходит
                        }
                        else if (textBox1.Text != "")                                               //Если поле не пустое
                        {
                            FirstNum = Convert.ToDouble(textBox1.Text);                             //Записывается первое число
                            Key = "-";                                                              //Назначается действие - вычитание
                            SecondEnter = true;                                                     //Определяется второе вхождение
                        }
                        break;                                                                      //Выход из выбора
                    case '*':                                                                       //Умножение по аналогии со сложением
                        if (textBox1.Text != "")
                        {
                            Key = "*";
                            SecondEnter = true;
                        }
                        break;
                    case '/':                                                                       //И деление по аналогии со сложением и умножением
                        if (textBox1.Text != "")
                        {
                            Key = "/";
                            SecondEnter = true;
                        }
                        break;
                }
                e.Handled = true;                                                                   //Если ни одно из условий не сработало, то кнопка не нажимается
            }
            else if (SecondEnter == true)                                                           //Проверка на второе вхождение именно для чисел
            {
                FirstNum = Convert.ToDouble(textBox1.Text);                                         //Запись первого числа
                textBox1.Text = "";                                                                 //Очищение поля
                SecondEnter = false;                                                                //Обнуление переменной ввода
                if (Char.IsNumber(e.KeyChar) || (e.KeyChar == ',') || (e.KeyChar.Equals((char)08))) //Проверка на нажатие или чисел или запятой или удаления
                {
                    return;                                                                         //Если истина, то кнока нажата                                                        
                }
                else
                {
                    e.Handled = true;                                                               //В противном случае ничего не нажато
                }
            }
            else                                                                                    //То же самое, только для первого вхождения числа
            {
                if (Char.IsNumber(e.KeyChar) || (e.KeyChar == ',') || (e.KeyChar.Equals((char)08)))
                {
                    return;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)                                     //Процедура нажатия Кнопки "="
        {
            if ((textBox1.Text != "") && (SecondEnter == false))                                    //Если поле не пустое и это не второе вхождение
            {
                try                                                                                 //Пробует ввести второе число
                {
                    SecondNum = Convert.ToDouble(textBox1.Text);                                    //Сделано на случай пустого числа ввиде "-"
                }
                catch                                                                               //Если оно пустое, ошибка отлавливается
                {
                    textBox1.Text = textBox1.Text;                                                  //И решается тем, что ничего не происходит
                }
                if (Convert.ToString(SecondNum).Contains("-") && (Key != ""))                       //Проверка для красивой записи в строчку
                {
                    label2.Text = Convert.ToString(FirstNum) + " " + Key + " (" + Convert.ToString(SecondNum) + ") ="; //Если второе число с минусом, то ставятся скобки
                }
                else if (Key != "")                                                                 //Так же проверяется, чтоб действие было не пустым
                {
                    label2.Text = Convert.ToString(FirstNum) + " " + Key + " " + Convert.ToString(SecondNum) + " =";    //В противном случае записывается без скобок
                }
                switch (Key)                                                                        //Выбор действия
                {
                    case "+":                                                                       //Одинаково для всех знаков, кроме "/"
                        textBox1.Text = Convert.ToString(FirstNum + SecondNum);                     //Полю присваивается сумма чисел
                        break;                                                                      //Выход из выбора
                    case "-":
                        textBox1.Text = Convert.ToString(FirstNum - SecondNum);                     //Полю присваивается разность чисел
                        break;                                                                      //Выход из выбора
                    case "*":
                        textBox1.Text = Convert.ToString(FirstNum * SecondNum);                     //Полю присваивается произведение чисел
                        break;                                                                      //Выход из выбора
                    case "/":                                                                       //В случае с делением
                        if (SecondNum == 0F)                                                        //Второе число проверяется на "0"
                        {
                            textBox1.Text = "Ошибка";                                               //Если равно "0" выводится сообщение "Ошибка"
                        }
                        else                                                                        
                        {
                            textBox1.Text = Convert.ToString(FirstNum / SecondNum);                 //Если не равно "0", то полю присваивается частное чисел
                        }
                        break;                                                                      //Выход из выбора
                    case "":                                                                        //Если действие, по какой-то причине пустое
                        textBox1.Text = textBox1.Text;                                              //То ничего не происходит
                        break;                                                                      //И выход из выбора
                }
                Key = "";                                                                           //Обязательное обнуление действия
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)                               //Процедура реагирующая на изменения в поле ввода
        {
            button10.Text = "C";                                                                    //Кнопке "АС" присваивается значение Text "С"
        }

        private void button18_Click(object sender, EventArgs e)                           //Процедура нажатия кнопки "Info"
        {
            MessageBox.Show("Calculator version 0.4 by IGgnom", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information); //Выводится информация в диалоге сообщения
        }

        private void Form1_Load(object sender, EventArgs e)                                         //Процедура дизайна элементов при создании
        {
            BackColor = Color.FromArgb(13, 20, 28);                                                 //Изменения цвета формы                         
            textBox1.BackColor = Color.FromArgb(12, 16, 25);                                        //Изменение цвета поля ввода
            button1.BackColor = Color.FromArgb(13, 20, 28);                                         //Начало изменения цвета кнопок
            button2.BackColor = Color.FromArgb(13, 20, 28);
            button3.BackColor = Color.FromArgb(13, 20, 28);
            button4.BackColor = Color.FromArgb(13, 20, 28);
            button5.BackColor = Color.FromArgb(13, 20, 28);
            button6.BackColor = Color.FromArgb(13, 20, 28);
            button7.BackColor = Color.FromArgb(13, 20, 28);
            button8.BackColor = Color.FromArgb(13, 20, 28);
            button9.BackColor = Color.FromArgb(13, 20, 28);
            button10.BackColor = Color.FromArgb(13, 20, 28);
            button11.BackColor = Color.FromArgb(13, 20, 28);
            button12.BackColor = Color.FromArgb(13, 20, 28);
            button13.BackColor = Color.FromArgb(242, 35, 41);
            button14.BackColor = Color.FromArgb(13, 20, 28);
            button15.BackColor = Color.FromArgb(13, 20, 28);
            button16.BackColor = Color.FromArgb(13, 20, 28);
            button17.BackColor = Color.FromArgb(13, 20, 28);
            button18.BackColor = Color.FromArgb(13, 20, 28);
            button18.ForeColor = Color.FromArgb(242, 35, 41);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button7.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button9.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button10.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button11.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button12.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button13.FlatAppearance.MouseOverBackColor = Color.FromArgb(206, 28, 31);
            button14.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button15.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button16.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button17.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button18.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 33, 43);
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button7.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button9.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button10.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button11.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button12.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button13.FlatAppearance.MouseDownBackColor = Color.FromArgb(184, 31, 34);
            button14.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button15.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button16.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button17.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button18.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 44, 55);
            button18.FlatAppearance.BorderColor = Color.FromArgb(13, 20, 28);                       //Конец изменения цвета кнопок :)
        }

        private void button10_Click(object sender, EventArgs e)                                     //Процедура нажатия на кнопку очищения 
        {
            if (button10.Text == "C")                                                               //Если Text равен "С"
            {
                textBox1.Text = "";                                                                 //Очищается поле ввода
                SecondEnter = false;                                                                //Обнуляется второе вхождение
                button10.Text = "AC";                                                               //Text меняется на "АС"
            }
            else                                                                                    //В противном случае обнуляется все
            {
                FirstNum = 0F;
                SecondNum = 0F;
                SecondEnter = false;
                Key = "";
                label2.Text = "";
            }
        }
    }
}
