using System;
using System.Collections.Generic;
using System.Text;
using Vogen;

namespace AvoidPrimitiveObsession.ValueObjects
{
    [ValueObject<decimal>]
    public readonly partial struct OrderTotal
    {
        public static Validation Validate(decimal value) => 
            value < 0 ? 
            Validation.Invalid("Order total cannot be less than 0") :
            Validation.Ok;
    }
}
