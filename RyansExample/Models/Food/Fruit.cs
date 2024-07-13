namespace RyansExample.Models.Food
{
    public class Fruit : IEdible
    {
        public double HungerReduction { get; private set; }
        public Fruit(double hungerReduction)
        {
            HungerReduction = hungerReduction;
        }

        /// <summary>
        /// Create a fruit with a random HungerReduction value.
        /// </summary>
        /// <returns></returns>
        public static Fruit MakeRandomFruit()
        {
            Random random = new Random();

            return new Fruit(random.Next(0, 10));
        }
    }
}
