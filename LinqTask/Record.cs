namespace LinqTask;

class Record
{
    public User Author { get; set; }
    public String Message { get; set; }
    public Record(User author, String message)
    {
        this.Author = author;
        this.Message = message;
    }
}
