using System.ComponentModel.DataAnnotations;

namespace StoreMMO.Web.Models.ViewModels.Admin
{
    public class CategoryUpdate
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
       public string Name { get; set; }


        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
