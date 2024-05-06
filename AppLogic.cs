using System.Collections.Generic;

namespace GDIK
{
    public class AppLogic
    {
        public static bool DoReplacement(Dictionary<string, string> table, string s, out string result) {
            bool affected = false;
            result = s;
            
            foreach (var pair in table)
            {
                if (!affected && result.Contains(pair.Key)) {
                    affected = true;
                }
                result = result.Replace(pair.Key, pair.Value);
            }
            return affected;
        }
    }
}