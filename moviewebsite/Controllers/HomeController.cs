using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using sefaodev.Models;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

         private static readonly List<LoginModel> Users = new List<LoginModel>
        {
            new LoginModel { UserName = "admin", Password = "1234" },
            new LoginModel { UserName = "user", Password = "abcd" }
        };

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult HomePage()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (string.IsNullOrEmpty(session.GetString("Movies")))
            {
                var movies = new List<Movie>
                {
                    new Movie(1, "X-Men: The Last Stand", "Brett Ratner", new string[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, 2006, "/images/xmen.jpg"),
                    new Movie(2, "Spider Man 2", "Sam Raimi", new string[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, 2004, "/images/spiderman2.jpg"),
                    new Movie(2, "Spider Man 3", "Sam Raimii", new string[] { "Tobey guire", "Kirsten unst", "Alfred Molina" }, 2004, "/images/spiderman2.jpg")
                };

                var jsonMovies = JsonSerializer.Serialize(movies);
                session.SetString("Movies", jsonMovies);
            }

            return View();
        }

        public IActionResult MovieInfo(int id)
{
    
    var session = _httpContextAccessor.HttpContext.Session.GetString("Movies");

    if (!string.IsNullOrEmpty(session))
    {
        var movies = JsonSerializer.Deserialize<List<Movie>>(session);

        var movie = movies.FirstOrDefault(m => m.MovieID == id);

        if (movie != null)
        {
            return View(movie);
        }
        else
        {
            ViewBag.ErrorMessage = "Movie not found!";
        }
    }
    else
    {
        ViewBag.ErrorMessage = "No movies found in session!";
    }

    return View();
}

    
    [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Users.FirstOrDefault(u => 
                    u.UserName == model.UserName && u.Password == model.Password);

                if (user != null)
                {
                    TempData["SuccessMessage"] = "Giriş başarılı!";
                    return RedirectToAction("HomePage");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
                }
            }
            return View(model);
        }
    }
}
