using AllTrails.Data;
using AllTrails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AllTrails.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly AllTrailsContext _context;
        private readonly string _userId;

        // constructor
        public UserProfilesController(AllTrailsContext context, IHttpContextAccessor httpContextAccessor) { 
            _context = context;
            _userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        // GET: UserProfiles/Edit
        public async Task<IActionResult> Edit()
        {
            // Get user profile
            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(m => m.IdentityId == _userId);

            // If not found, initialize a new one
            if (userProfile == null)
            {
                userProfile = new Models.UserProfile();
                userProfile.IdentityId = _userId; // set the IdentityId for the new profile
            }

            return View(userProfile);
        }

        // POST: UserProfiles/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdentityId,Name")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                if (UserProfileExists(userProfile.IdentityId))
                {
                    // Update existing profile
                    _context.Update(userProfile);
                }
                else
                {
                    // Add a new profile
                    _context.Add(userProfile);
                }

                await _context.SaveChangesAsync();

                ViewData["Message"] = "Display profile updated!";
            }

            return View(userProfile);
        }


        private bool UserProfileExists(string identityId)
        {
            return _context.UserProfile.Any(e => e.IdentityId == identityId);
        }
    }
}
