using System;
using AvoidPrimitiveObsession;
using Xunit;

namespace PrimitiveObsession.Tests
{
    public class OrderWithPrimitivesTests
    {
        [Fact]
        public void IsValid_ReturnsFalse_WhenEmailIsInvalid()
        {
            var order = new Order("Alice","not-an-email", 100m, DateTime.UtcNow.AddDays(-1));

            Assert.False(order.IsValid());
        }

        [Fact]
        public void IsValid_ReturnsFalse_WhenOrderTotalIsNonPositive()
        {
            var order = new Order("Bob","bob@example.com", -10m, DateTime.UtcNow.AddDays(-1));

            Assert.False(order.IsValid());
        }

        [Fact]
        public void UpdateCustomer_AllowsInvalidEmail()
        {
            var order = new Order("Carol","carol@example.com", 50m, DateTime.UtcNow.AddDays(-1));

            // Primitive obsession allows callers to set invalid primitives
            order.UpdateCustomer("Carol", "invalid-email");

            // The order will now be in an invalid state but the class allowed it
            Assert.False(order.IsValid());
        }
    }
}
