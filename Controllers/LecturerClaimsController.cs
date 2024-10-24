using LecturerClaimsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LecturerClaimsApp.Controllers
{
    public class LecturerClaimsController : Controller
    {
        // In-memory list to store claims for demonstration purposes.
        // Replace this with your database context if you are using a database.
        private static List<LecturerClaim> _claims = new List<LecturerClaim>();

        // GET: /LecturerClaims/Index
        // This method displays the list of claims for managing
        public IActionResult Index()
        {
            // Show all claims (approved, pending, rejected)
            return View(_claims); // This will render the "Index.cshtml" view with the list of claims
        }

        // GET: /LecturerClaims/Create
        // This method returns the Create view for submitting a new claim
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Render the "Create.cshtml" form view
        }

        // POST: /LecturerClaims/Create
        // This method handles the form submission for creating a new claim
        [HttpPost]
        public IActionResult Create(LecturerClaim newClaim)
        {
            if (ModelState.IsValid)
            {
                // Add the new claim to the list with a unique ClaimId
                newClaim.ClaimId = _claims.Count + 1;
                _claims.Add(newClaim);

                // Redirect to the Manage Claims (Index) page after saving the claim
                return RedirectToAction("Index");
            }

            // If validation fails, re-render the Create form with the current data
            return View(newClaim);
        }

        // POST: /LecturerClaims/Approve/5
        // This method allows approving a claim
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Approved";
            }
            return RedirectToAction("Index");
        }

        // POST: /LecturerClaims/Reject/5
        // This method allows rejecting a claim
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("Index");
        }
    }
}
