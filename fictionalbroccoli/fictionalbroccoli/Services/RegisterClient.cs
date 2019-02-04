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
    }
}
