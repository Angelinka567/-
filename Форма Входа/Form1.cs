using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Форма_Входа
{
    public partial class Form1 : Form
    {
        int try1 = 0;//количество попыток входа в систему
        public Form1()
        {
            InitializeComponent();
            if(Properties.Settings.Default.login.Length!=0 && Properties.Settings.Default.password.Length != 0)
            {
                nameLogin.Text = Properties.Settings.Default.login;
                namePassword.Text = Properties.Settings.Default.password;
                checkBox1.Checked = true;
            }
            if (Properties.Settings.Default.ThisTry > 0)
            {
                //блокировка всех кнопок, если время ожидание вледующей попытки еще не истекло
                timer1.Enabled = true;
                checkBox1.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
                nameLogin.Enabled = false;
                namePassword.Enabled = false;
            }
            else
            {
                //активация всех кнопок, если время ожидания следующей попытки истелко
                checkBox1.Enabled = true;
                timer1.Enabled = false;
                button1.Enabled = true;
                button3.Enabled = true;
                nameLogin.Enabled = true;
                namePassword.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (try1 == 3 | Properties.Settings.Default.ThisTry > 0)//проверка кол-ва попыток входа
            {
                Properties.Settings.Default.ThisTry = 60;
                //переменная, которая сохраняется в системе, даже после ее перезагрузки
                Properties.Settings.Default.Save();
                //сохраняем ее значения

                timer1.Enabled = true;

                button1.Enabled = false;
                //если попыток больше 3 раз, то система блакирует кнопки и текстовые поля
                checkBox1.Enabled = false;
                button3.Enabled = false;
                nameLogin.Enabled = false;
                namePassword.Enabled = false;
            }


            string login = nameLogin.Text.Trim();
            string password = namePassword.Text.Trim();
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.login = login;
                Properties.Settings.Default.password = password;
                Properties.Settings.Default.Save();
            }
            if (login=="" | password=="")
            {
                MessageBox.Show("Некоторые поля не заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if((login.Equals("login1") && password.Equals("pas1")) | (login.Equals("login2") && password.Equals("pas2")) | (login.Equals("login3") && password.Equals("pas3")))
                {
                    MessageBox.Show("Добро пожаловать в несуществующую систему!", "Несуществующая система приветсвует вас", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Form2 n = new Form2();
                    n.Show();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try1++;
                }
                nameLogin.Text = "";
                namePassword.Text = "";
            }
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //таймер, отображающий отсчёт времени
            
            button1.Text = (Properties.Settings.Default.ThisTry--) + "сек..";
            Properties.Settings.Default.Save();

            if (Properties.Settings.Default.ThisTry == -2)//если время истекло, то активирует все кнопки
            {
                timer1.Enabled = false;
                try1 = 1;
                button1.Text = "Авторизация";
                checkBox1.Enabled = true;
                button1.Enabled = true;
                button3.Enabled = true;
                nameLogin.Enabled = true;
                namePassword.Enabled = true;
            }

        }

        private void messageQueue1_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}