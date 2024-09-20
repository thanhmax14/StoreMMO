using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Car
{
    public class CarRepository
    {
        private readonly AppDbContext _db;

        public CarRepository(AppDbContext dbContext)
        {
            this._db = dbContext;
        }
    }
}
