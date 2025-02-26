namespace Utilities.General
{
    public static class HashFunctions
    {
        public static int FNVHash(string value)
        {
            uint offset = 2166136261;
            uint primeNumber = 16777619;
            uint hash = offset;
			
            foreach (var VARIABLE in value)
            {
                hash ^= VARIABLE;
                hash *= primeNumber;
            }

            return (int)hash;
        }
    }
}