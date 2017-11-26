﻿using System;
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
            var CutomerViewModel = new CustomerFormViewModel
            {
                MembershipType=MembershipType

            };
            return View("CustomerForm", CutomerViewModel);
        }

        [HttpPost]
        public ActionResult Create(CustomerFormViewModel viewsModel)
        {
            _context.Customers.Add(viewsModel.Customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel {
                Customer=customer,
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