using System;
using Xamarin.Forms;

namespace fictionalbroccoli.Models
{
    public class Registration
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public string DateText { get; set; }

        public Registration()
        {

        }

        public Registration(string name, string description, string tag, DateTime date, string imagePath)
        {
            this.Name = name;
            this.Description = description;
            this.Tag = tag;
            this.Date = date;
            this.ImagePath = imagePath;
        }
    }
}
