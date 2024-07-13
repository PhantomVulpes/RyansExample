using RyansExample.Models.Food;

namespace RyansExample.Models
{
    public class Animal
    {
        public string Name { get; private set; }
        public string Species { get; private set; }

        private double weight;
        public double Weight { get => weight; }

        private double hungerLevel;
        public double HungerLevel { get => hungerLevel; }

        public Animal(string name, string species, double weight)
        {
            Name = name;
            Species = species;
            this.weight = weight;
            hungerLevel = 50;
        }

        public void Feed(IEdible food)
        {
            // Reduce the animal's hunger level by the provided food's reduction level.
            hungerLevel -= food.HungerReduction;

            // Hunger being below 0 isn't allowed.
            if (hungerLevel < 0)
            {
                hungerLevel = 0;

                // This animal is satisfied, so it puts on weight.
                weight += food.HungerReduction * .3;
            }
            else
            {
                // This animal has eaten food, it gains some weight.
                weight += food.HungerReduction * .1;
            }
        }

        public void Wait()
        {
            Random random = new Random();

            // Reduce the animal's hunger value by a random double value between 0 and 15.
            hungerLevel += random.Next(0, 15);

            // If their hunger level is already max, it cannot go higher.
            if (hungerLevel > 100)
            {
                hungerLevel = 100;
            }

            // They are sitting around burning calories, reduce it by a random value.
            weight -= random.Next(0, 5);

            // Prevent weight from dropping any lower.
            if (Weight < .5d)
            {
                weight = 0.5d;
            }
        }

        /// <summary>
        /// Checks fields of the Animal to determine if it is valid.
        /// </summary>
        /// <returns>A value indicating the animal is valid.</returns>
        public bool IsValid()
        {
            // Check if the following values are allowed.
            if (string.IsNullOrEmpty(Name)) { return false; }
            if (string.IsNullOrEmpty(Species)) { return false; }
            if (Weight > 100 || Weight < 1) { return false; }

            // If the method execution made it this far, then the animal is valid.
            return true;
        }

        /// <summary>
        /// Override object's ToString method so it is displayed properly in the WPF list box.
        /// </summary>
        public override string ToString()
        {
            double roundedWeight = Math.Round(Weight, 2);
            return $"{Name} the {Species}, Hunger: {HungerLevel}, Weight: {roundedWeight}";
        }
    }
}