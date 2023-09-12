namespace authentication.domain.MyUtils
{
    public static class RandomUtils
    {
        private static Random _random = new Random();

        // https://www.delftstack.com/howto/csharp/generate-random-alphanumeric-strings-in-csharp/
        public static string GetRandomAlphanumericString(int length)
        {
            const string characters = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
            //const string characters = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789";
            return new string(Enumerable.Repeat(characters, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomNumericString(int length)
        {
            const string characters = "0123456789";
            return new string(Enumerable.Repeat(characters, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomInteger(int min, int max)
        {
            return (int)_random.NextInt64(min, max);
        }
    }
}