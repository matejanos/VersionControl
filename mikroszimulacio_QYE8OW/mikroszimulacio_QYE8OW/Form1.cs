﻿using mikroszimulacio_QYE8OW.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mikroszimulacio_QYE8OW
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        List<int> f = new List<int>();
        List<int> n = new List<int>();

        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Windows\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Windows\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Windows\Temp\halál.csv");

            //for (int year = 2005; year <= 2024; year++)
            //{

            //    for (int i = 0; i < Population.Count; i++)
            //    {

            //    }

            //    int nbrOfMales = (from x in Population
            //                      where x.Gender == Gender.Male && x.IsAlive
            //                      select x).Count();
            //    int nbrOfFemales = (from x in Population
            //                        where x.Gender == Gender.Female && x.IsAlive
            //                        select x).Count();
            //    Console.WriteLine(
            //        string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));


            //}

        }

        //public void SimStep(int year, Person person)
        //{
        //    if (!person.IsAlive) return;
        //    byte age = (byte)(year - person.BirthYear);
        //    double pDeath = (from x in DeathProbabilities
        //                     where x.Gender == person.Gender && x.Age == age
        //                     select x.P).FirstOrDefault();

        //    if (rng.NextDouble() <= pDeath)
        //        person.IsAlive = false;

        //    if (person.IsAlive && person.Gender == Gender.Female)
        //    {
        //        double pBirth = (from x in BirthProbabilities
        //                         where x.Age == age
        //                         select x.P).FirstOrDefault();
        //        if (rng.NextDouble() <= pBirth)
        //        {
        //            Person újszülött = new Person();
        //            újszülött.BirthYear = year;
        //            újszülött.NbrOfChildren = 0;
        //            újszülött.Gender = (Gender)(rng.Next(1, 3));
        //            Population.Add(újszülött);
        //        }
        //    }


        //}

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        P = double.Parse(line[2]),
                        NbrOfChildren = int.Parse(line[1])

                    });
                }
            }

            return birthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return deathProbabilities;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void Simulation()

        {
            richTextBox1.Clear();

            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    Person person = new Person();
                    if (!person.IsAlive) return;
                    byte age = (byte)(year - person.BirthYear);
                    double pDeath = (from x in DeathProbabilities
                                     where x.Gender == person.Gender && x.Age == age
                                     select x.P).FirstOrDefault();

                    if (rng.NextDouble() <= pDeath)
                        person.IsAlive = false;

                    if (person.IsAlive && person.Gender == Gender.Female)
                    {
                        double pBirth = (from x in BirthProbabilities
                                         where x.Age == age
                                         select x.P).FirstOrDefault();
                        if (rng.NextDouble() <= pBirth)
                        {
                            Person újszülött = new Person();
                            újszülött.BirthYear = year;
                            újszülött.NbrOfChildren = 0;
                            újszülött.Gender = (Gender)(rng.Next(1, 3));
                            Population.Add(újszülött);
                        }
                    }
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                n.Add(nbrOfFemales);
                f.Add(nbrOfMales);
            }
            DisplayResults();
        }
        private void DisplayResults()
        {
            for (int i = 2005; i <= numericUpDown1.Value; i++)
            {

                richTextBox1.Text = "Év:" + i + "\n" + "\t" + "Nő:" + f[i - 2005] + "\n" + "\t" + "Férfi:" + f[i - 2005];


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog()== DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }
    }
}
