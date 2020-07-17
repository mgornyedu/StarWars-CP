using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using StarWars.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Transactions;

namespace StarWars.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly TestServer server;
        protected readonly HttpClient client;
        protected readonly TransactionScope transactionScope;
        protected TestBase()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>());
            client = server.CreateClient();
            transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions(), TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            transactionScope.Dispose();
        }
    }
}
