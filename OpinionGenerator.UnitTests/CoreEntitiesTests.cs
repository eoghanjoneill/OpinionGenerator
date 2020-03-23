using NUnit.Framework;
using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.UnitTests
{
    public class CoreEntitiesTests
    {
        [Test]
        public void Article_TextToAnalyze_Get_SucceedsWithNulls()
        {
            var article = new Article
            {
                Title = "This is the title",
                Description = "And this is the description",
                Content = null
            };
            Assert.AreEqual(article.TextToAnalyze, "This is the title. And this is the description.");

            var article2 = new Article
            {
                Title = "This is the title",
                Description = null,
                Content = "And this is the content"
            };
            Assert.AreEqual(article2.TextToAnalyze, "This is the title. And this is the content.");
        }
    }
}
