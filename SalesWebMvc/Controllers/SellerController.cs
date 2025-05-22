using System.Diagnostics;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellerController(SellerService sellerService, DepartmentService departmentservice)
        {
            _sellerService = sellerService;
            _departmentService = departmentservice;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Department = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            seller.DepartmentId = 1; // ✅ Departamento padrão (Id 1 deve existir no banco)
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not Found"});
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" }); ;
            }
            return View(obj);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int  id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" }); ;
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            }
            return View(obj);
        }
        public IActionResult Edit (int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });

            }
            var obj = _sellerService.FindById(Id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Department = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Seller seller)
        {
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
           
        }
        public IActionResult Error (string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };
            return  View(viewModel);
        }
    }
}
