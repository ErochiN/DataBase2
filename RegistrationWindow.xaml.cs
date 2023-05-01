using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        DataBase dataBase = new DataBase();
        public RegistrationWindow()
        {
            InitializeComponent();
            
        }
        //Обработка клика по Имени:
        public void ClickingPromptingName(object sender, EventArgs e)
        {

            //Подсказка убирается:
            if (TextBoxName.Text == "Имя")
            {
                TextBoxName.Text = "";
                TextBoxName.Foreground = Brushes.Black;
            }

            //Включить подсказу на других TextBox:
            EnableHint(TextBoxLogin, "Логин");
            EnableHintPassword(PasswordBoxVis, PasswordBoxHea, "Пароль");
        }

        //Обработка клика Логин:
        public void ClickingPromptingLogin(object sender, EventArgs e)
        {

            //Подсказка убирается:
            if (TextBoxLogin.Text == "Логин")
            {
                TextBoxLogin.Text = "";
                TextBoxLogin.Foreground = Brushes.Black;
            }

            //Включить подсказу на других TextBox:
            EnableHint(TextBoxName, "Имя");
            EnableHintPassword(PasswordBoxVis, PasswordBoxHea, "Пароль");
        }

        //Обработка клика Пароль:
        public void ClickingPromptingPassword(object sender, EventArgs e) 
        {

            //Скрываем поле TextBox:
            PasswordBoxHea.Visibility = Visibility.Hidden;

            //Включить подсказу на других TextBox:
            EnableHint(TextBoxName, "Имя");
            EnableHint(TextBoxLogin, "Логин");
        }

        //Проверяет есть ли текст в TextBox:
        public static void EnableHint(TextBox textBox, string text) 
        {
            //Включает подсказку если нет текста:
            if (textBox.Text == "") 
            {
                textBox.Text = text;
                textBox.Foreground = Brushes.Silver;
            }
        }

        //Проверят есть ли текст в PasswordBox:
        public static void EnableHintPassword(PasswordBox passwordBox, TextBox textBox, string text) 
        {

            //Передает текст с PasswordBox в TextBox:
            textBox.Text = passwordBox.Password;

            //Включает подсказку если нет текста:
            if (textBox.Text == "") 
            {
                textBox.Text = text;
                textBox.Foreground = Brushes.Silver;
                textBox.Visibility = Visibility.Visible;
            }
        }

        //Событие при нажатии на кнопку зарегестрироваться
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var nameVar = TextBoxName.Text;
            var loginVar = TextBoxLogin.Text;
            var passwordVar = PasswordBoxVis.Password.ToString();
            var dos = "1";

            string querystring = $"insert into SchoolTable(NAME, LOGIN, PASSWORD, DOSTUP) values('{nameVar}', '{loginVar}', '{passwordVar}', '{dos}')";

            SqlCommand sqlCommand = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            if (sqlCommand.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("All Ok");
            }
            else 
            {
                MessageBox.Show("Error");
            }

            dataBase.ClosedConnection();
        }

        private Boolean checkUser() 
        {
            var nameVar = TextBoxName.Text;
            var loginVar = TextBoxLogin.Text;
            var passwordVar = PasswordBoxVis.Password.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, NAME, LOGIN, PASSWORD, DOSTUP from SchoolTable where NAME = '{nameVar}' and LOGIN = '{loginVar}' and PASSWORD = '{passwordVar}' and DOSTUP = '1'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Кажется такой аккаунт уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
