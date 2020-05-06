using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered Genres
        /// </summary>
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public float IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public float IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomatoes minimium rating
        /// </summary>
        public float TomMin { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomatoes maximum rating
        /// </summary>
        public float TomMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, int? TomMin, int? TomMax) {
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.All;

            if (SearchTerms != null) {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
            }
            if (MPAARatings != null && MPAARatings.Length != 0) {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }
            if (Genres != null && Genres.Length != 0) {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }
            if(IMDBMin != null && IMDBMax != null) {
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
            else if (IMDBMin != null) {
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
            }
            else if (IMDBMax != null) {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
            }
            if (TomMin != null && TomMax != null) {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating >= TomMin && movie.RottenTomatoesRating <= TomMax);
            }
            else if (TomMin != null) {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating >= TomMin);
            }
            else if (TomMax != null) {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating <= TomMax);
            }
        }

    }
}