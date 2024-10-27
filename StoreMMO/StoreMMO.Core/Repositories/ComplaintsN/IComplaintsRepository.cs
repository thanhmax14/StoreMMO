using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.ViewModels;
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
        Task<bool> AddAsync(complantViewModels complaintsMapper);
		Task<bool> EditAsync(complantViewModels complaintsMapper);
		Task<bool> DeleteAsync(complantViewModels complaintsMapper);
		Task<complantViewModels> GetByIDAsync(string complaintsMapper);
	}
}
