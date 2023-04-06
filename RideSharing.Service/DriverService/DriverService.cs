﻿using RideSharing.Entity;
using RideSharing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class DriverService : BaseService<Driver>, IDriverService
    {
        public DriverService(IBaseRepository<Driver> baseRepository) : base(baseRepository)
        {
        }
    }
}
