using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGeneratorTests
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("85264f1f-3446-40df-bdd1-e17356e84d2d")
                .AddEnvironmentVariables()
                .Build();
        }

        public static OpinionGeneratorConfiguration GetApplicationConfiguration(string outputPath)
        {
            var configuration = new OpinionGeneratorConfiguration();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig.Bind(configuration);

            return configuration;
        }

        public class OpinionGeneratorConfiguration
        {
            public AzTextAnalyticsConfiguration AzTextAnalytics { get; set; } = new AzTextAnalyticsConfiguration();
            public string NewsAPIKey { get; set; }
            public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
        }

        public class AzTextAnalyticsConfiguration
        {
            public string Key { get; set; }
            public string Url { get; set; }
        }
        public class ConnectionStrings
        {
            public string OpinionGenerator { get; set; }
        }

    }
}
