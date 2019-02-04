﻿using System;
using System.Collections.Generic;
using fictionalbroccoli.Models;

namespace fictionalbroccoli.Services
{
    public interface IRegisterService
    {
        Registration Get(int id);
        List<Registration> GetAll();
        void Delete(int id);
        void Add(Registration registration);
        void Update(Registration registration);
    }
}
