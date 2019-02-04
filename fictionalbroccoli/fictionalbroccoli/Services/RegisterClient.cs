using System;
using System.IO;

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
    }
}
