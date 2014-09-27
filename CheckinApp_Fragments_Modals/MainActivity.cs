using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.IO;
using System.Collections;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace CheckinApp_Fragments_Modals
{
	[Activity (Label = "CheckinApp Modals", MainLauncher = true, Icon = "@drawable/icon", Theme="@android:style/Theme.Holo.Light")]
	public class MainActivity : Activity
	{
		private ListView listViewMovies;
		private EditText editTextNewMovie;
		private Button buttonAddMovie;
		private ArrayAdapter adapter;
		private ArrayList movies = new ArrayList();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			editTextNewMovie = FindViewById<EditText> (Resource.Id.editTextNewMovie);
			buttonAddMovie = FindViewById<Button> (Resource.Id.buttonAddMovie);

			listViewMovies = FindViewById<ListView> (Resource.Id.listViewMovies);
			adapter = new ArrayAdapter (this, Resource.Layout.MovieItem, new string[] { });

			listViewMovies.Adapter = adapter;

			listViewMovies.ItemLongClick += delegate(object sender, AdapterView.ItemLongClickEventArgs e) {
				InfoMovieDialogFragment dialog = new InfoMovieDialogFragment ();
				dialog.Movie = movies[e.Position] as Movie;

				dialog.Show(FragmentManager, "InfoMovieDialogFragment");
			};

			buttonAddMovie.Click += async delegate(object sender, EventArgs e) {
				string newMovie = editTextNewMovie.Text.Trim ();
				TMDB api = new TMDB();

				if (newMovie != "") {
					Task<object> resultsTask = api.searchMovies(newMovie);

					JObject results = await resultsTask as JObject;

					JArray moviesArray = (JArray)results["results"];

					foreach(var movieJSON in moviesArray) {
						adapter.Add(movieJSON["title"].ToString());

						Movie movie = new Movie();
						movie.Title = movieJSON["title"].ToString();
						movie.PosterPath = "http://image.tmdb.org/t/p/w154" + movieJSON["poster_path"].ToString();

						movies.Add(movie);
					}
				}
			};
		}
	}
}


