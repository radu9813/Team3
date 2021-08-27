using Moq;
using System;
using System.IO;
using Team3Assig.Controllers;
using Team3Assig.Data;
using Team3Assig.Services;
using Xunit;

namespace TestProject
{
    public class RequestTests
    {
        [Fact]
        public void ArticleResponseTest()
        {
             //Assume
            string content = LoadJsonFromResource();
            var diplomataControllerSettingsMock = new Mock<IDiplomataControllerSettings>();
            var broadcastServiceMock = new Mock<IBroadcastService>();
            ApplicationDbContext applicationDbContext = null;
            DiplomataController diplomataController = new DiplomataController(applicationDbContext, diplomataControllerSettingsMock.Object, broadcastServiceMock.Object);
            //Act
            var result = diplomataController.CreateArticlesRecordFromJson(content);
            string[] authors = { "Lin, Qinwei", "Li, Chao", "Zhao, Xifeng", "Chen, Xianhai" };
            string downloadUrl = "http://arxiv.org/abs/2101.10699";
            string title = "Measuring Decentralization in Bitcoin and Ethereum using Multiple\n  Metrics and Granularities";
            string[] topics = { "Computer Science - Cryptography and Security", "Computer Science - Databases" };

            //Assert
            Assert.Equal(authors, result[0].Authors);
            Assert.Equal(downloadUrl, result[0].DownloadUrl);
            Assert.Equal(title, result[0].Title);
            Assert.Equal(topics, result[0].Topics);
        }

        private string LoadJsonFromResource()
        {
            var resourceName = "TestProject.Article.json";
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }

        }

    }
}
