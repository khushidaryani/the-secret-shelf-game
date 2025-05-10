[System.Serializable]
public class Book
{
    public string ISBN;
    public string title;
    public string author;
    public string genre;
    public string[] hints;
}

[System.Serializable]
public class BookListWrapper
{
    public Book[] books;
}
