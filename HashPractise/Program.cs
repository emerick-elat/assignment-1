using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace HashPractise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hasSetOfStrings = new HashSet<string>();

            var hashSet = new HashSet<User>();
            hashSet.Add(new User() { FistName = "John", LastName = "Doe" });
            hashSet.Add(new User() { FistName = "Jane", LastName = "Doe" });


            Console.WriteLine($"Users {hashSet.Count}");
            Console.ReadKey();
        }
    }

    public struct User: IEquatable<User>
    {
        public string FistName { get; set;  }
        public string LastName { get; set; }


        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return LastName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(LastName) : 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is User u) return string.Equals(LastName, u.LastName, StringComparison.OrdinalIgnoreCase);
            return false;
        }

        public bool Equals(User u)
        {
            Queue<char> q = new Queue<char>();
            q.ToArray().ToString();
            return string.Equals(LastName, u.LastName, StringComparison.OrdinalIgnoreCase);
            

        }
    }

    //public class UserComparer : IEqualityComparer<User>
    //{
    //    public bool Equals(User? x, User? y)
    //    {
    //        if (x == null && y == null) return true;
    //        if (x == null || y == null) return false;
    //        if (ReferenceEquals(x, y)) return true;
    //        return string.Equals(x.LastName, y.LastName, StringComparison.OrdinalIgnoreCase);
    //    }

    //    public int GetHashCode([DisallowNull] User obj)
    //    {
    //        if ((obj?.LastName) is null) return 0;

    //        return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.LastName);
    //    }
    //}


}


//public class UserComparer: 