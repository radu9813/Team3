using Moq;
using System;
using System.IO;
using Team3Assig.Controllers;
using Team3Assig.Data;
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
            ApplicationDbContext applicationDbContext = null;
            DiplomataController diplomataController = new DiplomataController(applicationDbContext, diplomataControllerSettingsMock.Object);
            //Act
            var result = diplomataController.CreateArticlesRecordFromJson(content);
            string[] authors = { "Lin, Qinwei", "Li, Chao", "Zhao, Xifeng", "Chen, Xianhai" };
            string downloadUrl = "http://arxiv.org/abs/2101.10699";

            //Assert
            Assert.Equal(authors, result[0].Authors);
            Assert.Equal(downloadUrl, result[0].DownloadUrl);        
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
