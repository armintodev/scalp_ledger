using System.Text;

namespace ScalpLedger.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new StringBuilder(input.Length + Math.Min(2, input.Length));
        // A small buffer expansion to accommodate underscores without frequent re-allocations.

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (char.IsUpper(c))
            {
                // If not the first character:
                //   1) If the previous char is uppercase but the *next* char is lowercase,
                //      that means we're at the last uppercase letter in a consecutive block, so insert underscore.
                //
                //   2) If the previous char is not uppercase, we're transitioning from a lower/digit to an uppercase,
                //      so insert underscore.
                if (i > 0)
                {
                    bool prevCharIsUpper = char.IsUpper(input[i - 1]);
                    bool nextCharIsLower = (i < input.Length - 1) && char.IsLower(input[i + 1]);

                    if ((prevCharIsUpper && nextCharIsLower) || !prevCharIsUpper)
                    {
                        sb.Append('_');
                    }
                }

                sb.Append(char.ToLower(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}