using System;

namespace Suduko
{
    public static class InputValidator
        {
            public static bool IsValidInput(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new InputException("Input is empty");
                }

                int length = input.Length;
                int size = (int)Math.Sqrt(length);
                if (size * size != length)
                {
                throw new InputException("The input size does not create a square board.");
                }

                int cobe = (int)Math.Sqrt(size);
                if (cobe * cobe != size)
                {
                    throw new InputException("The input size does not create a square cobe. ");
                }

                foreach (char c in input)
                {
                    if (c < '0' || c > '9')
                    {
                    throw new InputException("The input contains invalid characters. Only digits 0-9 are allowed.");
                }
                }

               

            return true;
            }
        }
}
