using System;
using System.Collections.Generic;
using System.Linq;
namespace LinqTask;

class BusinessLogic
{
    private List<User> users = new List<User>();
    private List<Record> records = new List<Record>();

    public BusinessLogic()
    {
        InitializeTestData();
    }

    private void InitializeTestData()
    {
        users = new List<User>{new User(1, "John", "Doe"),
            new User(2, "Jane", "Smith"),
            new User(3, "Alice", "Johnson"),
            new User(4, "Bob", "Williams"),
            new User(5, "Charlie", "Brown"),
            new User(6, "Alice", "Davis"),
        };

        records = new List<Record>
        {
            new Record(users[0], "Hello World"),
            new Record(users[1], "Test Message"),
            new Record(users[0], "Another message"),
            new Record(users[2], "Sample text"),
        };
    }

    public List<User> GetUsersBySurname(String surname)
    {
        var query = from u in users
                    where u.Surname == surname
                    select u;
        return query.ToList();
    }

    public User GetUserByID(int id)
    {
        var query = from u in users
                    where u.ID == id
                    select u;
        return query.Single();
    }

    public List<User> GetUsersBySubstring(String substring)
    {
        var query = from u in users
                    where u.Name.Contains(substring) || u.Surname.Contains(substring)
                    select u;
        return query.ToList();
    }

    public List<String> GetAllUniqueNames()
    {
        var query = (from u in users
                    select u.Name).Distinct();
        return query.ToList();
    }

    public List<User> GetAllAuthors()
    {
        var query = from u in users
            join r in records on u.ID equals r.Author.ID
            select u;
        return query.DistinctBy(u => u.ID).ToList();;
    }

    public Dictionary<int, User> GetUsersDictionary()
    {
        return users.ToDictionary(u => u.ID, u => u);
    }

    public int GetMaxID()
    {
        return users.Max(u => u.ID);
    }

    public List<User> GetOrderedUsers()
    {
        var query = from u in users
                    orderby u.ID
                    select u;
        return query.ToList();
    }

    public List<User> GetDescendingOrderedUsers()
    {
        var query = from u in users
                    orderby u.ID descending
                    select u;
        return query.ToList();
    }

    public List<User> GetReversedUsers()
    {
        return users.AsEnumerable().Reverse().ToList();
    }

    public List<User> GetUsersPage(int pageSize, int pageIndex)
    {
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        if (pageIndex <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageIndex));
        int skipCount = pageSize * (pageIndex - 1);
        var query = (from u in users
                orderby u.ID
                select u)
            .Skip(skipCount)
            .Take(pageSize);
        return query.ToList();
    }
}