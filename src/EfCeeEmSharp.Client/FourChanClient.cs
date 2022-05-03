using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EfCeeEmSharp.Client.DTOs;

namespace EfCeeEmSharp.Client;

public class FourChanClient : IDisposable
{
    private readonly HttpClient _httpClient = new();

    public FourChanClient(string baseUrl = "https://a.4cdn.org/")
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<ApiResponse<IEnumerable<Board>>> GetBoards()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/boards.json");
        var response = await _httpClient.SendAsync(request);

        if (response == null)
        {
            throw new Exception("Empty response!");
        }

        var etag = response.Headers.ETag?.Tag;
        var lastModifiedAt = response.Content.Headers.LastModified;

        var result = await response.Content.ReadFromJsonAsync<BoardsResponse>();

        return new ApiResponse<IEnumerable<Board>>()
        {
            Data = result!.Boards,
            Meta = new ApiResponse<IEnumerable<Board>>.MetaData()
            {
                ETag = etag,
                LastModifiedAt = lastModifiedAt,
            }
        };
    }

    public async Task<bool> GetBoardsIsModified(string? etag = null, DateTimeOffset? lastModified = null)
    {
        return await HasChanged("/boards.json", etag, lastModified);
    }

    public async Task<bool> GetThreadListHasChanged(string board, string? etag = null, DateTimeOffset? lastModified = null)
    {
        return await HasChanged($"/{board}/threads.json", etag, lastModified);
    }

    protected async Task<bool> HasChanged(string uri, string? etag = null, DateTimeOffset? lastModified = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Head, uri);

        if (etag != null) request.Headers.Add("If-None-Match", $"\"{etag.Trim('"')}\"");
        if (lastModified != null) request.Headers.Add("If-Modified-Since", lastModified?.ToString("R"));

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotModified)
        {
            return false;
        }

        return true;
    }

    public async Task<ApiResponse<IEnumerable<ThreadPage>>> GetThreadList(string board)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/{board}/threads.json");
        var response = await _httpClient.SendAsync(request);

        if (response == null)
        {
            throw new Exception("Empty response!");
        }

        var etag = response.Headers.ETag?.Tag;
        var lastModifiedAt = response.Content.Headers.LastModified;

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ThreadPage>>();

        return new ApiResponse<IEnumerable<ThreadPage>>()
        {
            Data = result,
            Meta = new ApiResponse<IEnumerable<ThreadPage>>.MetaData()
            {
                ETag = etag,
                LastModifiedAt = lastModifiedAt,
            }
        };
    }

    public async Task<ApiResponse<IEnumerable<ThreadPost>>> GetThreadPosts(string board, long threadNumber)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/{board}/thread/{threadNumber}.json");
        var response = await _httpClient.SendAsync(request);

        if (response == null)
        {
            throw new Exception("Empty response!");
        }

        var etag = response.Headers.ETag?.Tag;
        var lastModifiedAt = response.Content.Headers.LastModified;

        var result = await response.Content.ReadFromJsonAsync<ThreadPostsResponse>();

        return new ApiResponse<IEnumerable<ThreadPost>>()
        {
            Data = result.Posts,
            Meta = new ApiResponse<IEnumerable<ThreadPost>>.MetaData()
            {
                ETag = etag,
                LastModifiedAt = lastModifiedAt,
            }
        };
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}