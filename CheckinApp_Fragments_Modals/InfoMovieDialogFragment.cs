
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CheckinApp_Fragments_Modals
{
	public class InfoMovieDialogFragment : DialogFragment
	{
		public Movie Movie { get; set; }
		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			base.OnCreateDialog (savedInstanceState);

			LayoutInflater inflater = Activity.LayoutInflater;

			View view = inflater.Inflate (Resource.Layout.InfoMovie, null);

			ImageView imageViewInfoMoviePicture = view.FindViewById<ImageView> (Resource.Id.imageViewInfoMoviePicture);
			TextView textViewInfoMovieTitle = view.FindViewById<TextView> (Resource.Id.textViewInfoMovieTitle);

			if (Movie != null) {
				if (Movie.PosterPath != null) {
					Koush.UrlImageViewHelper.SetUrlDrawable (imageViewInfoMoviePicture, Movie.PosterPath);
				}

				textViewInfoMovieTitle.Text = Movie.Title;
			}

			AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
			builder.SetView (view);
			builder.SetPositiveButton ("OK", (object sender, DialogClickEventArgs e) => {
			});

			return builder.Create ();
		}
	}
}

