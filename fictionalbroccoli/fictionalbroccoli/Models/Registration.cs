﻿using System;
namespace fictionalbroccoli.Models
{
    public class Registration
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public string Description { get; set; }


        public Registration()
        {

        }

        public Registration(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }
    }
}
