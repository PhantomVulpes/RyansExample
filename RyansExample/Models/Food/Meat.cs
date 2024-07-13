namespace RyansExample.Models.Food
{
    public class Meat : IEdible
    {
        /// <summary>
        /// What animal the meat was made out of. Meat doesn't grow on trees, you know.
        /// </summary>
        public Animal Animal { get; private set; }
        public double HungerReduction { get => Animal.Weight / 3; }

        public Meat(Animal animal)
        {
            Animal = animal;
        }

    }
}
