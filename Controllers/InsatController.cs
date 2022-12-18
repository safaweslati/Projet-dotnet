using Insat.Data;
using Insat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Transactions;


namespace Insat.Controllers
{
    public class InsatController : Controller
    {
       public ClubsInsatContext context = new ClubsInsatContext();
        private readonly ILogger<InsatController> _logger;

        public InsatController(ILogger<InsatController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();


        }

        public IActionResult Clubs()
        {
            return View(context);
        }

        public IActionResult Events()
        {
            return View(context);
        }

        [HttpGet]
        //  /Insat/JoinClub/id
        public IActionResult JoinClub(long id)
        {
            var club=context.Clubs.Find(id); 
            ViewBag.club=club;
            return View();
        }
      //  [HttpPost]
      public IActionResult JoinClub(Student std,long id)
        {
            var club = context.Clubs.Find(id);
            ViewBag.club = club;

            ViewBag.club = context.Clubs.Find(id);

            if (std.Ninscri == null || std.FirstName == null || std.LastName == null || std.Email == null )
            {
                ViewData["Data"] = "vrai";
                return View();
            }
            else
            {
                var s = context.Students.Find(std.Ninscri);
                
                if ( s!= null )
                {
                   ViewData["stud"] = "vrai";
                   return View();
                }
                
            }
            ViewData["std"] = "vrai";
            std.Clubs.Add(club);
            club.Studs.Add(std);
            context.Students.Add(std);
            context.SaveChanges();

            return RedirectToAction("ClubMembers",new { id = club.Id});
       
        }
        
        public IActionResult ClubMembers(long id) 
        {
            var club = context.Clubs.Find(id);
            ViewBag.club = club;
            return(View(context.Clubs)); 
        }


        public IActionResult Members(int id)
        {
           ClubsInsatContext context = new ClubsInsatContext();
            return View(context);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}