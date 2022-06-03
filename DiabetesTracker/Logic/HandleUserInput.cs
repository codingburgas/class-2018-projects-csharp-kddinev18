using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Logic
{
    public static class HandleUserInput
    {
        public static bool GeneralHandler(string userInput)
        {
            if(string.IsNullOrEmpty(userInput) || userInput.Contains(", ") || userInput.Contains('|'))
                return false;

            return true;
        }
    }
}
