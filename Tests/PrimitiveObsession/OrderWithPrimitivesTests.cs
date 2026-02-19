using System;
using AvoidPrimitiveObsession;
using AvoidPrimitiveObsession.ValueObjects;
using Newtonsoft.Json;
using Xunit;

namespace PrimitiveObsession.Tests
{
    public class OrderWithPrimitivesTests
    {
        [Fact]
        public void IsValid_ReturnsFalse_WhenEmailIsInvalid()
        {
            var order = new Order("Alice", CustomerEmail.From("not-an-email"), OrderTotal.From(100m), DateTime.UtcNow.AddDays(-1));

            Assert.False(order.IsValid());
        }

        [Fact]
        public void IsValid_ReturnsFalse_WhenOrderTotalIsNonPositive()
        {
            var order = new Order("Bob", CustomerEmail.From("bob@example.com"), OrderTotal.From(-10m), DateTime.UtcNow.AddDays(-1));

            Assert.False(order.IsValid());
        }

        [Fact]
        public void UpdateCustomer_AllowsInvalidEmail()
        {
            var order = new Order("Carol", CustomerEmail.From("carol@example.com"), OrderTotal.From(50m), DateTime.UtcNow.AddDays(-1));

            // Primitive obsession allows callers to set invalid primitives
            order.UpdateCustomer("Carol", CustomerEmail.From("invalid-email"));

            // The order will now be in an invalid state but the class allowed it
            Assert.False(order.IsValid());
        }

        [Fact]
        public void CustomerEmail_Serializes_Value_WithoutNesting()
        {
            var primitiveEmail = "\"aaron.martin@plainconcepts.com\"";

            var email = CustomerEmail.From("aaron.martin@plainconcepts.com");

            var serialized = JsonConvert.SerializeObject(email);

            //Should be a simple string, not something like {value:"email"}
            Assert.Equal(primitiveEmail, serialized);

            var deserialized = JsonConvert.DeserializeObject<CustomerEmail>(serialized);

            //Equality comparison should be by value not by reference.
            Assert.Equal(email, deserialized);
        }
    }
}
