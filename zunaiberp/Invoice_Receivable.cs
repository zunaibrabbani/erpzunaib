﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace zunaiberp
{
    public partial class Invoice_Receivable : Form
    {
        Form3 f3 = new Form3();
        public Invoice_Receivable()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Invoice_Receivable_Load(object sender, EventArgs e)
        {
            {
                this.BackColor = Color.White;
                this.dataGridView1.BackgroundColor = Color.Teal;
                int c = 0;
                f3.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select count(InvoiceID) from IR ", f3.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = Convert.ToInt32(dr[0]);
                    c++;

                }
                {
                    this.textBox1.Text = "IR-0" + c.ToString() + "-" + System.DateTime.Today.Year;

                }
                f3.oleDbConnection1.Close();
            }

            {
                f3.oleDbConnection1.Open();
                OleDbCommand cmd = new OleDbCommand("select DCID from DC where status='Open'", f3.oleDbConnection1);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["DCID"].ToString());
                }

                f3.oleDbConnection1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            f3.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("select * from DC where DCID=@DCID", f3.oleDbConnection1);
            cmd.Parameters.AddWithValue("@DCID", this.comboBox1.Text);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox4.Text = dr["SOID"].ToString();
                this.textBox3.Text = dr["Cname"].ToString();
                this.textBox5.Text = dr["CID"].ToString();
                this.textBox7.Text = dr["TotalAmount"].ToString();
            }
            f3.oleDbConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                {
                    f3.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into IR(CID,Cname,AmountReceivable,DCID,invoiceid,icdate)values(@CID,@Cname,@AmountReceivable,@DCID,@invoiceid,@icdate)", f3.oleDbConnection1);
                    cmd.Parameters.AddWithValue("@CID", this.textBox5.Text);
                    cmd.Parameters.AddWithValue("@Cname", this.textBox3.Text);
                    cmd.Parameters.AddWithValue("@AmountReceivable", this.textBox7.Text);
                    cmd.Parameters.AddWithValue("@DCID", this.comboBox1.Text);
                    cmd.Parameters.AddWithValue("invoiceid", textBox1.Text);
                    cmd.Parameters.AddWithValue("@icdate", this.dateTimePicker1);
                    cmd.ExecuteNonQuery();
                    f3.oleDbConnection1.Close();
                    MessageBox.Show("Data Inserted in Table");
                }
                {
                    f3.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("select * from IR", f3.oleDbConnection1);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    f3.oleDbConnection1.Close();

                }
                {
                    f3.oleDbConnection1.Open();
                    OleDbCommand cmd = new OleDbCommand("Update DC set Status='Close' where DCID='" + comboBox1.Text + "'", f3.oleDbConnection1);
                    cmd.ExecuteNonQuery();
                    f3.oleDbConnection1.Close();
                
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int dis;
            int amt;
            int totalamt;
            dis = Convert.ToInt32(textBox6.Text);
            amt = Convert.ToInt32(textBox2.Text);
            totalamt = (amt * dis) / 100;
            this.textBox7.Text = Convert.ToString(totalamt);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
