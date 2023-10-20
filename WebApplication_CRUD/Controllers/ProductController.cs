using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_CRUD.DataAccesLayer;
using WebApplication_CRUD.Models;

namespace WebApplication_CRUD.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL Product_DAL = new Product_DAL();

        // GET: Product
        public ActionResult Index()
        {
            var productList = Product_DAL.GetAllProducts();

            if (productList.Count() == 0)
            {
                TempData["InfoMessage"] = "Currently there are no products available in DataBase.";
            }

            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                // TODO: Add insert logic here
                bool isInserted = false;
                if(ModelState.IsValid)
                {
                    isInserted = Product_DAL.InsertProduct(product);

                    if (isInserted == true) 
                    {
                        TempData["SuccessMessage"] = "Product inserted succesfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to insert the Product/Already Available...!";
                    }
                    
                }
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Product_DAL.GetProductsByID(id).FirstOrDefault();

            if(product == null)
            {
                TempData["InfoMessage"] = "products not available with id " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    bool IsUpdated = Product_DAL.UpdateProduct(product);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product Updated succesfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the Product/Already Available...!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = Product_DAL.GetProductsByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "products not available with id " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = Product_DAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
