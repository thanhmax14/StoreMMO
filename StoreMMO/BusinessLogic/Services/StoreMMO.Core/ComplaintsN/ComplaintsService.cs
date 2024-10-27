using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.ComplaintsN;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.ComplaintsN
{
    public class ComplaintsService : IComplaintsService
    {
        private readonly IComplaintsRepository _complaintsRepository;
        public ComplaintsService(IComplaintsRepository complaintsRepository)
        {
            _complaintsRepository = complaintsRepository;
        }
        public IEnumerable<ComplaintsMapper> GetAll(string id)
        {
            // Gọi phương thức GetAll từ repository để lấy danh sách complaints
            var complaints = _complaintsRepository.GetAll(id);
            return complaints;
        }

        public IEnumerable<ComplaintsMapper> GetAllReportAdmin()
        {
            return _complaintsRepository.GetAllReportAdmin();
        }

        public UserMapper GetUserById(string id)
        {
            return _complaintsRepository.GetUserById(id);
        }

        public bool ReportAdmin(string id, string status)
        {
            return _complaintsRepository.ReportAdmin(id,status);
        }
		public async Task<bool> AddAsync(complantViewModels complaints)
		{
			return await this._complaintsRepository.AddAsync(complaints);
		}


		public async Task<bool> EditAsync(complantViewModels complaintsMapper)
		{
            return await this._complaintsRepository.EditAsync(complaintsMapper);
		}


		public async Task<bool> DeleteAsync(complantViewModels complaintsMapper)
		{
			return await this.DeleteAsync(complaintsMapper);
		}
		public async Task<complantViewModels> GetByIDAsync(string id)
		{
            return await this._complaintsRepository.GetByIDAsync(id);
		}

	}
}
