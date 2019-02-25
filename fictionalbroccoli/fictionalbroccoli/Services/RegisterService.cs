using System;
using System.Collections.Generic;
using fictionalbroccoli.Models;
using Xamarin.Forms;

namespace fictionalbroccoli.Services
{
    public class RegisterService : IRegisterService 
    {
        private RegisterClient _registerClient;

        public RegisterService()
        {
            _registerClient = new RegisterClient();
            //this.Populate(); 
        }

        public Registration Get(int id)
        {
            return _registerClient.Get(id);
        }

        public List<Registration> GetAll()
        {
            return _registerClient.GetAll();
        }

        public void Add(Registration registration)
        {
            _registerClient.Add(registration);
        }

        public void Delete(int id)
        {
            _registerClient.Delete(id);
        }

        public void Update(Registration registration)
        {
            _registerClient.Update(registration);
        }

        /*
         * Populate the database     
         */       
        public void Populate()
        {
            Registration registration = new Registration(
                "Factorio", 
                "Factorio is a game about building and creating automated factories to produce items of increasing complexity, within an infinite 2D world. Use your imagination to design your factory, combine simple elements into ingenious structures, and finally protect it from the creatures who don't really like you.",
                "#drink",
                new DateTime(2008, 5, 1),
                "factorio"
            );
            Registration registration2 = new Registration(
                "Aye sir", 
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", 
                "#food",
                 DateTime.Now,
                 "factorio"
                );
            Registration registration3 = new Registration(
                "Albert3",
                "The best three", 
                "#videogame", 
                DateTime.Now,
                "factorio"
                );
            this.Add(registration);
            this.Add(registration2);
            this.Add(registration3);
        }
    }
}
