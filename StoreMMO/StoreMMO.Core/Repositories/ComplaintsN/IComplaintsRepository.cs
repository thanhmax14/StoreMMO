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
        bool Warrant(string idcomplaint, string ordercode);
        ComplaintsMapper GetAllReportAdminbyid(string idcomplaint);
        bool BackMoney(string id);
        bool checkStockProductType(string idcomplaint);
        ComplaintsMapper GetAllNonebyid(string idcomplaint);
        bool WarrantSeller(string idcomplaint, string ordercode, string sellerid);
        bool BackMoneySeller(string idcomplant, string sellerid);
        bool BackMoneyforseller(string id);
    }
}
