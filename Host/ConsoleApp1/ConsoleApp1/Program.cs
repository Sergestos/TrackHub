namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = GetValues().ToList();
            if (result == null)
            {
                Console.WriteLine("collection is null");
            }
            else
            {
                foreach (var value in result)
                {
                    Console.WriteLine(value);
                }
            }            
        }

        static IEnumerable<string> GetValues()
        {
            IEnumerable<string>? values = new List<string>();

            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}
