using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Net.Mail;
using Vogen;

namespace AvoidPrimitiveObsession.ValueObjects
{
    [ValueObject<string>]
    public readonly partial struct CustomerEmail
    {
        private static string NormalizeInput(string? input) =>
            string.IsNullOrWhiteSpace(input) ? null : input.ToLowerInvariant();

        public static Validation Validate(string value)
        {
            var normalized = NormalizeInput(value);

            if (string.IsNullOrEmpty(normalized))
                return Validation.Invalid("Email must not be empty.");

            // RFCs allow up to 254 chars for an email address, but many sources cite 320 including display parts.
            // We'll enforce a conservative limit on the local@domain form.
            if (normalized.Length > 254)
                return Validation.Invalid("Email is too long.");

            try
            {
                var addr = new MailAddress(normalized);

                // MailAddress accepts some forms like "Name <addr@domain>", ensure exact match to avoid display-name being allowed.
                if (!string.Equals(addr.Address, normalized, StringComparison.OrdinalIgnoreCase))
                    return Validation.Invalid("Invalid email format.");

                return Validation.Ok;
            }
            catch (FormatException)
            {
                return Validation.Invalid("Invalid email format.");
            }
        }
    }
}
