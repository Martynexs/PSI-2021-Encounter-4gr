using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Encounter
{
    public class Player
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string Nickname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Score { get; set; }
        public int Level { get; set; }
        public string Nationality { get; set; }

        public Player()
        {

        }
        public Player(int id,string name, string surname, string nickname, string gender, int age, double score, int level, string nationality)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Gender = gender;
            Age = age;
            Score = score;
            Level = level;
            Nationality = nationality;
        }
        public  string Output()
        {
            return ("ID:" + ID + "\n" +
                    "Name:" + Name + "\n" +
                    "Surname:" + Surname + "\n" +
                    "Nickname:" + Nickname + "\n" +
                    "Gender:" + Gender + "\n" +
                    "Age:" + Age + "\n" +
                    "Score:" + Score + "\n" +
                    "Level:" + Level + "\n" +
                    "Nationality:" + Nationality + "\n");
        }

    }
}
