using INWKAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INWKAssignment.ViewModels;
using System.Configuration;

namespace INWKAssignment.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _context;

        public HomeController()
        {
            _context = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /// <summary>
        /// Return form to input data 
        /// </summary>
        public ActionResult Calculate()
        {
            try
            {
                var viewModel = new CalculateJobViewModel()
                {
                    Job = new Job(),
                    ItemTypes = _context.ItemTypes.ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }

        /// <summary>
        /// Process user entry data and show Results 
        /// </summary>
        [HttpPost]
        public ActionResult Result(CalculateJobViewModel viewModel)
        {
            try
            {
                // Fill Name property for ItemTypes (fix)
                FillItemTypesPropertiesByItemsList(viewModel.Job.Items);

                // Calculate Taxes for Items
                CalculateItemsTaxAmount(viewModel.Job.Items);

                // Add margin and extra margin (if necessary)
                CalculateJobMargins(viewModel.Job);

                // Set TotalRounded property with special rounding of the total
                viewModel.TotalRounded = TotalAmountRounding(viewModel.Job.Total);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }

        /// <summary>
        /// Calculate job margin and extra margin
        /// </summary>
        private void CalculateJobMargins(Job job)
        {
            decimal subTotal, subTotalWithoutTaxes, marginRate, extraMarginRate;

            // Get Rates from config
            marginRate = decimal.Parse(ConfigurationManager.AppSettings["JobMarginRate"].ToString());
            extraMarginRate = decimal.Parse(ConfigurationManager.AppSettings["JobExtraMarginRate"].ToString());

            subTotal = job.Items.Sum(i => i.ItemType.IsExempt ? i.Amount : i.Amount + i.TaxAmount);
            subTotalWithoutTaxes = job.Items.Sum(i => i.Amount);

            // Calculate margin 
            job.Total = job.Total + subTotalWithoutTaxes * marginRate;

            // Calculate extra margin - if necessary
            if (job.HasExtraMargin)
                job.Total = job.Total + subTotalWithoutTaxes * extraMarginRate;

            job.Total = job.Total + subTotal;
        }

        /// <summary>
        /// Calculate items TaxAmount
        /// </summary>
        private void CalculateItemsTaxAmount(List<Item> items)
        {
            // Get Rates from config
            decimal itemTaxRate = decimal.Parse(ConfigurationManager.AppSettings["ItemsTaxRate"].ToString());

            // Calculate TaxAmount - if necessary
            foreach (var item in items)
            {
                if (!item.ItemType.IsExempt)
                {
                    item.TaxAmount = item.Amount * itemTaxRate;
                }
            }
        }

        /// <summary>
        /// Fill all properties of an Item by it's Id
        /// </summary>
        private void FillItemTypesPropertiesByItemsList(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                var itemFromDb = _context.ItemTypes.Single(it => it.Id == item.ItemType.Id);
                item.ItemType.Name = itemFromDb.Name;
                item.ItemType.IsExempt = itemFromDb.IsExempt;
            }
        }

        /// <summary>
        /// Special rounding for the Total Amount (to the nearest even cent)
        /// </summary>
        private string TotalAmountRounding(decimal value)
        {
            int oneDigit;

            value = Math.Truncate(value * 100);

            int.TryParse(value.ToString().Substring(value.ToString().Length - 1, 1), out oneDigit);
            if (oneDigit % 2 == 0)
                return Math.Round(value / 100, 2).ToString("0.00");
            else
                return Math.Round((value + 1) / 100, 2).ToString("0.00");
        }

        /// <summary>
        /// Shows generic Error page
        /// </summary>
        /// <param name="message">Error message</param>
        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}