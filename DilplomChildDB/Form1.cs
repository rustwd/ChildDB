using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;
using System.Collections.Generic;


namespace DilplomChildDB
{

    public partial class Form1 : Form
    {

        public string ConnectionString, CommandString, k_travma_s, TargetSport, profselect;
        
        public int age_temp, k1_sh, k2_sh, k3_sh, k4_sh; //коэффициенты для таблицы

        public int A, B, C, D, dudge_temp;

        public int vozrast_temp;
        public double heartbeat_temp, heartbeat_f_temp, period_s, nagruzka, K1, K2, K3, K4, K5, K6, K7;

        private void button3_Click(object sender, EventArgs e) //произведения ранжирования
        {
            //перевод чисел в переменные 
            A = Convert.ToInt32(textBox7.Text);
            B = Convert.ToInt32(textBox8.Text);
            C = Convert.ToInt32(textBox9.Text);
            D = Convert.ToInt32(textBox10.Text);

            label15.Text = "";

            List<Spisoc> a = new List<Spisoc>();

            a.Add(new Spisoc("Хореография", A));
            a.Add(new Spisoc("Работа с предметом", B));
            a.Add(new Spisoc("Отработка элементов", C));
            a.Add(new Spisoc("Танцевальный класс", D));

            a.Sort((v2, v1) => (int)v2.count - (int)v1.count);

            
            for (int i = 0; i < a.Count; i++)
            {
                label15.Text += "\n" + (a[i].name);
                
            }
        }

        


        public Form1()
        {
            InitializeComponent();

        }

        private void CheckHealthB_Click(object sender, EventArgs e) // определения состояния здоровья
        {
            vozrast_temp = Convert.ToInt32(textBox1.Text); // переменная возраста
            heartbeat_temp = Convert.ToDouble(textBox3.Text); // переменная биения сердца в нормальном режиме
            heartbeat_f_temp = Convert.ToDouble(textBox4.Text);


            //подключение к бд
            ConnectionString = @"Data Source = C:\Users\HomePC\Documents\Visual Studio 2015\Projects\DilplomChildDB\DilplomChildDB\bin\Debug\child_db.db; Version = 3 ";
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand("SELECT Id, Vozrast, Nagruzka FROM Age where [Vozrast] = "+ vozrast_temp, con);
                con.Open();

                //поиск возраста
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        label16.Text = reader["Nagruzka"].ToString();
                        age_temp = Convert.ToInt32(reader["Vozrast"]);

                    }
                }
                else
                {
                    label16.Text = "No rows found.";
                }

                
                ////////////// поиск переменной К1. Поиск идёт через сравнение возрастапеременной возраста
                if (age_temp >= 3 && age_temp <= 5)
                {
                    SQLiteCommand command_02 = new SQLiteCommand("SELECT Id, start, end, K_n_3_5 FROM Age_n_3_5 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                    command_02.Parameters.AddWithValue("@heartbeat", heartbeat_temp);

                    SQLiteDataReader reader_02 = command_02.ExecuteReader();
                    if (reader_02.HasRows)
                    {
                        while (reader_02.Read())
                        {
                            label17.Text = reader_02["K_n_3_5"].ToString();
                            K1 = Convert.ToDouble(label17.Text);
                        }
                    }
                    else
                    {
                        label17.Text = "No rows found.";
                    }
                }
                else
                {
                    if (age_temp >= 6 && age_temp <= 8)
                    {
                        SQLiteCommand command_03 = new SQLiteCommand("SELECT Id, start, end, K_n_6_8 FROM Age_n_6_8 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                        command_03.Parameters.AddWithValue("@heartbeat", heartbeat_temp);

                        SQLiteDataReader reader_03 = command_03.ExecuteReader();
                        if (reader_03.HasRows)
                        {
                            while (reader_03.Read())
                            {
                                label18.Text = reader_03["K_n_6_8"].ToString();
                                K1 = Convert.ToDouble(label18.Text);
                            }
                        }
                        else
                        {
                            label18.Text = "No rows found.";
                        }
                    }
                    else
                    {
                        if (age_temp >= 9 && age_temp <= 11)
                        {
                            SQLiteCommand command_04 = new SQLiteCommand("SELECT Id, start, end, K_n_9_11 FROM Age_n_9_11 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                            command_04.Parameters.AddWithValue("@heartbeat", heartbeat_temp);

                            SQLiteDataReader reader_04 = command_04.ExecuteReader();
                            if (reader_04.HasRows)
                            {
                                while (reader_04.Read())
                                {
                                    label19.Text = reader_04["K_n_9_11"].ToString();
                                    K1 = Convert.ToDouble(label19.Text);
                                }
                            }
                            else
                            {
                                label19.Text = "No rows found.";
                            }
                        }
                        else
                        {
                            if (age_temp >= 12)
                            {
                                SQLiteCommand command_05 = new SQLiteCommand("SELECT Id, start, end, K_n_12_18 FROM Age_n_12_18 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                                command_05.Parameters.AddWithValue("@heartbeat", heartbeat_temp);

                                SQLiteDataReader reader_05 = command_05.ExecuteReader();
                                if (reader_05.HasRows)
                                {
                                    while (reader_05.Read())
                                    {
                                        label20.Text = reader_05["K_n_12_18"].ToString();
                                        K1 = Convert.ToDouble(label20.Text);
                                    }
                                }
                                else
                                {
                                    label20.Text = "No rows found.";
                                }
                            }
                            else
                            {
                                if (age_temp < 3)
                                {
                                    MessageBox.Show("Возраст не должен быть меньше 3 лет!");
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                ///////////////////////////////////поиск переменной К2


                if (age_temp >= 3 && age_temp <= 5)
                {
                    SQLiteCommand command_06 = new SQLiteCommand("SELECT Id, start, end, K_t_3_5 FROM Age_t_3_5 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                    command_06.Parameters.AddWithValue("@heartbeat", heartbeat_f_temp);

                    SQLiteDataReader reader_06 = command_06.ExecuteReader();
                    if (reader_06.HasRows)
                    {
                        while (reader_06.Read())
                        {
                            label21.Text = reader_06["K_t_3_5"].ToString();
                            K2 = Convert.ToDouble(label21.Text);
                        }
                    }
                    else
                    {
                        label21.Text = "No rows found.";
                    }
                }
                else
                {
                    if (age_temp >= 6 && age_temp <= 8)
                    {
                        SQLiteCommand command_07 = new SQLiteCommand("SELECT Id, start, end, K_t_6_8 FROM Age_t_6_8 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                        command_07.Parameters.AddWithValue("@heartbeat", heartbeat_f_temp);

                        SQLiteDataReader reader_07 = command_07.ExecuteReader();
                        if (reader_07.HasRows)
                        {
                            while (reader_07.Read())
                            {
                                label22.Text = reader_07["K_t_6_8"].ToString();
                                K2 = Convert.ToDouble(label22.Text);
                            }
                        }
                        else
                        {
                            label22.Text = "No rows found.";
                        }
                    }
                    else
                    {
                        if (age_temp >= 9 && age_temp <= 11)
                        {
                            SQLiteCommand command_08 = new SQLiteCommand("SELECT Id, start, end, K_t_9_11 FROM Age_t_9_11 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                            command_08.Parameters.AddWithValue("@heartbeat", heartbeat_f_temp);

                            SQLiteDataReader reader_08 = command_08.ExecuteReader();
                            if (reader_08.HasRows)
                            {
                                while (reader_08.Read())
                                {
                                    label23.Text = reader_08["K_t_9_11"].ToString();
                                    K2 = Convert.ToDouble(label23.Text);
                                }
                            }
                            else
                            {
                                label23.Text = "No rows found.";
                            }
                        }
                        else
                        {
                            if (age_temp >= 12)
                            {
                                SQLiteCommand command_09 = new SQLiteCommand("SELECT Id, start, end, K_t_12_18 FROM Age_t_12_18 where (start <= @heartbeat) and (end >= @heartbeat)", con);
                                command_09.Parameters.AddWithValue("@heartbeat", heartbeat_f_temp);

                                SQLiteDataReader reader_09 = command_09.ExecuteReader();
                                if (reader_09.HasRows)
                                {
                                    while (reader_09.Read())
                                    {
                                        label24.Text = reader_09["K_t_12_18"].ToString();
                                        K2 = Convert.ToDouble(label24.Text);
                                    }
                                }
                                else
                                {
                                    label24.Text = "No rows found.";
                                }
                            }
                            else
                            {
                                if (age_temp < 3)
                                {
                                    MessageBox.Show("Возраст не должен быть меньше 3 лет!");
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                ///////////////////////////////////////////////////////


                ///////////////// выбор травмы
                k_travma_s = listBox1.SelectedItem.ToString();

                    SQLiteCommand command_10 = new SQLiteCommand("SELECT Id, Travma, K_travma FROM Injury where Travma = @k_travma_s", con);
                    command_10.Parameters.AddWithValue("@k_travma_s", k_travma_s);

                    SQLiteDataReader reader_10 = command_10.ExecuteReader();
                    if (reader_10.HasRows)
                    {
                        while (reader_10.Read())
                        {
                            label25.Text = reader_10["K_travma"].ToString();
                            K3 = Convert.ToDouble(label25.Text);
                    }
                    }
                    else
                    {
                        label25.Text = "No rows found.";
                    }
                    ///////////////////////

                K4 = ((K1 + K2 + K3) / 3); // переменная K4
                K4 = Math.Round(K4, 1); // округление до одной переменной после запятой
                label26.Text = K4.ToString();
                ///////////////////////////


                SQLiteCommand command_11 = new SQLiteCommand("SELECT Id, K4_start, K4_end, UserText FROM Health where (K4_start <= @k4) and (K4_end >= @k4)", con);
                command_11.Parameters.AddWithValue("@k4", K4);

                SQLiteDataReader reader_11 = command_11.ExecuteReader();
                if (reader_11.HasRows)
                {
                    while (reader_11.Read())
                    {
                        label8.Text = reader_11["UserText"].ToString();
                        
                    }
                }
                else
                {
                    label8.Text = "bug.";
                }
            }
        }


       


        private void button2_Click(object sender, EventArgs e) //расчёт недельной нагрузки
        {
            TargetSport = comboBox2.SelectedItem.ToString(); // переносим текстовое значение из комбобокса в переменную
            period_s = Convert.ToDouble(textBox2.Text);
            vozrast_temp = Convert.ToInt32(textBox1.Text);

            ConnectionString = @"Data Source = C:\Users\HomePC\Documents\Visual Studio 2015\Projects\DilplomChildDB\DilplomChildDB\bin\Debug\child_db.db; Version = 3 ";
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                con.Open(); //открываем бд

                ////////////// поиск переменной   K5
                SQLiteCommand command_12 = new SQLiteCommand("SELECT Id, TargetS, K_Target FROM TargetSport where TargetS = @TargetSport", con);
                command_12.Parameters.AddWithValue("@TargetSport", TargetSport);

                SQLiteDataReader reader_12 = command_12.ExecuteReader();
                if (reader_12.HasRows)
                {
                    while (reader_12.Read())
                    {
                        label27.Text = reader_12["K_Target"].ToString();
                        K5 = Convert.ToDouble(label27.Text);
                    }
                }
                else
                {
                    label27.Text = "No rows found.";
                }
                //////////////

                ////////////// поиск переменной   K5
                SQLiteCommand command_13 = new SQLiteCommand("SELECT Id, Period, K_Nagruzka FROM SportAge where Period = @period_s", con);
                command_13.Parameters.AddWithValue("@period_s", period_s);

                SQLiteDataReader reader_13 = command_13.ExecuteReader();
                if (reader_13.HasRows)
                {
                    while (reader_13.Read())
                    {
                        label28.Text = reader_13["K_Nagruzka"].ToString();
                        K6 = Convert.ToDouble(label28.Text);
                    }
                }
                else
                {
                    label28.Text = "No rows found.";
                }

                //////// поиск нагрузки 

                SQLiteCommand command_14 = new SQLiteCommand("SELECT Id, Vozrast, Nagruzka FROM Age where Vozrast = @vozrast_temp", con);
                command_14.Parameters.AddWithValue("@vozrast_temp", vozrast_temp);

                SQLiteDataReader reader_14 = command_14.ExecuteReader();
                if (reader_14.HasRows)
                {
                    while (reader_14.Read())
                    {
                        label29.Text = reader_14["Nagruzka"].ToString();
                        nagruzka = Convert.ToDouble(label29.Text);
                    }
                }
                else
                {
                    label29.Text = "No rows found.";
                }

            }


            ///////расчёт недельной нагрузки 
            K7 = nagruzka * ((K4*K5*K6) / 3);
            label9.Text = K7.ToString();
        }
        public class Spisoc //класс для списка ранжирования
        {
            public Spisoc(string name, Int32 count)
            {
                this.name = name;
                this.count = count;
            }
            public string name;
            public Int32 count;
        }
    }


}


