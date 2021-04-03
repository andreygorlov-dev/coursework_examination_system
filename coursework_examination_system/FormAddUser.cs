﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coursework_examination_system
{
    public partial class FormAddUser : Form
    {
        public FormAddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("ФИО пользователя должно быть заполнено!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox2.Text.Trim().Length != 10)
                {
                    MessageBox.Show("Номер и серия паспорта должны быть введены корректно!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox3.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Не указан логин пользователя!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox4.Text.Trim().Length < 8)
                        {
                            MessageBox.Show("Пароль должен быть не меньше 8 символов!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}