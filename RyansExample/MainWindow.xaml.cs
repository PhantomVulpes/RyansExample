using RyansExample.Models;
using RyansExample.Models.Food;
using System.Windows;
using System.Windows.Controls;

namespace RyansExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The list of animals we will be using to keep track of what's been added to our zoo.
        /// </summary>
        private List<Animal> animals = new List<Animal>();

        /// <summary>
        /// The default value for animal names, used for setting and clearing placeholder data.
        /// </summary>
        private readonly string animalNameDefault;

        /// <summary>
        /// The default value for animal species, used for settign and clearing placeholder data.
        /// </summary>
        private readonly string animalSpeciesDefault;

        public MainWindow()
        {
            InitializeComponent();

            // Set the default values as whatever the textboxes hold at the start of the application.
            animalNameDefault = AnimalNameTextBox.Text;
            animalSpeciesDefault = AnimalSpeciesTextBox.Text;

            AppWindow.Title = "Ryan's Robust Ranimal Rzoo";
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AppWindow.Title = "MRCS Logging";

            // Load the animals textbox.
            PopulateAnimalsListBox();

            // Set the weight label to ensure it is properly displayed on startup.
            SetWeightLabel();
        }

        /// <summary>
        /// Set the label for weight to display what the value of the weight slider is.
        /// </summary>
        private void SetWeightLabel()
        {
            // Round the weight to 2 decimal places. Any more specific is unneeded.
            double roundedWeight = Math.Round(WeightSlider.Value, 2);

            // Set the text value for the weight textbox.
            WeightTextBlock.Text = "Weight (" + roundedWeight.ToString() + ")";
        }

        /// <summary>
        /// If a textbox's data is a provided string, clear it so the user can enter their own data freely.
        /// </summary>
        /// <param name="textBox">Which textbox to clear data from.</param>
        /// <param name="placeholder">The string to check against.</param>
        private static void ClearPlaceholder(TextBox textBox, string placeholder)
        {
            // If the text in the provided textbox is not the provided placeholder, no action is needed.
            if (textBox.Text != placeholder)
            {
                // A method that returns a value will end its execution and go back to the caller of the method. No code beyond it will be called.
                // We can use this functionality in a void method to end it early as well, preventing our entire method from being wrapped in a logic check and keep our code cleaner.
                return;
            }

            textBox.Text = string.Empty;
        }

        /// <summary>
        /// If a provided textbox value is empty, we can apply the placeholder.
        /// </summary>
        /// <param name="textBox">The textbox to reapply the placeholder to.</param>
        /// <param name="placeholder">The placeholder to apply.</param>
        private static void SetPlaceholder(TextBox textBox, string placeholder)
        {
            // If the text is not empty, we do not want to clear it. No action is needed, method can return early.
            if (textBox.Text != string.Empty)
            {
                return;
            }

            textBox.Text = placeholder;
        }

        private void AnimalNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearPlaceholder(AnimalNameTextBox, animalNameDefault);
        }

        private void AnimalNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetPlaceholder(AnimalNameTextBox, animalNameDefault);
        }

        private void AnimalSpeciesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetPlaceholder(AnimalSpeciesTextBox, animalSpeciesDefault);
        }

        private void AnimalSpeciesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearPlaceholder(AnimalSpeciesTextBox, animalSpeciesDefault);
        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // If the animal species has not been entered, we don't want to use the placeholder data.
            string animalSpecies = string.Empty;
            if (!string.IsNullOrEmpty(animalSpecies))       // string has a built-in method to check if the value is "" or null in one check.
            {
                animalSpecies = AnimalSpeciesTextBox.Text;
            }

            // This is shorthand for what was written above, called a ternary operator.
            // If the value AnimalNameTextBox.Text is the placeholder value, set the animalName variable to string.Empty. Else, set it to AnimalNameTextBox.Text.
            string animalName = AnimalNameTextBox.Text == animalNameDefault ? string.Empty : AnimalNameTextBox.Text;

            // Create the new animal.
            Animal animal = new Animal(AnimalNameTextBox.Text, AnimalSpeciesTextBox.Text, WeightSlider.Value);

            // Ask the newly created animal object if it is valid. If so, add it to our zoo.
            if (animal.IsValid())
            {
                animals.Add(animal);

                // Clear the textboxes to their defaults.
                AnimalSpeciesTextBox.Text = animalSpeciesDefault;
                AnimalNameTextBox.Text = animalNameDefault;
            }
            else
            {
                // If the animal is not valid, WPF apps have a built-in message box feature to create an error window.
                MessageBox.Show("Animal's values were not all valid. Make sure all data is entered and try again.");
            }

            // Refresh the animals in the window.
            PopulateAnimalsListBox();
        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetWeightLabel();
        }

        private void PopulateAnimalsListBox()
        {
            // Clear out the animals in the listbox to make sure there is no stale data.
            AnimalsListBox.Items.Clear();

            // Re-add every animal to the listbox.
            foreach (var animal in animals)
            {
                AnimalsListBox.Items.Add(animal);
            }

            // Set the animals header to show how many animals are in the zoo.
            AnimalsListBoxHeader.Text = "Animals: " + animals.Count;
        }

        private void FeedFruitButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a fruit to feed to the animals.
            Fruit fruit = Fruit.MakeRandomFruit();

            // For every animal in the zoo, we want to feed it.
            foreach (Animal animal in animals)
            {
                // Pass in the fruit.
                animal.Feed(fruit);
            }

            // Since we updated the animals, we should update the animals list box to reflect the new information.
            PopulateAnimalsListBox();
        }

        /// <summary>
        /// Get the animal that is currently selected on the list box.
        /// </summary>
        /// <returns>The currently selected animal, as an Animal.</returns>
        /// <exception cref="NullReferenceException">If no animal is selected, throws.</exception>
        private Animal GetSelectedAnimal()
        {
            // If there is no selected item, we can throw an error.
            if (AnimalsListBox.SelectedItem == null)
            {
                throw new NullReferenceException("No animal is selected.");
            }

            // Cast the generic object from the list box into an Animal type object.
            return AnimalsListBox.SelectedItem as Animal;
        }

        private void FeedMeatButton_Click(object sender, RoutedEventArgs e)
        {
            // If we don't have enough animals, we don't allow the user to feed meat to the animals.
            if (animals.Count <= 1)
            {
                MessageBox.Show("You do not have any animals that can be fed to other animals.", "Why are you even trying this Q-Q");
                return;     // Not enough animals, return and end the method early.
            }

            // To allow for error handling where something can fail, we wrap it in a try/catch block. Try the following block...
            try
            {
                // Get the selected animal.
                Animal selectedAnimal = GetSelectedAnimal();

                // We're slaughtering this bitch, so remove it from the list.
                animals.Remove(selectedAnimal);

                // Create a new meat object, send it the selected animal.
                IEdible meat = new Meat(selectedAnimal);

                // For every animal that remains, we can feed the meat to them.
                foreach (Animal animal in animals)
                {
                    animal.Feed(meat);
                }

                // Reload the list box.
                PopulateAnimalsListBox();

                // Verbally abuse the user for being a bad person.
                MessageBox.Show(selectedAnimal.Name + " was just fed to the rest of the animals. Good job. Monster.", "Goodbye, " + selectedAnimal.Name);
            }
            // And catch the exceptions. We know what the null reference exception will be from, so we can use that as a parameter.
            catch (NullReferenceException ex)
            {
                MessageBox.Show("You must select an animal to feed meat to the remaining animals.", ex.Message);
            }
            // We can also choose to not catch any exception in particular.
            catch
            {
                MessageBox.Show("An unexpected error occured. A blessing, really. No one had to die.");
            }
        }

        private void SimulateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            // Every animal waits.
            foreach (Animal animal in animals)
            {
                animal.Wait();
            }

            // Reload the list box.
            PopulateAnimalsListBox();
        }
    }
}