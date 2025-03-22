using System;

namespace sefaodev.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string[] Stars { get; set; }
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; }

        public Movie(int movieID, string title, string director, string[] stars, int releaseYear, string imageUrl)
        {
            MovieID = movieID;
            Title = title;
            Director = director;
            Stars = stars;
            ReleaseYear = releaseYear;
            ImageUrl = imageUrl;
        }

        
        public override string ToString()
        {
            return $"{Title} ({ReleaseYear}) - Directed by {Director}";
        }
    }
}
