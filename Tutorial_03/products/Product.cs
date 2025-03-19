namespace Tutorial_03.products
{
    public class Product
    {
        private string name;
        private float temperature;
        private bool isHazardous;

        public Product(string name, float temperature, bool isHazardous)
        {
            this.name = name;  // Use 'this' to refer to the instance variable.
            this.temperature = temperature;
            this.isHazardous = isHazardous;
        }

        public bool IsHazardous()
        {
            return isHazardous;
        }

        public float GetTemperature()
        {
            return temperature;
        }

        public string Name
        {
            get => name;  // Return the private backing field 'name'.
            set => name = value ?? throw new ArgumentNullException(nameof(value));  // Set the backing field.
        }
    }
}