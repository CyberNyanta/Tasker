namespace Tasker.Core.AL.Utils
{
    public static class ValidationExtensions
    {
        public static bool IsLengthInRange(this string text, int maxLength = int.MaxValue, int minLength = 0)
        {
            return text.Length <= maxLength && text.Length >= minLength ? true : false;
        }
    }
}
