using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var MembershipType = _context.MembershipType.ToList();
            var Customer = new Customer();
            var CutomerViewModel = new CustomerFormViewModel
            {
                Customer=Customer,
                MembershipType=MembershipType

            };
            return View("CustomerForm", CutomerViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipType.ToList()

                };
                return View("CustomerForm", viewModel);
            }

           
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {

                var CustomerInDB = _context.Customers.Single(c => c.Id == customer.Id);

                CustomerInDB.Name = customer.Name;
                CustomerInDB.Birthdate = customer.Birthdate;
                CustomerInDB.MembershipTypeId = customer.MembershipTypeId;
                CustomerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        //[HttpPost]
        //public ActionResult Save(CustomerFormViewModel viewsModel)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        var viewModel = new CustomerFormViewModel
        //        {
        //            Customer=viewsModel.Customer,
        //            MembershipType=_context.MembershipType.ToList()

        //        };
        //        return View("CustomerForm",viewModel);
        //    }

        //    if(viewsModel.Customer.Id==0)
        //    {
        //        _context.Customers.Add(viewsModel.Customer);
        //    }
        //    else
        //    {

        //        var CustomerInDB = _context.Customers.Single(c => c.Id == viewsModel.Customer.Id);

        //        CustomerInDB.Name = viewsModel.Customer.Name;
        //        CustomerInDB.Birthdate = viewsModel.Customer.Birthdate;
        //        CustomerInDB.MembershipTypeId = viewsModel.Customer.MembershipTypeId;
        //        CustomerInDB.IsSubscribedToNewsletter = viewsModel.Customer.IsSubscribedToNewsletter;
        //    }

        //    _context.SaveChanges();

        //    return RedirectToAction("Index", "Customer");
        //}

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel {
                Customer = customer,
                MembershipType=_context.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        // GET: Customer
        public ActionResult Index()
        {
            //var customer = new Customer();
            //customer.Customers = Getcustomer();
            //return View(customer);

            var customer = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customer);
        }

        //private IEnumerable<Customer> Getcustomer()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer {Name="Hari",Id=1 },
        //        new Customer {Name="Anna",Id=2 }

        //    };

        //}

        [Route("customer/details/{id}")]
        public ActionResult Details(int? id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(row => row.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
            //if (customer != null && customer.Count() == 0)
            //{
            //    ViewBag.customerName = "We don't have any customers yet";

            //}
            //else
            //{
            //    ViewBag.customerName = customer.Name;
            //}

            //return View(ViewBag);

            //var customer = Getcustomer().SingleOrDefault(row => row.Id == id);
            //if (customer!=null && customer.Customers.Count() == 0)
            //{
            //    ViewBag.customerName = "We don't have any customers yet";

            //}
            //else
            //{
            //    ViewBag.customerName = customer.Name;
            //}

            //return View(ViewBag);

        }
    }
}