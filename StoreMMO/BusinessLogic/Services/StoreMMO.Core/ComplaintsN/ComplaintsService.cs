using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Repositories.ComplaintsN;
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
        public IEnumerable<ComplaintsMapper> GetAll()
        {
            // Gọi phương thức GetAll từ repository để lấy danh sách complaints
            var complaints = _complaintsRepository.GetAll();
            return complaints;
        }
    }
}
