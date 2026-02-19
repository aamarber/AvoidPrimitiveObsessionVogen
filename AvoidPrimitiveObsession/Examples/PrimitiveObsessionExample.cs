namespace AvoidPrimitiveObsession
{
    // This class demonstrates the "primitive obsession" code smell
    // by using raw primitives for domain concepts instead of small value objects.
    public class Order
    {
        // Primitive types used to represent domain concepts
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public Order(string customerName, string customerEmail, decimal total, DateTime date)
        {
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Total = total;
            Date = date;
        }

        // Validation method that knows too much about the semantics of these primitives
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
                return false;

            if (string.IsNullOrWhiteSpace(CustomerEmail) || !CustomerEmail.Contains("@"))
                return false;

            if (Total <= 0)
                return false;

            if (Date > DateTime.UtcNow)
                return false;

            return true;
        }

        // Example of a method that passes primitives around instead of domain-specific types
        public void UpdateCustomer(string name, string email)
        {
            // no encapsulation of invariant rules for name/email — callers must get it right
            CustomerName = name;
            CustomerEmail = email;
        }
    }
}