namespace Services.Services
{
    using Interfaces;
    using System.Text.RegularExpressions;

    public class RefereeService : IRefereeService
    {
        public bool IsValid(string combination)
        {
            if (combination.Length == 4)
            {
                Regex regex = new Regex("[(r|j|v|b|o|n)]+$", RegexOptions.IgnoreCase);

                return regex.Match(combination).Success;
            }

            return false;
        }
    }
}
