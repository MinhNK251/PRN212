using Repository.Models;
using Service;
using System.Globalization;
using System.Windows;

namespace MovieManagement
{
	public partial class UpdateCreateWindow : Window
	{
		private ActorsService _actorService = new();
		private DirectorsService _directorService = new();
		private MoviesService _movieService = new();
		public Movie SelectedMovie { get; set; } = null;
		public UpdateCreateWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			cbbDirector.ItemsSource = _directorService.GetAll();
			cbbDirector.DisplayMemberPath = nameof(Director.DirectorName);
			cbbDirector.SelectedValuePath = nameof(Director.DirectorId);
			cbbStar1.ItemsSource = _actorService.GetAll();
			cbbStar1.DisplayMemberPath = nameof(Actor.ActorName);
			cbbStar1.SelectedValuePath = nameof(Actor.ActorId);
			cbbStar2.ItemsSource = _actorService.GetAll();
			cbbStar2.DisplayMemberPath = nameof(Actor.ActorName);
			cbbStar2.SelectedValuePath = nameof(Actor.ActorId);
			cbbStar3.ItemsSource = _actorService.GetAll();
			cbbStar3.DisplayMemberPath = nameof(Actor.ActorName);
			cbbStar3.SelectedValuePath = nameof(Actor.ActorId);
			cbbStar4.ItemsSource = _actorService.GetAll();
			cbbStar4.DisplayMemberPath = nameof(Actor.ActorName);
			cbbStar4.SelectedValuePath = nameof(Actor.ActorId);
			if (SelectedMovie != null)
            {
                lblCreateUpdate.Content = "Update Movie";
				txtMovieId.Text = SelectedMovie.MovieId.ToString();
				txtPosterLink.Text = SelectedMovie.PosterLink;
				txtTitle.Text = SelectedMovie.SeriesTitle;
				txtYear.Text = SelectedMovie.ReleasedYear.ToString();
				txtCertificate.Text = SelectedMovie.Certificate;
				txtRuntime.Text = SelectedMovie.Runtime;
				txtGenre.Text = SelectedMovie.Genre;
				txtImdbrating.Text = SelectedMovie.Imdbrating.ToString();
				txtOverview.Text = SelectedMovie.Overview;
				txtMetaScore.Text = SelectedMovie.MetaScore.ToString();
				cbbDirector.SelectedValue = SelectedMovie.DirectorId;
				cbbStar1.SelectedValue = SelectedMovie.Star1Id;
				cbbStar2.SelectedValue = SelectedMovie.Star2Id;
				cbbStar3.SelectedValue = SelectedMovie.Star3Id;
				cbbStar4.SelectedValue = SelectedMovie.Star4Id;
			}
            else lblCreateUpdate.Content = "Create Movie";
		}

		public bool Validation()
		{
			// Check for required fields
			if (string.IsNullOrWhiteSpace(txtPosterLink.Text) ||
				string.IsNullOrWhiteSpace(txtTitle.Text) ||
				string.IsNullOrWhiteSpace(txtYear.Text) ||
				string.IsNullOrWhiteSpace(txtCertificate.Text) ||
				string.IsNullOrWhiteSpace(txtRuntime.Text) ||
				string.IsNullOrWhiteSpace(txtGenre.Text) ||
				string.IsNullOrWhiteSpace(txtImdbrating.Text) ||
				string.IsNullOrWhiteSpace(txtOverview.Text) ||
				string.IsNullOrWhiteSpace(txtMetaScore.Text) ||
				cbbDirector.SelectedValue == null ||
				cbbStar1.SelectedValue == null ||
				cbbStar2.SelectedValue == null ||
				cbbStar3.SelectedValue == null ||
				cbbStar4.SelectedValue == null)
			{
				MessageBox.Show("All fields are required!");
				return false;
			}

			// Validate the released year is a valid integer
			if (!int.TryParse(txtYear.Text, out int releasedYear) || releasedYear <= 0)
			{
				MessageBox.Show("Invalid released year. Please enter a valid year.");
				return false;
			}

			// Validate the IMDB rating is a valid double
			if (!double.TryParse(txtImdbrating.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double imdbRating) || imdbRating < 0 || imdbRating > 10)
			{
				MessageBox.Show("Invalid IMDB rating. Please enter a valid rating between 0 and 10.");
				return false;
			}

			// Validate MetaScore is a valid integer
			if (!int.TryParse(txtMetaScore.Text, out int metaScore) || metaScore < 0)
			{
				MessageBox.Show("Invalid MetaScore. Please enter a valid non-negative integer.");
				return false;
			}

			// Validate the selected director and stars are valid
			if (cbbDirector.SelectedValue == null ||
				cbbStar1.SelectedValue == null ||
				cbbStar2.SelectedValue == null ||
				cbbStar3.SelectedValue == null ||
				cbbStar4.SelectedValue == null)
			{
				MessageBox.Show("Please select valid options for Director and Stars.");
				return false;
			}

			return true;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (!Validation())
			{
				return;
			}
			if (SelectedMovie != null)
			{
				_movieService.Update(new Movie
				{
					MovieId = int.Parse(txtMovieId.Text),
					PosterLink = txtPosterLink.Text,
					SeriesTitle = txtTitle.Text,
					ReleasedYear = int.Parse(txtYear.Text),
					Certificate = txtCertificate.Text,
					Runtime = txtRuntime.Text,
					Genre = txtGenre.Text,
					Imdbrating = double.Parse(txtImdbrating.Text),
					Overview = txtOverview.Text,
					MetaScore = int.Parse(txtMetaScore.Text),
					DirectorId = (int)cbbDirector.SelectedValue,
					Star1Id = (int)cbbStar1.SelectedValue,
					Star2Id = (int)cbbStar2.SelectedValue,
					Star3Id = (int)cbbStar3.SelectedValue,
					Star4Id = (int)cbbStar4.SelectedValue
				});
				MessageBox.Show("Movie updated successfully.");

			}
			else
			{
				_movieService.Add(new Movie
				{
					PosterLink = txtPosterLink.Text,
					SeriesTitle = txtTitle.Text,
					ReleasedYear = int.Parse(txtYear.Text),
					Certificate = txtCertificate.Text,
					Runtime = txtRuntime.Text,
					Genre = txtGenre.Text,
					Imdbrating = double.Parse(txtImdbrating.Text),
					Overview = txtOverview.Text,
					MetaScore = int.Parse(txtMetaScore.Text),
					DirectorId = (int)cbbDirector.SelectedValue,
					Star1Id = (int)cbbStar1.SelectedValue,
					Star2Id = (int)cbbStar2.SelectedValue,
					Star3Id = (int)cbbStar3.SelectedValue,
					Star4Id = (int)cbbStar4.SelectedValue
				});
				MessageBox.Show("Movie added successfully.");
			}
			this.Close();
		}
	}
}
