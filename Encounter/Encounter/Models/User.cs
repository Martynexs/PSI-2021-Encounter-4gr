namespace Encounter
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string Nickname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }

        public User()
        {

        }
        public User(string id,string name, string surname, string nickname, string gender, int age, string nationality)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Gender = gender;
            Age = age;
            Nationality = nationality;
        }
    }
}
