using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.ComplaintsN
{
    public interface IComplaintsService
    {
        IEnumerable<ComplaintsMapper> GetAll();
        public bool ReportAdmin(string id);
        IEnumerable<ComplaintsMapper> GetAllReportAdmin();

    }
}
