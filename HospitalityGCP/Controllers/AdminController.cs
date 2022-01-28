using HospitalityGCP.Data;
using HospitalityGCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalityGCP.Controllers
{
    public class AdminController : Controller
    {
        private readonly HospitalityGCPContext _context;
        private SystemUser _user;

        public AdminController(HospitalityGCPContext context)
        {
            _context = context;

            //Call 'GetCurrentUserEmail' method testing of AmendUser class to get the users email address from the HTTP headers if available
            string userEmail = SystemUser.GetCurrentUserEmail();

            //and then load the AmendUser using the result
            _user = _context.SystemUser.Where(x => x.UserEmail == userEmail).FirstOrDefault();

            //If users email not available in HTTP headers (because you're playing solution locally rather than published on app engine)
            //then theUser will be null. In that case, just load the default AmendUser which we're using for basic impersonation.    
            if (_user == null)
            {
                _user = new SystemUser();
                _user.UserEmail = userEmail;
            }
        }

        #region MenuItems

        public async Task<IActionResult> MenuItemsList()
        { 
            if (_user.IsAdmin)
            {
                return View(await _context.MenuItems.OrderBy(x => x.MenuItemName).ToListAsync());
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public IActionResult AddMenuItem()
        {
            if (_user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        //ADD MENU ITEM TO DATABASE [Bind("Position,MenuItemName,MenuItemDesc,MenuItemPrice,NoticeReq")]
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(MenuItems menuitems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuitems);
                await _context.SaveChangesAsync();
                return RedirectToAction("MenuItemsList", "Admin");
            }
            return View(menuitems);
        }


        //EDIT ITEM AND SAVE BACK TO DATABASE
        public async Task<IActionResult> EditMenuItem(int? id)
        {
            if (_user.IsAdmin)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var menuitems = await _context.MenuItems.SingleOrDefaultAsync(m => m.MenuItemID == id);

                if (menuitems == null)
                {
                    return NotFound();
                }
                return View(menuitems);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuItem(int id, [Bind("MenuItemID,Position,MenuItemName,MenuItemDesc,MenuItemPrice,NoticeReq,RFD")] MenuItems menuitems)
        {
            if (id != menuitems.MenuItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuitems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuitems.MenuItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MenuItemsList));
            }
            return View(menuitems);
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.MenuItemID == id);
        }

        #endregion

        #region SystemUser

        public async Task<IActionResult> SystemUserList()
        {
            if (_user.IsAdmin)
            {
                return View(await _context.SystemUser.OrderBy(x => x.UserLastName).ThenBy(y => y.UserFirstName).ToListAsync());
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public IActionResult AddUser()
        {
            if (_user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        //ADD User TO DATABASE
        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("UserFirstName,UserLastName,UserEmail,EthosName,IsAdmin,IsCaterer")] SystemUser newuser)
        {
            if (_user.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(newuser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("SystemUserList", "Admin");
                }
                return View(newuser);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        //edit User
        public async Task<IActionResult> EditUser(int? id)
        {
            if(_user.IsAdmin)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var amenduser = await _context.SystemUser.SingleOrDefaultAsync(m => m.UserID == id);

                if (amenduser == null)
                {
                    return NotFound();
                }
                return View(amenduser);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }


        //save amend user to the database
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, [Bind("UserID,UserFirstName,UserLastName,UserEmail,EthosName,IsAdmin,IsCaterer")] SystemUser amenduser)
        {
            if (id != amenduser.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amenduser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(amenduser.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SystemUserList));
            }
            return View(amenduser);
        }

        //Delete User
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if(_user.IsAdmin)
        {
                var delete = await _context.SystemUser.SingleOrDefaultAsync(m => m.UserID == id);
                _context.SystemUser.Remove(delete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SystemUserList));
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        private bool UserExists(int id)
        {
            {
                return _context.SystemUser.Any(e => e.UserID == id);
            }
        }

        #endregion


        public IActionResult SearchOrder()
        {
            if (_user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public IActionResult AdminMenu()
        {
            if (_user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public async Task<IActionResult> Rooms()
        {
            if(_user.IsAdmin)
            {
                return View(await _context.Rooms.OrderBy(x => x.RoomID).ToListAsync());
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public async Task<IActionResult> EditRoom(int? id)
        {
            if(_user.IsAdmin)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var editRoom = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomID == id);

                if (editRoom == null)
                {
                    return NotFound();
                }
                return View(editRoom);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(int id, [Bind("RoomID,RoomDesc,RFD")] Rooms editroom)
        {
            if (id != editroom.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(editroom.RoomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Rooms));
            }
            return View(editroom);
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }

        public IActionResult AddRoom()
        {
            if(_user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        //Add room
        [HttpPost]
        public async Task<IActionResult> AddRoom ([Bind("RoomDesc,RFD")] Rooms newroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newroom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Rooms", "Admin");
            }
            return View(newroom);
        }
    }
}
