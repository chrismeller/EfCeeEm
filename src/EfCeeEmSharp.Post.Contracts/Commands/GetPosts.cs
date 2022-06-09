namespace EfCeeEmSharp.Post.Contracts.Commands;

public interface GetPosts
{
    public string Board { get; }
    public long Thread { get; }
}