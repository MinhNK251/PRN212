namespace Bai1
{
    public class Customer
    {
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }

        public Customer(int phoneNumber, string name, int gender)
        {
            PhoneNumber = phoneNumber;
            Name = name;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"Phone number: {PhoneNumber}, Name: {Name}, Gender: {Gender}";
        }
    }
}
