namespace MyVaccinesWeb.My_Classes
{
    public class MyVaccineClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Maker { get; set; }

        public string Type { get; set;}

        public MyVaccineClass(int id, string name, string maker, string type)
        {
            Id = id;
            Name = name;
            Maker = maker;
            Type = type;
        }
    }
}
