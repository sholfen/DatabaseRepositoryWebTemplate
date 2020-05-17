using DBLib.Models;
using DBLib.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBLib.Repositories.Implements
{
    public class EmployeeRepository : BaseRepository<Employees>, IEmployeeRepository
    {
        public EmployeeRepository() : base(Tools.Tools.ConnectionString)
        {

        }
    }
}
