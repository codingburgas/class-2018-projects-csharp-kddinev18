using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiabetesTracker.Logic
{
    public static class HandleUserInput
    {
        public static bool GeneralHandler(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || userInput.Contains(", ") || userInput.Contains('|'))
            {
                MessageBox.Show("Your input must not contain \', \' or \'|\'", "Error");
                return false;
            }

            return true;
        }

        public static bool GeneralHandler(params string[] userInputs)
        {
            foreach (string userInput in userInputs)
            {
                if (string.IsNullOrEmpty(userInput) || userInput.Contains(", ") || userInput.Contains('|'))
                {
                    MessageBox.Show("Your input must not contain \', \' or \'|\'", "Error");
                    return false;
                }
            }

            return true;
        }
    }
}
