namespace RickAndMorty.Tests
{
    using System;

    public static class Helper
    {
        public static int ToInt(this Enum myEnum)
        {
            return Convert.ToInt32(myEnum);
        }
    }
}
