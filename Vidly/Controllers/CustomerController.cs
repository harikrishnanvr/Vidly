using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer
        public ActionResult Index()
        {
            var customer = new Customer();

            customer.Customers = Getcustomer();
            return View(customer);
        }

        private IEnumerable<Customer> Getcustomer()
        {
            return new List<Customer>
            {
                new Customer {Name="Hari",Id=1 },
                new Customer {Name="Anna",Id=2 }

            };

        }

        [Route("customer/details/{id}")]
        public ActionResult Details(int? id)
        {
            
            var customer = Getcustomer().SingleOrDefault(row => row.Id == id);
            if (customer!=null && customer.Customers.Count() == 0)
            {
                ViewBag.customerName = "We don't have any customers yet";
              
            }
            else
            {
                ViewBag.customerName = customer.Name;
            }

            return View(ViewBag);

        }
    }
}