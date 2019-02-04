using System;
namespace fictionalbroccoli.Models
{
    public class Registration
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }


        public Registration()
        {

        }

        public Registration(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
