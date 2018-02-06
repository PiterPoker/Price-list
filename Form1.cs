using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Test_Price.DataB;

namespace Test_Price
{
    public partial class Form1 : Form
    {
        PriceContext db = new PriceContext();

        Category category = new Category();
        Brend brend = new Brend();
        Product product = new Product();

        public Form1()
        {
            InitializeComponent();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Items.Clear();
                comboBox2.Items.Clear();
                var val = comboBox1.SelectedItem.ToString();
                category = db.Categorys.Where(br=>br.Name == val).Include(p => p.Brends).FirstOrDefault();
                foreach (var comB in category.Brends)
                {
                    comboBox2.Items.Add(comB.Name.ToString());
                }
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Проблема соединения с базой данных. \nПрограмма будет работать в режиме Offline.",
                             "Сообщение", MessageBoxButtons.OK);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            var cBox = db.Categorys.ToList();
            foreach (var comB in cBox)
            {
                comboBox1.Items.Add(comB.Name);
                comboBox1.ValueMember = comB.Id.ToString();
            }            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Items.Clear();
                var val = comboBox2.SelectedItem.ToString();
                brend = db.Brends.Where(br => br.Name == val).Where(nm=>nm.CategoryId == category.Id).Include(p => p.Products).FirstOrDefault();
                foreach (var comB in brend.Products)
                {
                    comboBox3.Items.Add(comB.Model.ToString());
                }
                comboBox3.Text = "";
            }
            catch (Exception)
            {

                MessageBox.Show("Проблема соединения с базой данных. \n Программа будет работать в режиме Offline. ",
                             "Сообщение", MessageBoxButtons.OK);
            }
            
        }

        int categ = 0, breCat = 0, prod = 0;
        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                DialogResult dr = MessageBox.Show("Хотите записать модель в базу данных?",
                             "Сообщение", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        UpDateDataBase();
                        UpdateDate();
                        break;
                    case DialogResult.No: break;
                }
            }
            catch (Exception) { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Enabled = false;
                textBox3.Enabled = true;
                comboBox2.Enabled = false;
                textBox4.Enabled = true;
                comboBox3.Enabled = false;
                textBox5.Enabled = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
                checkBox3.Checked = true;
                checkBox3.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
                textBox3.Enabled = false;
                comboBox2.Enabled = true;
                textBox4.Enabled = false;
                comboBox3.Enabled = true;
                textBox5.Enabled = false;
                checkBox2.Checked = false;
                checkBox2.Enabled = true;
                checkBox3.Checked = false;
                checkBox3.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comboBox2.Enabled = false;
                textBox4.Enabled = true;
                comboBox3.Enabled = false;
                textBox5.Enabled = true;
                checkBox3.Checked = true;
                checkBox3.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
                textBox4.Enabled = false;
                comboBox3.Enabled = true;
                textBox5.Enabled = false;
                checkBox3.Checked = false;
                checkBox3.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox3.Enabled = false;
                textBox5.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = true;
                textBox5.Enabled = false;
            }
        }

        private void UpdateDate()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dataGridView1.Rows.Clear();
            var cBox = db.Categorys.ToList();
            foreach (var comB in cBox)
            {
                comboBox1.Items.Add(comB.Name.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(ind);
            }
            catch (Exception) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string chBox1, chBox2, chBox3;
            //827*1027
            if (checkBox1.Checked == true) { chBox1 = textBox3.Text; } else { chBox1 = comboBox1.Text; }
            if (checkBox2.Checked == true) { chBox2 = textBox4.Text; } else { chBox2 = comboBox2.Text; }
            if (checkBox3.Checked == true) { chBox3 = textBox5.Text; } else { chBox3 = comboBox3.Text; }           
            int hight = 1027;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Color colorFont = Color.FromArgb(255, 0, 0, 255);
            Pen blackPen = new Pen(colorFont, 3);
            Font myFont = new Font("Calibri", 20, System.Drawing.FontStyle.Bold);
            Font price = new Font("Calibri", 46, System.Drawing.FontStyle.Bold);
            Brush myBrush = new SolidBrush(colorFont);
            int yLogo = 30;
            int xLogo = 25;
            System.Drawing.Image img =
                          System.Drawing.Image.FromFile("logo.jpg");
            e.Graphics.DrawImage(img, xLogo, yLogo, 165, 60);
            e.Graphics.DrawRectangle(blackPen, 25, 30, 765, 120);
            e.Graphics.DrawString(chBox1, myFont, myBrush, new Rectangle(25, 30, 765, 40), stringFormat);
            e.Graphics.DrawString(chBox2 + " "+ chBox3, myFont, myBrush, new Rectangle(25, 70, 765, 40), stringFormat);
            e.Graphics.DrawString("Производитель - "+textBox1.Text, myFont, myBrush, new Rectangle(25, 110, 765, 40), stringFormat);
            e.Graphics.DrawLine(blackPen, 790, 120, 790, 180);
            e.Graphics.DrawLine(blackPen, 25, 180, 25, 120);
            e.Graphics.DrawString("Технические характеристики", myFont, myBrush, new Rectangle(25, 135, 765, 60), stringFormat);
            e.Graphics.DrawLine(blackPen, 25, 180, 790, 180);
            int x1 = 25, y1 = 140;
            hight = hight - y1 - 40 - 150;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int size = 40;
                if (hight > y1)
                {
                    
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Count() > 18)
                        size += 60;
                    e.Graphics.DrawRectangle(blackPen, x1, y1 += 40, 765, size);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), myFont, myBrush, new Rectangle(25, y1, 400, size), stringFormat);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), myFont, myBrush, new Rectangle(400, y1, 365, size), stringFormat);
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Count() > 18)
                        y1 += 60;
                }
                else
                {
                    break;
                }
            }
            if (checkBox5.Checked == true)
            {   
                EconomOper.InstallmentPlan(double.Parse(textBox2.Text.Replace('.', ',').Trim()), int.Parse(textBox6.Text), int.Parse(textBox7.Text));
                string startStr = String.Format("{0:0.00}", Bundle.Price);
                var monthInstall = String.Format("{0:0.00}", Bundle.LoanInterest);
                e.Graphics.DrawRectangle(blackPen, 25, y1 += 40, 765, 40);
                e.Graphics.DrawString(checkBox5.Text, myFont, myBrush, new Rectangle(25, y1, 765, 40), stringFormat);
                e.Graphics.DrawRectangle(blackPen, 25, y1 += 40, 765, 120);
                e.Graphics.DrawString("Первоначальный взнос ("+ textBox6.Text + "%): ", myFont, myBrush, new Rectangle(25, y1, 500, 40), stringFormat);
                e.Graphics.DrawString(startStr + " руб.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
                e.Graphics.DrawString("Количество выплат: ", myFont, myBrush, new Rectangle(25, y1 += 40, 500, 40), stringFormat);
                e.Graphics.DrawString(textBox7.Text+" мес.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
                e.Graphics.DrawString("Выплата в месяц: ", myFont, myBrush, new Rectangle(25, y1 += 40, 500, 40), stringFormat);
                e.Graphics.DrawString(monthInstall + " руб.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
            }

            if (checkBox6.Checked == true)
            {
                EconomOper.Credit(double.Parse(textBox2.Text.Replace('.', ',').Trim()), int.Parse(textBox8.Text), int.Parse(textBox9.Text), int.Parse(textBox10.Text));
                string startStr = String.Format("{0:0.00}", Bundle.StartCredit);
                var monthInstall = String.Format("{0:0.00}", Bundle.PriceMonth);
                e.Graphics.DrawRectangle(blackPen, 25, y1 += 40, 765, 40);
                e.Graphics.DrawString(checkBox6.Text, myFont, myBrush, new Rectangle(25, y1, 765, 40), stringFormat);
                e.Graphics.DrawRectangle(blackPen, 25, y1 += 40, 765, 120);
                e.Graphics.DrawString("Первоначальный взнос (" + textBox8.Text + "%): ", myFont, myBrush, new Rectangle(25, y1, 500, 40), stringFormat);
                e.Graphics.DrawString(startStr + " руб.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
                e.Graphics.DrawString("Количество выплат: ", myFont, myBrush, new Rectangle(25, y1 += 40, 500, 40), stringFormat);
                e.Graphics.DrawString(textBox9.Text + " мес.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
                e.Graphics.DrawString("Выплата в месяц ("+ textBox10.Text + "% в год): ", myFont, myBrush, new Rectangle(25, y1 += 40, 500, 40), stringFormat);
                e.Graphics.DrawString(monthInstall + " руб.", myFont, myBrush, new Rectangle(500, y1, 265, 40), stringFormat);
            }
            e.Graphics.DrawRectangle(blackPen, 25, y1+=40, 765, 150);
            e.Graphics.DrawString("Цена: "+ textBox2.Text+" руб.", price, myBrush, new Rectangle(25, y1, 765, 150), stringFormat);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                groupBox1.Enabled = true;
                textBox6.Text = "0";
                textBox7.Text = "0";
            }
            else
            {
                groupBox1.Enabled = false;
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                groupBox2.Enabled = true;
                textBox8.Text = "0";
                textBox9.Text = "0";
                textBox10.Text = "0";
            }
            else
            {
                groupBox2.Enabled = false;
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
            }
        }

        private void удалитьМодельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                db.Options.RemoveRange(product.Options);
                db.Products.Remove(product);
                db.SaveChanges();
                UpdateDate();
            }
            catch (Exception)
            {}
            
        }

        private void удалитьПроизводителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var br in brend.Products)
                {
                    db.Options.RemoveRange(br.Options);
                }
                db.Products.RemoveRange(brend.Products);
                db.Brends.Remove(brend);
                db.SaveChanges();
                UpdateDate();
            }
            catch (Exception)
            {}
            

        }

        private void удалитьКатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
            DialogResult dr = MessageBox.Show("Для удаление категории, нужно удалить произвадителей и его модели. \n Вы удалили производителей этой категории и модели этих производителей?",
                         "Сообщение", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    db.Categorys.Remove(category);
                    UpdateDate();
                    break;
                case DialogResult.No: break;
            }
            }
            catch (Exception) { }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null ||
                    dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    dataGridView1.Rows.RemoveAt(i); i--;
                }                    
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox6.Text = checkForAnInteger(textBox6.Text).ToString();
        }

        public int checkForAnInteger(string tBCheck)
        {
            int intTbCheck = 0;
            string pattern = @"(\d{2})|(\d{1})";
            if (Regex.IsMatch(tBCheck, pattern, RegexOptions.IgnoreCase))
            {
                try
                {
                    var result = Convert.ToDouble(tBCheck.Replace('.', ',').Trim());
                    if (result > 0 && result < 100) { intTbCheck = Convert.ToInt32(result); }
                }
                catch (Exception)
                {

                    MessageBox.Show("Для определения этого поля введите целое число больше нуля.", "Ошибка!");
                }
                
            }
            else
            {
                MessageBox.Show("Для определения этого поля введите целое число больше нуля.", "Ошибка!");                
            }
            return intTbCheck;
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            textBox7.Text = checkForAnInteger(textBox7.Text).ToString();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = checkForAnInteger(textBox8.Text).ToString();
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            textBox9.Text = checkForAnInteger(textBox9.Text).ToString();
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            textBox10.Text = checkForAnInteger(textBox10.Text).ToString();
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            textBox8.Text = checkForAnInteger(textBox8.Text).ToString();
        }

        public decimal convertPrice(string price)
        {
            decimal covertPrice = 0;

            string pattern = @"\d";
            if (Regex.IsMatch(price, pattern, RegexOptions.IgnoreCase))
            {
                try
                {
                    covertPrice = decimal.Parse(price.Replace('.', ',').Trim());
                }
                catch (Exception)
                {
                    MessageBox.Show("Для определения этого поля введите число. \n Пример: 12,03 или 16", "Ошибка!");
                }
            }
            else
            {
                MessageBox.Show("Для определения этого поля введите число. \n Пример: 12,03 или 16", "Ошибка!");
            }

            return covertPrice;
        }

        private void checkBox4_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {            
            textBox2.Text = String.Format("{0:0.00}", convertPrice(textBox2.Text));
        }

        private void UpDateDataBase()
        {
            try { 
            decimal price;
            string pattern = @"[0-9]{10}\,\d{2}";
            if (Regex.IsMatch(textBox2.Text, pattern, RegexOptions.IgnoreCase))
            {
                price = decimal.Parse(textBox2.Text);
            }
            else
            {
                price = decimal.Parse(textBox2.Text.Replace('.', ',').Trim());
            }
            if (checkBox1.Checked == true)
            {
                if (db.Categorys.Where(cat => cat.Name == textBox3.Text).Count() == 0)
                {
                    Category category = new Category { Name = textBox3.Text };
                    db.Categorys.Add(category);
                    db.SaveChanges();
                }
                categ = db.Categorys.Where(cat => cat.Name == textBox3.Text).FirstOrDefault().Id;
            }
            else
            {
                categ = category.Id;
            }

            if (checkBox2.Checked == true)
            {
                if (db.Brends.Where(bre => bre.Name == textBox4.Text)
                    .Where(ctg => ctg.CategoryId == categ).Count() == 0)
                {
                    Brend brend = new Brend { Name = textBox4.Text, CategoryId = categ };
                    db.Brends.Add(brend);
                    db.SaveChanges();
                }

                breCat = db.Brends.Where(br => br.Name == textBox4.Text)
                    .Where(ctg => ctg.CategoryId == categ).FirstOrDefault().Id;

            }
            else
            {
                breCat = brend.Id;
            }

            if (checkBox3.Checked == true)
            {
                if (db.Products.Where(cat => cat.Model == textBox5.Text)
                    .Where(ctg => ctg.BrendId == breCat).Count() == 0)
                {                    
                    Product product = new Product
                    {
                        Model = textBox5.Text,
                        BrendId = breCat,
                        Price = price,
                        Country = textBox1.Text
                    };
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                prod = db.Products.Where(pr => pr.Model == textBox5.Text)
                    .Where(ctg => ctg.BrendId == breCat).FirstOrDefault().Id;
            }
            else
            {
                prod = product.Id;
            }

            if (dataGridView1.Rows.Count > 1)
            {
                if(checkBox1.Checked == true || 
                    checkBox2.Checked == true || 
                    checkBox3.Checked == true)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        Option option = new Option
                        {
                            Name = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                            Value = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            ProductId = prod
                        };
                        db.Options.Add(option);
                        db.SaveChanges();
                    }
                }
                if (checkBox1.Checked == false &&
                    checkBox2.Checked == false &&
                    checkBox3.Checked == false)
                {
                    var option = db.Options.Where(op => op.ProductId == prod).ToList();
                    db.Options.RemoveRange(option);
                    db.SaveChanges();
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        Option optionCreate = new Option
                        {
                            Name = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                            Value = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            ProductId = prod
                        };
                        db.Options.Add(optionCreate);
                        db.SaveChanges();
                    }
                    
                    product.Price = price;
                    product.Country = textBox1.Text;
                    db.SaveChanges();
                }
            }
            }
            catch(Exception e) { MessageBox.Show("Ошибка в заполнение полей!\n" + e,"Сообщение об ошибке!"); }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                var lbModel = comboBox3.SelectedItem.ToString();
                product = db.Products.Where(br => br.Model == lbModel).Include(p=>p.Options).FirstOrDefault();
                textBox1.Text = product.Country;
                textBox2.Text = product.Price.ToString();
                foreach (var options in product.Options)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = options.Name;
                    dataGridView1.Rows[i].Cells[1].Value = options.Value;
                    i++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблема соединения с базой данных. \n Программа будет работать в режиме Offline. ",
                                             "Сообщение", MessageBoxButtons.OK);
            }
            
        }
    }
}
