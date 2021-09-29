using Yt.Infrasctructure.Explode;

ExplodeYouTubeService finderYouTubeService = new ExplodeYouTubeService();

var test = await finderYouTubeService.GetActivity();


foreach (var activity in test)
{
    Console.WriteLine(
    $"\n{activity.Title}  {activity.Statistics.LikeCount} \n");
    Console.WriteLine(
        $"{activity.PublishedAt}  " +
        $"{activity.YouTubeUrl} \n {activity.ChannelTitle}");
}
