using System;
using System.Linq;
using System.Threading.Tasks;
using EfCeeEmSharp.Client;
using NUnit.Framework;

namespace EfCeeEm.Client.Test
{
    public class BoardTests
    {
        private FourChanClient _client = new();

        [SetUp]
        public void SetUp()
        {
            _client = new();
        }

        [Test]
        public async Task GetBoards()
        {
            var boards = await _client.GetBoards();

            Assert.IsNotEmpty(boards.Data, "Returned some boards");
            Assert.Greater(boards.Data.Count(), 20, "Has a reasonable number of boards");
            Assert.True(boards.Data.Any(x => x.Name == "b"), "Contains the b board");

            var a = boards.Data.First(x => x.Name == "a");

            Assert.True(a.IsWorkSafe, "A is SFW");
            Assert.True(a.IsArchived, "A is archived");

            var b = boards.Data.First(x => x.Name == "b");

            Assert.False(b.IsWorkSafe, "B is clearly not SFW");
            Assert.False(b.IsArchived, "B is not archived");
            Assert.Greater(b.ThreadsPerPage, 0, "B has threads per page > 0");
            Assert.Greater(b.Pages, 0, "B has pages > 0");
            Assert.AreEqual("Random", b.Title, "B's title is correct");

            Assert.Pass();
        }

        [Test]
        public async Task GetBoardsMeta()
        {
            var boards = await _client.GetBoards();

            Assert.IsNotNull(boards.Meta);
            Assert.IsNotNull(boards.Meta.ETag);
            Assert.IsNotNull(boards.Meta.LastModifiedAt);
        }

        [Test]
        public async Task GetBoardsIsModifiedDates()
        {
            // at present, they were modified on june 16, 2021
            var oldDate = new DateTimeOffset(2020, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var newDate = DateTimeOffset.UtcNow;

            var oldModified = await _client.GetBoardsIsModified(null, oldDate);
            var newModified = await _client.GetBoardsIsModified(null, newDate);

            Assert.True(oldModified);
            Assert.False(newModified);
        }

        [Test]
        public async Task GetBoardsIsModifiedETags()
        {
            var invalidEtag = "\"asdfasdf\"";
            var invalidEtagWithoutQuotes = "lksdjflaksdf";

            var boards = await _client.GetBoards();
            var currentEtag = boards.Meta.ETag;

            var invalidResult = await _client.GetBoardsIsModified(invalidEtag);
            var invalidResultWithoutQuotes = await _client.GetBoardsIsModified(invalidEtagWithoutQuotes);

            var currentResult = await _client.GetBoardsIsModified(currentEtag);

            Assert.True(invalidResult, "Invalid ETag should return as modified");
            Assert.True(invalidResultWithoutQuotes, "Invalid ETag without quotes should return as modified");
            Assert.False(currentResult, "Current ETag should return not modified");
        }

        [Test]
        public async Task GetBoardsIsModifiedCombos()
        {
            // at present, they were modified on june 16, 2021
            var oldDate = new DateTimeOffset(2020, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var newDate = DateTimeOffset.UtcNow;

            var oldModified = await _client.GetBoardsIsModified(null, oldDate);
            var newModified = await _client.GetBoardsIsModified(null, newDate);

            Assert.True(oldModified);
            Assert.False(newModified);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}