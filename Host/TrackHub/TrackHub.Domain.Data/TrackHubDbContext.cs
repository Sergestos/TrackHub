using Cassandra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TrackHub.Domain.Data;

public sealed class TrackHubDbContext : IDisposable
{



    public TrackHubDbContext()
    {

        ;

    }



    public async Task SeedAsync()
    {

        var createKeyspace = await _session.PrepareAsync("CREATE KEYSPACE IF NOT EXISTS cosmicworks WITH replication = {'class':'basicclass', 'replication_factor': 1};");
        await _session.ExecuteAsync(createKeyspace.Bind());

        var createTable = await _session.PrepareAsync("CREATE TABLE IF NOT EXISTS cosmicworks.products (id text PRIMARY KEY, name text)");
        await _session.ExecuteAsync(createTable.Bind());

        var item = new
        {
            id = "68719518371",
            name = "Kiama classic surfboard"
        };

        var createItem = await _session.PrepareAsync("INSERT INTO cosmicworks.products (id, name) VALUES (?, ?)");

        var createItemStatement = createItem.Bind(item.id, item.name);

        await _session.ExecuteAsync(createItemStatement);
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}
