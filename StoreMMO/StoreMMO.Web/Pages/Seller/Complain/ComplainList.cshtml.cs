using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;

namespace StoreMMO.Web.Pages.Seller.Complain
{
    public class ComplainListModel : PageModel
    {
        private readonly IComplaintsService _complaintsServices;

        public ComplainListModel(IComplaintsService complaintsServices)
        {
            _complaintsServices = complaintsServices;
        }
        public IEnumerable<ComplaintsMapper> listcomplaints = new List<ComplaintsMapper>();
        public void OnGet()
        {
            listcomplaints = this._complaintsServices.GetAll();
            var temp = listcomplaints;
/*            listcomplaints = new List<ComplaintsMapper>();*/

        }
    }
}
