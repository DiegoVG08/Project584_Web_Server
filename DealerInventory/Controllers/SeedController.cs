using DealerInventory.Data;
using DealerInventory.Data.DealerModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;    
using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;




namespace DealerInventory.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   // [Authorize(Roles = "Administrator")]
    public class SeedController : Controller
    {
        private readonly MasterContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public SeedController(MasterContext context, RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager, IWebHostEnvironment env, IConfiguration configuration)
        {
            _context = context;
            _env = env;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            // Prevents non-development environments from running this method
            if (!_env.IsDevelopment())
                throw new SecurityException("Not allowed");

            var path = Path.Combine(
                _env.ContentRootPath,
                "Data/Source/Dealer.xlsx");

            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            // Get the first worksheet
            var worksheet = excelPackage.Workbook.Worksheets[0];

            // Define how many rows we want to process
            var nEndRow = worksheet.Dimension.End.Row;

            // Initialize the record counters
            var numberOfDealersAdded = 0;
            var numberOfVehiclesAdded = 0;

            // create a lookup dictionary 
            // containing all the dealers already existing 
            // into the Database (it will be empty on first run).
            var DealerByName = _context.VehicleTypes
                .AsNoTracking()
                .ToDictionary(x => x.Make, StringComparer.OrdinalIgnoreCase);

            // iterates through all rows, skipping the first one 
            //CarDealersship
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

               // var VehicleTypeId = row[nRow, 8].GetValue<int>();
                var Make = row[nRow, 4].GetValue<string>();
                var Model = row[nRow, 5].GetValue<string>();
               var Year = row[nRow, 6].GetValue<int>();
              //  var DealershipId =  row[nRow, 9].GetValue<int>();

                // skip this country if it already exists in the database
                if (DealerByName.ContainsKey(Make))
                    continue;
                // create the CarDealeship entity and fill it with xlsx data 
                var Vehicle = new VehicleType
                {
                   // VehicleTypeID =VehicleTypeId,
                    Make  = Make,
                    Model = Model,
                    Year = Year,
                   // DealershipID= DealershipId
                };
                // add the new country to the DB context 
                await _context.VehicleTypes.AddAsync(Vehicle);
                // store the country in our lookup to retrieve its Id later on
                
                if (!DealerByName.ContainsKey(Make))
                {
                    DealerByName.Add(Make, Vehicle);
                }
                // increment the counter 
                numberOfDealersAdded++;
            }
            // save all the countries into the Database 
            if (numberOfDealersAdded > 0)
                await _context.SaveChangesAsync();
            // create a lookup dictionary
            // containing all the cities already existing 
            // into the Database (it will be empty on first run). 

            var dealers = _context.carDealership
               .AsNoTracking()
               .ToDictionary(x => (
                   Name: x.Name,
                   Location: x.Location,
                   ContactInfo: x.ContactInfo));
                   //DealershipID: x.DealershipID));
            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

               // var DealershipID = row[nRow, 1].GetValue<string>();
                var Name = row[nRow, 1].GetValue<string>();
                var Location = row[nRow, 2].GetValue<string>();
                var ContactInfo = row[nRow, 3].GetValue<string>();
               

                // retrieve country Id by countryName
                //var DealerID = DealerByName[DealershipID].VehicleTypeID;
                // skip this city if it already exists in the database
               /* if (dealers.ContainsKey((
                    Make: make,
                    Model: model,
                    Year: year,
                    DealershipID: VehicleId)))
                    continue;*/
                // create the City entity and fill it with xlsx data 
                var carDealership = new CarDealership
                {
                   // DealershipID = DealerID,
                    Name = Name,
                    Location = Location,
                    ContactInfo = ContactInfo
                    
                };
                // add the new city to the DB context 
                _context.carDealership.Add(carDealership);
                // increment the counter 
                numberOfVehiclesAdded++;
            }
            // save all the cities into the Database 
            if (numberOfVehiclesAdded > 0)
                await _context.SaveChangesAsync();
            return new JsonResult(new
            {
                carDealership = numberOfVehiclesAdded,
                VehicleTypes = numberOfDealersAdded
                
            });
        }
        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            // setup the default role names
            string role_RegisteredUser = "RegisteredUser";
            string role_Administrator = "Administrator";

            // create the default roles (if they don't exist yet)
            if (await _roleManager.FindByNameAsync(role_RegisteredUser) ==
             null)
                await _roleManager.CreateAsync(new
                 IdentityRole(role_RegisteredUser));

            if (await _roleManager.FindByNameAsync(role_Administrator) ==
             null)
                await _roleManager.CreateAsync(new
                 IdentityRole(role_Administrator));

            // create a list to track the newly added users
            var addedUserList = new List<ApplicationUser>();

            // check if the admin user already exists
            var email_Admin = "admin@email.com";
            if (await _userManager.FindByNameAsync(email_Admin) == null)
            {
                // create a new admin ApplicationUser account
                var user_Admin = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin,
                };

                string defaultPassword_Admin = _configuration["DefaultPasswords:Administrator"] ?? "Admin@#123"; // provide a default password value if configuration value is null or empty

                // insert the admin user into the DB
                var result = await _userManager.CreateAsync(user_Admin, defaultPassword_Admin);

                if (!result.Succeeded) // check if the operation was successful
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description); // return an error response
                }

                await _context.SaveChangesAsync(); // save changes here

                // assign the "RegisteredUser" and "Administrator" roles
                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);

                // confirm the e-mail and remove lockout
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;

                // add the admin user to the added users list
                addedUserList.Add(user_Admin);
            }

            // check if the standard user already exists
            var email_User = "user@email.com";
            if (await _userManager.FindByNameAsync(email_User) == null)
            {
                // create a new standard ApplicationUser account
                var user_User = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };

                string defaultPassword_User = _configuration["DefaultPasswords:RegisteredUser"] ?? "User@#123"; // provide a default password value if configuration value is null or empty

                // insert the standard user into the DB
                var result = await _userManager.CreateAsync(user_User, defaultPassword_User);

                if (!result.Succeeded) // check if the operation was successful
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description); // return an error response
                }

                await _context.SaveChangesAsync(); // save changes here

                // assign the "RegisteredUser" role
                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);

                // confirm the e-mail and remove lockout
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;

                // add the standard user to the added users list
                addedUserList.Add(user_User);
            }

            // if we added at least one user, persist the changes into the DB
            if (addedUserList.Count > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList
            });
        }
    }
}
