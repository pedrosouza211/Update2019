using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel : Controller
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}
