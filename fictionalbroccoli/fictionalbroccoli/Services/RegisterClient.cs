using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fictionalbroccoli.Models;
using LiteDB;

namespace fictionalbroccoli.Services
{
    public class RegisterClient
    {
        public string DbPath { get;}

        public RegisterClient()
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            DbPath = Path.Combine(docPath, "Broco.db");
        }

        public Registration Get(int id)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");
                return collection.FindById(id);
            }
        }

        public List<Registration> GetAll()
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");
                return collection.FindAll().ToList();
            }
        }

        public void Add(Registration registration)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");
                collection.Insert(registration);
            }
        }

        public void Delete(int id)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");
                collection.Delete(id);
            }
        }

        public void Update(Registration registration)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");
                collection.Update(registration);
            }
        }

        public List<Registration> Search(string text)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");

                var searchRegistrations = collection.Find(
                    registration => 
                        registration.Tag.ToLower().Contains(text)
                        || registration.Name.ToLower().Contains(text)
                    ).ToList();

                return searchRegistrations;
            }
        }

        public List<Registration> SearchTag(string tag)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var collection = db.GetCollection<Registration>("Registrations");

                var searchRegistrations = collection.Find(
                    registration =>
                        registration.Tag.ToLower().Contains(tag)
                    ).ToList();

                return searchRegistrations;
            }
        }
    }
}
