using System;
namespace fictionalbroccoli.Models
{
    public class Registration
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime Date { get; set; }


        public Registration()
        {

        }

        public Registration(string name, string description, string tag, DateTime date)
        {
            this.Name = name;
            this.Description = description;
            this.Tag = tag;
            this.Date = date;
        }
    }
}
