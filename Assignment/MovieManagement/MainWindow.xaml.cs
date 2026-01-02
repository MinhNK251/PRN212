using Microsoft.Win32;
using Repository.Models;
using Service;
using System.Windows;

namespace MovieManagement
{
	public partial class MainWindow : Window
	{
		private MoviesService _moviesService = new MoviesService();
		public MainWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataGrid();
		}
		private void LoadDataGrid()
		{
			var result = _moviesService.GetAllMovies();
			dtgMovieList.ItemsSource = null;
			dtgMovieList.ItemsSource = result;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult answer = MessageBox.Show("Do you really want to close?", "Warning", MessageBoxButton.YesNo);
			if (answer == MessageBoxResult.No)
			{
				e.Cancel = true;
			}
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			var searchMovies = _moviesService.SearchMovies(txtTitle.Text);
			dtgMovieList.ItemsSource = null;
			dtgMovieList.ItemsSource = searchMovies;
		}

		private void btnCreate_Click(object sender, RoutedEventArgs e)
		{
			UpdateCreateWindow ud = new UpdateCreateWindow();
			ud.ShowDialog();
			LoadDataGrid();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			Movie? selected = dtgMovieList.SelectedItem as Movie;
			if (selected == null)
			{
				MessageBox.Show("Please choose a movie to delete!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete!", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if (answer == MessageBoxResult.No)
			{
				return;
			}
			_moviesService.Delete(selected);
			LoadDataGrid();
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (dtgMovieList.SelectedItem is Movie selectedMovie)
			{
				UpdateCreateWindow ud = new UpdateCreateWindow();
				ud.SelectedMovie = selectedMovie;
				ud.ShowDialog();
				LoadDataGrid();
			}
			else
			{
				MessageBox.Show("Please select a movie to update!");
			}
		}

		private async void btnUpload_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv"
			};
			if (openFileDialog.ShowDialog() == true)
			{
				string filePath = openFileDialog.FileName;
				try
				{
					await _moviesService.LoadCsvDataAsync(filePath);
					MessageBox.Show("Data loaded successfully!");
					Dispatcher.Invoke(() =>
					{
						LoadDataGrid();
					});
				}
				catch (Exception ex)
				{
					MessageBox.Show($"An error occurred while loading the CSV data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void btnClearAll_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult answer = MessageBox.Show("Are you sure you want to clear all!", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if (answer == MessageBoxResult.No)
			{
				return;
			}
			_moviesService.ClearAll();
			LoadDataGrid();
		}
	}
}