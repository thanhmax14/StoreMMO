using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ComplaintsN
{
    public interface IComplaintsRepository
    {
        IEnumerable<ComplaintsMapper> GetAll(string id);
        public bool ReportAdmin(string id,string status);
        IEnumerable<ComplaintsMapper> GetAllReportAdmin();
        public UserMapper GetUserById(string id);

    }
}
