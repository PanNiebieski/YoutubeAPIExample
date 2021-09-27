

using System.Reflection;
using YT.Infrastructure.GoogleApi;

GoogleYouTubeService finderYouTubeService = new GoogleYouTubeService();

var test = await finderYouTubeService.GetActivity(GetApiKey());


foreach (var activity in test)
{
    Console.WriteLine(
    $"\n{activity.Title}  {activity.Statistics.LikeCount} \n");
    Console.WriteLine(
        $"{activity.PublishedAt}  {activity.YouTubeUrl} \n {activity.ChannelTitle}");
}


string GetApiKey()
{
    var assembly = Assembly.GetExecutingAssembly();
    var resourceName = "Yt.Infrastructure.GoogleApi.ConsoleTest.ApiKey.txt";

    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new StreamReader(stream))
    {
        string result = reader.ReadToEnd();
        //result = EncryptTool.Decrypt(result);
        return result;
    }
}