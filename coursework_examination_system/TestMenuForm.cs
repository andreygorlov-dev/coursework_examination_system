﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace coursework_examination_system
{
    //ФОрма с тестами
    public partial class TestMenuForm : Form
    {
        //Содержит информацию о предметах
        private List<SubjectClass> subjects;
        //Содержит инфу о тестах
        private List<TestClass> tests;

        public TestMenuForm(bool editing)
        {
            InitializeComponent();
            if (editing)
            {
                this.Controls.Remove(button1);
                Button updateButton = new Button();
                updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
                updateButton.Location = new System.Drawing.Point(12, 389);
                updateButton.Name = "button1";
                updateButton.Size = new System.Drawing.Size(196, 31);
                updateButton.TabIndex = 1;
                updateButton.Text = "Изменить тест";
                updateButton.UseVisualStyleBackColor = true;
                updateButton.Click += new System.EventHandler(this.updateButton_Click);

                Button deleteButton = new Button();
                deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))));
                deleteButton.Location = new System.Drawing.Point(211, 389);
                deleteButton.Name = "button1";
                deleteButton.Size = new System.Drawing.Size(196, 31);
                deleteButton.TabIndex = 1;
                deleteButton.Text = "Удалить тест";
                deleteButton.UseVisualStyleBackColor = true;
                deleteButton.Click += new System.EventHandler(this.deleteButton_Click);

                this.Controls.Add(updateButton);
                this.Controls.Add(deleteButton);
            }

            Form1.refresEventClass.events += new RefreshClass.EventButtonClickHandler(button2_Click);
            try
            {
                String response = SendRequestClass.PostRequestAsync("getSubjects", "").Result;
                subjects = JsonConvert.DeserializeObject<List<SubjectClass>>(response);
                if (subjects.Count > 0)
                {
                    comboBox1.DataSource = subjects;
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("В системе нет ни одного предмета, необходимо добавить!", "Нет предметов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show("Проверьте соединение с интернетом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            String response = SendRequestClass.PostRequestAsync("deleteTest", "{ \"id\" : " + ((TestClass)listBox1.SelectedItem).Id + " }").Result;
            if (response.Equals("\terror"))
            {
                MessageBox.Show("Ошибка при удалении теста, обратитесь к админу", "Ошибка при удалении теста", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            else
            {
                MessageBox.Show("Тест успешно удалён!", "Успешное удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1.refresEventClass.StartRefreshEvent(sender, e);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            ConstructorTestForm constructorTestForm = new ConstructorTestForm(((TestClass)listBox1.SelectedItem).Id);
            constructorTestForm.MdiParent = Form1.ActiveForm;
            constructorTestForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SubjectClass subjectSelect = (SubjectClass)comboBox1.SelectedItem;
                String response = SendRequestClass.PostRequestAsync("getTests", "{ \"id\" : " + subjectSelect.Id + "} ").Result;
                tests = JsonConvert.DeserializeObject<List<TestClass>>(response);
                fillListBox();
                if (tests.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show("Проверьте соединение с интернетом!", "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillListBox()
        {
            listBox1.Items.Clear();
            tests.ForEach(delegate (TestClass test)
            {
                listBox1.Items.Add(test);
            });
        }

        private void TestMenuForm_Resize(object sender, EventArgs e)
        {
            if (this.Width < 436)
            {
                this.Width = 436;
            }
            if (this.Height < 465)
            {
                this.Height = 465;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tests.Sort((str1, str2) => { return string.Compare(str1.Name, str2.Name); });
            fillListBox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tests.Sort((str1, str2) => { return string.Compare(str2.Name, str1.Name); });
            fillListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestingForm testingForm = new TestingForm(((TestClass)listBox1.SelectedItem).Id);
            testingForm.MdiParent = this.MdiParent;
            testingForm.Show();
        }
    }
}
