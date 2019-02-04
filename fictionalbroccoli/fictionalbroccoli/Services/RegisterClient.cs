using System;
using System.IO;
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
                var collection = db.GetCollection<Registration>("Pizza");
                return collection.FindById(id);
            }
        }
    }
}
