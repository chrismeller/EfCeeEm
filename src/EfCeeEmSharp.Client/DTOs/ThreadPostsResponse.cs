using System.Text.Json.Serialization;

namespace EfCeeEmSharp.Client.DTOs;

public class ThreadPostsResponse
{
    public IEnumerable<ThreadPost> Posts { get; set; }
}

public class ThreadPost
{
    [JsonPropertyName("no")]
    public long Number { get; set; }

    [JsonPropertyName("resto")]
    public long InReplyTo { get; set; }

    [JsonPropertyName("name")]
    public string? PosterName { get; set; }

    [JsonPropertyName("com")]
    public string? Comment { get; set; }

    [JsonPropertyName("sub")]
    public string? Subject { get; set; }

    [JsonPropertyName("time")]
    public long Posted { get; set; }

    public DateTimeOffset PostedAt => DateTimeOffset.FromUnixTimeSeconds(Posted);

    [JsonPropertyName("tim")]
    public long? FileUploaded { get; set; }

    public DateTimeOffset? FileUploadedAt =>
        (FileUploaded != null) ? DateTimeOffset.FromUnixTimeMilliseconds(FileUploaded.Value) : null;

    [JsonPropertyName("ext")]
    public string? FileUploadExtension { get; set; }

    [JsonPropertyName("fsize")]
    public long? FileUploadSize { get; set; }

    [JsonPropertyName("md5")]
    public string? FileUploadMd5 { get; set; }

    [JsonPropertyName("w")]
    public long? FileUploadWidth { get; set; }

    [JsonPropertyName("h")]
    public long? FileUploadHeight { get; set; }

    [JsonPropertyName("tn_w")]
    public long? ThumbnailWidth { get; set; }

    [JsonPropertyName("tn_h")]
    public long? ThumbnailHeight { get; set; }

    [JsonPropertyName("filedeleted")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsFileDeleted { get; set; }

    [JsonPropertyName("spoiler")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsFileASpoiler { get; set; }

    [JsonPropertyName("custom_spoiler")]
    public int? CustomSpoilerId { get; set; }

    [JsonPropertyName("sticky")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsSticky { get; set; }

    [JsonPropertyName("closed")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsClosed { get; set; }

    [JsonPropertyName("trip")]
    public string? TripCode { get; set; }

    [JsonPropertyName("capcode")]
    public string? CapCode { get; set; }

    [JsonPropertyName("country")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("country_name")]
    public string? CountryName { get; set; }

    [JsonPropertyName("board_flag")]
    public string? PosterBoardFlag { get; set; }

    [JsonPropertyName("flag_name")]
    public string? PosterBoardFlagName { get; set; }

    [JsonPropertyName("replies")]
    public long? Replies { get; set; }

    [JsonPropertyName("images")]
    public long? ImageReplies { get; set; }

    [JsonPropertyName("bumplimit")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsBumpLimitReached { get; set; }

    [JsonPropertyName("imagelimit")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsImageLimitReached { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("semantic_url")]
    public string? Slug { get; set; }

    [JsonPropertyName("m_img")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsMobileOptimizedImageAvailable { get; set; }

    [JsonPropertyName("archived")]
    [JsonConverter(typeof(BoolConverter))]
    public bool IsThreadArchived { get; set; }

    [JsonPropertyName("archived_on")]
    public long? ThreadArchived { get; set; }

    public DateTimeOffset? ThreadArchivedAt =>
        (ThreadArchived != null) ? DateTimeOffset.FromUnixTimeSeconds(ThreadArchived.Value) : null;

    [JsonPropertyName("unique_ips")]
    public int? UniqueIpsInThread { get; set; }


}