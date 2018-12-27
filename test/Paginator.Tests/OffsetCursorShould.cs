using Bogus;
using Paginator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paginator.Tests
{
    public class OffsetCursorShould
    {
        private readonly IEnumerable<Client> _clients;

        public OffsetCursorShould()
        {
            int clientIds = 0;

            Faker<Client> _clientFaker = new Faker<Client>()
                .RuleFor(c => c.Id, _ => clientIds++)
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Name, (_, c) => $"{c.FirstName} {c.LastName}")
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                .RuleFor(c => c.DateJoined, f => f.Date.Past(5));

            _clients = _clientFaker.Generate(100);
        }

        [Fact]
        public void TakeCorrectFirstItem()
        {
            var cursor = new OffsetCursor<Client>(0);
            var options = new OffsetCursorOptions(10);

            var results = cursor.ApplyCursor(_clients.AsQueryable(), options);

            Assert.Equal(results.First().Id, _clients.First().Id);
        }

        [Fact]
        public void CreateACursorForNextPage()
        {
            var cursor = new OffsetCursor<Client>(offset: 0);
            var options = new OffsetCursorOptions(limit: 1);

            var firstResults = cursor.ApplyCursor(_clients.AsQueryable(), options);

            var nextCursor = new OffsetCursor<Client>(firstResults.Metadata.NextCursor);
            var secondResults = nextCursor.ApplyCursor(_clients.AsQueryable(), options);



            Assert.Equal(secondResults.First().Id, _clients.Skip(1).First().Id);
        }

        [Fact]
        public void BeAbleToLoopThroughAllItems()
        {
            var count = 0;
            var nextCursor = "";
            var options = new OffsetCursorOptions(limit: 1);

            do
            {
                var cursor = new OffsetCursor<Client>(nextCursor);
                var results = cursor.ApplyCursor(_clients.AsQueryable(), options);

                nextCursor = results.Metadata.NextCursor;
                count++;
            } while (!string.IsNullOrEmpty(nextCursor));

            Assert.Equal(count, _clients.Count() + 1);

        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
