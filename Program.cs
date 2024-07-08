
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        // Sample Collections to practise
        IEnumerable<int> collection = [1, 2, 3, 4, 5];
        IEnumerable<List<int>> multiCollection = [[1, 2, 3], [4, 5]];
        IEnumerable<Object> multiTypeCollection = [1, 2, "hello", 4, 5];
        IEnumerable<Object> objectTypeInt = [1, 2, 3, 4, 5];
        IEnumerable<int> emptyCollection = [];
        
        List<Customer> customers =
        [
            new() { Id = 1, Name = "Alice", Age = 30, City = "New York" },
            new() { Id = 2, Name = "Bob", Age = 30, City = "Los Angeles" },
            new() { Id = 3, Name = "Charlie", Age = 25, City = "Chicago" },
            new() { Id = 4, Name = "David", Age = 25, City = "San Francisco" },
            new() { Id = 5, Name = "Eve", Age = 28, City = "New York" }
        ];

        collection.Dump();

        var dict = new Dictionary<int, int>
        {
            { 1, 2 }
        };

        dict.Dump();

        // ****   1. Filtering ******  Where and OfType
        // Where extension method
        IEnumerable<int> filteredCollection = collection.Where(a => a >= 3);
        filteredCollection.Dump();

        IEnumerable<Customer> filteredCustomers = customers.Where(cust => cust.Age == 30);
        filteredCustomers.Dump(); 

        // OfType : retuen values of specified type
        multiTypeCollection.OfType<string>().Dump();  // return "hello"
        multiTypeCollection.OfType<int>().Dump();  // return 1,2,4,5


        // **** 2. Partitioning ******  Skip , Take,  SkipLast, TakeLast, SkipWhile, TakeWhile

        // Skip
        collection.Skip(3).Dump();  // skip 3 values and dump 4 ,5 
        // Take
        collection.Take(3).Dump();  // take 3 value - 1,2,3
        // SkipLast
        collection.SkipLast(2).Dump(); // 1,2,3 skips last 2 values
        // SkipWhile
        collection.SkipWhile(a => a < 3).Dump();  // skips while a specified condition is true : 3,4,5
        // Take While
        collection.TakeWhile(a => a < 3).Dump();  // takes while a specified condition is true : 1,2



        // **** 3. Projecting ******  Select (with index) , SelectMany (with index), Cast, Chunk

        // selecting and projecting to different type

        collection.Select(a => a.ToString()).Dump();
        collection.Select((a, idx) => $"index: {idx} : Value {a}").Dump();

        // SlectMany - when we have nested collection and we want to flaten it

        multiCollection.SelectMany(a => a).Dump();  // 1,2,3,4,5

        // if we want to convert to string , we can't directly do that as it is interally reading the list one by one so,
        multiCollection.SelectMany(a => a)
        .Select(x => x.ToString())
        .Dump();  // 1,2,3,4,5 in string format

        // Or

        multiCollection.SelectMany(a => a.Select(x => x.ToString())).Dump();

        // selctmany with index

        multiCollection.SelectMany((a, idx) => a.Select(x => $"index: {idx} values {x.ToString()}")).Dump();

        // cast - one type to another type , if it castable . like object to int is poosible but int -> string is not

        objectTypeInt.Cast<int>().Dump();

        // Chunk: Kinda opposite to selectMany

        collection.Chunk(2).Dump();  // create seperate lists of chunk size 2


         // **** 4. Existence or Quantity Check ******  ANy, All, Contains

        collection.Any(a => a > 3).Dump(); // return True as predicate is true

        collection.All(a => a > 100).Dump(); // return False, as it will check for all the element met the predicate condition

        collection.Contains(2).Dump(); // check if collection contains the specified value : True
        collection.Contains(500).Dump(); // return False


        // **** 5. Sequence Manipulation ******  Append and Prepand

        collection.Append(6).Dump();
        collection.Prepend(0).Dump();

        // **** 6. Agreegationn ******  Count, TryGetNonEnumeratedCount, MAx, MaxBy, Min, MinBy, Sum, Average, Aggregate, LongCount, 

        collection.Count().Dump();

        collection.Where(a => a > 2)
        .Count()
        .Dump();

        customers.TryGetNonEnumeratedCount(out int count).Dump();
        customers.Where(a => a.Age > 40).TryGetNonEnumeratedCount(out int enumeratedValue).Dump();

        collection.Max().Dump();
        collection.MaxBy(a => a * -1).Dump();

        // longCOunt same as count , diffr is it return long type
        // [1,2,3,4,5] => (1,2) => (3 (from previous iteration x + y), 3) => (6, 4) => (10, 5) => 15
        collection.Aggregate((x, y) => x + y).Dump();   // 15 

        collection.Aggregate(10, (x, y) => x+y).Dump(); // 10 being the seed value

        collection.Aggregate(10, (x, y) => x+y, x => (float)x/3).Dump(); // manipulating final result

         // **** 6. Element Operator ******  First, FirstOrDefault, Last LastOrDefault, Single, 
         // SingleOrDefault, ElementAt or ElementAtOrDefault, DefaulfIfEmpty


        // emptyCollection.First().Dump(); // throw InvalidOperationException as the collection is empty
        emptyCollection.FirstOrDefault().Dump(); // return default value of underlying type
        emptyCollection.FirstOrDefault(-1).Dump(); // we can specift default in this case -1

        // Single accepts only single value in your collection, it throws invalidOperationException if collection has
        // more than one element

        // SingleOrDefault still throws an error if there are more than one element in collection 
        // but if your collection is empty then it will throw the default value of underlying type.

        // ElementAt specific index : if does not exist IndexOutOfRange
        // ElementAtOrDefault: instead of throwing error it will return default value

        // If Collection is Empty it will add a default value to collection

        // **** 7. Method ****** ToList, ToArray, ToDictionary, ToHashSet, ToLookUp, 

        collection.ToDictionary(k => k, v=> v).Dump();

        customers.ToLookup(cust => cust.Age).Dump();

        customers.ToLookup(cust => cust.Age)[30].Dump();  // grouping of 30

        customers.ToLookup(cust => cust.Age)[28].Single().Name.Dump();

        // **** 7. Generation Method ****** AsEnumerable, AsQueryable, Range, Repeat, Empty

        IEnumerable<Customer> IECust = customers.AsEnumerable();   // convert to Enumrable
        IQueryable<Customer> IQCust = customers.AsQueryable();   // convert to Queryable

        IEnumerable<int> ints = Enumerable.Range(1, 10);
        ints.Dump();

        IEnumerable<int> repeat = Enumerable.Repeat(1, 10);
        repeat.Dump();

        IEnumerable<int> empty = Enumerable.Empty<int>();   // every time you call it in application you are referencing the same collection as it is a static method
        empty.Dump();


        // **** 7. Set Operations ****** Distinct, DistinctBy, Union,UnionBy,  Intersect, IntersectBy,  
        // Except, ExceptBy, SequenceEqual

        // SequenceEqual : Checks if two sequences are equal

        // **** 8. Joining and Grouping ****** Zip, Join, GroupJoin, Concat, GroupBy

         // **** 9. Sorting****** Order, OrderBy, OrderDescending, OrderByDescending, ThenBy, ThenByDescending, Reverse

         Enumerable.Range(0,2).Dump();


    }
}


public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Age: {Age}, City: {City}";
    }
}


