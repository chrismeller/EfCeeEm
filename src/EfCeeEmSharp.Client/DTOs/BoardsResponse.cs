using System.Text.Json.Serialization;

namespace EfCeeEmSharp.Client.DTOs;

public class BoardsResponse
{
    [JsonPropertyName("boards")] public List<Board> Boards { get; set; } = null!;
}

public class Board
{
    [JsonPropertyName("board")] public string Name { get; set; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("ws_board")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsWorkSafe { get; set; }

    [JsonPropertyName("per_page")]
    public int ThreadsPerPage { get; set; }

    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    [JsonPropertyName("is_archived")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsArchived { get; set; }
}