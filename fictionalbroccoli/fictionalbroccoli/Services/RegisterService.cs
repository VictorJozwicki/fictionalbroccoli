using System;
using System.Collections.Generic;
using fictionalbroccoli.Models;

namespace fictionalbroccoli.Services
{
    public class RegisterService : IRegisterService 
    {
        private RegisterClient _registerClient;

        public RegisterService()
        {
            _registerClient = new RegisterClient();
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
            throw new NotImplementedException();
        }

        public void Update(Registration registration)
        {
            throw new NotImplementedException();
        }
    }
}
