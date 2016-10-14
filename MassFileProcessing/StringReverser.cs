namespace MassFileProcessing
{
    internal class StringReverser
    {
        public string Reverse(string line)
        {
            var charArray = line.ToCharArray();
            string stringResult = null;

            for (var i = charArray.Length; i > 0; i--)
            {
                stringResult += charArray[i - 1];
            }
            return stringResult;
        }
    }
}