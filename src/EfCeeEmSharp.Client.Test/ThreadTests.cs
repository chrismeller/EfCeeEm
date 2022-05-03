using System;
using System.Linq;
using System.Threading.Tasks;
using EfCeeEmSharp.Client;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace EfCeeEm.Client.Test
{
    public class ThreadTests
    {
        private FourChanClient _client = new();

        [SetUp]
        public void SetUp()
        {
            _client = new();
        }

        [Test]
        public async Task GetThreadList()
        {
            var bizThreadPages = await _client.GetThreadList("biz");

            Assert.IsNotNull(bizThreadPages, "Returned a result");
            Assert.IsNotNull(bizThreadPages.Data, "Returned some pages");
            Assert.Greater(bizThreadPages.Data.Count(), 2, "Returned a reasonable number of pages");

            var firstPage = bizThreadPages.Data.First();

            Assert.IsNotNull(firstPage, "Has a first page of threads");
            Assert.AreEqual(firstPage.Page, 1, "The first page of threads is the first page");
            Assert.IsNotNull(firstPage.Threads, "The first page has a list of threads");
            Assert.IsNotEmpty(firstPage.Threads, "The first page has actual threads");

            var firstThread = firstPage.Threads.First();

            Assert.NotZero(firstThread.Number, "Thread has a real number");
            Assert.Greater(firstThread.LastModified, 957344995, "Thread has a real raw modification date");
            Assert.Greater(firstThread.LastModifiedAt, new DateTimeOffset(2000, 01, 01, 0, 0, 0, TimeSpan.Zero), "Thread has a real parsed modification date");

            Assert.Pass();
        }

        [Test]
        public async Task GetThreadPosts()
        {
            var bizThreadPages = await _client.GetThreadList("biz");
            var firstPage = bizThreadPages.Data.First();

            // try to find a thread that has multiple posts on it - fall back to the first otherwise
            var firstThread = firstPage.Threads.FirstOrDefault(x => x.Replies > 1, firstPage.Threads.First());

            var posts = await _client.GetThreadPosts("biz", firstThread.Number);

            Assert.IsNotNull(posts, "Returned a result");
            Assert.IsNotNull(posts.Data, "Returned some posts");
            Assert.IsNotEmpty(posts.Data, "Returned at least one post (the OP)");

            var firstPost = posts.Data.First();

            Assert.NotZero(firstPost.Number, "OP has a number");
            Assert.IsNotEmpty(firstPost.Comment, "OP comment text");
            Assert.IsNotEmpty(firstPost.PosterName, "OP has poster name");

            Assert.IsNotEmpty(firstPost.Slug, "Has a slug");
            Assert.NotZero(firstPost.Posted, "Posted at is not zero");
            Assert.Greater(firstPost.PostedAt, new DateTimeOffset(2000, 01, 01, 0, 0, 0, TimeSpan.Zero), "Has realistic posted at date");

            if (firstPost.Replies == 0)
            {
                Assert.Pass("Passed with no replies");
                return;
            }

            var secondPost = posts.Data.Skip(1).First();

            Assert.NotZero(secondPost.Number, "Second post has a number");
            Assert.IsNotEmpty(secondPost.Comment, "Second post has a comment");
            Assert.IsNotEmpty(secondPost.PosterName, "Second post has a poster name");

            Assert.AreEqual(secondPost.InReplyTo, firstPost.Number, 0, "Second post is a reply to OP");

            Assert.Pass("Passed with replies");
        }

        [Test]
        public async Task GetThreadPostsFiles()
        {
            var bizThreadPages = await _client.GetThreadList("biz");
            var firstPage = bizThreadPages.Data.First();

            // try to find a thread that has multiple posts on it - fall back to the first otherwise
            var firstThread = firstPage.Threads.FirstOrDefault(x => x.Replies > 1, firstPage.Threads.First());

            var posts = await _client.GetThreadPosts("biz", firstThread.Number);

            // try to find a post that has a file upload
            var postWithUpload = posts.Data.FirstOrDefault(x => x.FileUploaded != null && x.FileUploaded != 0);

            if (postWithUpload == null)
            {
                Assert.Pass("Passed with no file uploads");
                return;
            }

            Assert.IsNotEmpty(postWithUpload.FileUploadExtension, "Has file extension");
            Assert.IsNotEmpty(postWithUpload.FileUploadMd5, "Has file MD5");
            Assert.IsNotNull(postWithUpload.FileUploadWidth, "File width is not null");
            Assert.NotZero(postWithUpload.FileUploadWidth.Value, "File width is > 0");

            Assert.IsNotNull(postWithUpload.FileUploadHeight, "File height is not null");
            Assert.NotZero(postWithUpload.FileUploadHeight.Value, "File height is > 0");

            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}