using System.Text.Json.Serialization;

namespace EfCeeEmSharp.Board.Consumer.DTOs;

public class BoardsResponse
{
    [JsonPropertyName("boards")]
    public List<BoardResponse> Boards { get; set; }
}

public class BoardResponse
{
    [JsonPropertyName("board")] public string Board { get; set; } = null!;

    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("ws_board")] public bool IsWorkSafe { get; set; }

    [JsonPropertyName("meta_description")] public string? Description { get; set; }
}