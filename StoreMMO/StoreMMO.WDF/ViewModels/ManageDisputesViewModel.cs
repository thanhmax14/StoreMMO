using BusinessLogic.Services.StoreMMO.Core.Disputes;
using StoreMMO.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreMMO.WDF.ViewModels
{
    public class ManageDisputesViewModel : BaseViewModel
    {
        private readonly IDisputeService _disputeService;
        public ObservableCollection<DisputeViewModels> ViewModels { get; set; }
        public ICommand Update { get; }
        public ICommand Reject1 { get; }



        public ManageDisputesViewModel(IDisputeService disputeService)
        {
            _disputeService = disputeService;
            ViewModels = new ObservableCollection<DisputeViewModels>();
            LoadData();

            // Avoid directly calling an async method in the constructor.
            // LoadData(); // Remove this line.
        }
     
        public async Task LoadData()
        {
            var items = await _disputeService.getAllAsync();
            ViewModels = new ObservableCollection<DisputeViewModels>(items);
            OnPropertyChanged(nameof(ViewModels));
        }
   

        private DisputeViewModels _SelectItem;

        public DisputeViewModels SelectItem
        {
           get
            {
                return _SelectItem;
            }
            set
            {
                _SelectItem = value;    
                OnPropertyChanged(nameof(SelectItem));
                if(_SelectItem != null)
                {
                    IdInfo = _SelectItem.ID;
                    OrderDetailIDInfo = _SelectItem.OrderDetailID;
                    DescriptionInfo = _SelectItem.Description;
                    CreateDateInfo = _SelectItem.CreateDate;
                    RelyInfo = _SelectItem.Reply;
                    StatusInfo = _SelectItem.Status;
                }
            }
        }
        private string _OrderDetailIDInfo;

        public string OrderDetailIDInfo
        {
            get
            {
                return _OrderDetailIDInfo;
            }
            set
            {
                _DescriptionInfo = value;
                OnPropertyChanged(nameof(OrderDetailIDInfo));
            }
        }
        private string _IdInfo;
        public string IdInfo
        {
            get
            {
                return _IdInfo;
            }
            set
            {
                _DescriptionInfo = value;
                OnPropertyChanged(nameof(IdInfo));
            }
        }

        private string _DescriptionInfo;
        public string DescriptionInfo
        {
            get
            {
                return _DescriptionInfo;
            }
            set
            {
                _DescriptionInfo = value;
                OnPropertyChanged(nameof(DescriptionInfo));
            }
        }
        private string? _RelyInfo;
        public string? RelyInfo
        {
            get { 
                return _RelyInfo;
            }
            set
            {
                _RelyInfo = value;
                OnPropertyChanged(nameof(RelyInfo));    
            }
        }
        private DateTime _CreateDateInfo;
        public DateTime CreateDateInfo { 
        get
            {
                return _CreateDateInfo;
            }
            set
            {
                _CreateDateInfo = value;
                OnPropertyChanged(nameof(CreateDateInfo));
            }
        }
        private string _StatusInfo;
        public string StatusInfo
        {
            get
            {
                return _StatusInfo;
            }
            set
            {
                _StatusInfo = value;
                OnPropertyChanged(nameof(StatusInfo));  
            }
        }


    }
}