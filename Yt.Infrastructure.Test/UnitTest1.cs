using Xunit;
using Yt.Infrasctructure.Explode;

namespace Yt.Infrastructure.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ExplodeYouTubeService finderYouTubeService= new ExplodeYouTubeService();

            var test = finderYouTubeService.GetActivity();
        }
    }
}