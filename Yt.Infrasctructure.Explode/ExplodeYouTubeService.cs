using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using Yt.ApplicationCore;
using Yt.Models;

namespace Yt.Infrasctructure.Explode
{
    public class ExplodeYouTubeService : IFinderYouTubeService
    {
        public async Task<List<YtActivityVideoUploadModel>> GetActivity()
        {
            var youtube = new YoutubeClient();

            var searchCezaryWalenciukOnYouTube = youtube.Search.GetVideosAsync("Cezary Walenciuk");

            List<YtActivityVideoUploadModel> uploadModels =
                new List<YtActivityVideoUploadModel>();


            await foreach (var searchResult in searchCezaryWalenciukOnYouTube)
            {
                var video = await
                    youtube.Videos.GetAsync(searchResult.Url);

                if (video.Author.Title != "Cezary Walenciuk")
                {

                    break;
                }



                YtActivityVideoUploadModel ytActivity =
                    new YtActivityVideoUploadModel();

                ytActivity.ChannelTitle = video.Author.Title;
                ytActivity.ChannelId = video.Author.ChannelId;
                ytActivity.Description = video.Description;
                ytActivity.PublishedAt = video.UploadDate.UtcDateTime;
                ytActivity.PublishedAtRaw = video.UploadDate.ToString();
                ytActivity.UploadVideoId = video.Id;
                ytActivity.Title = video.Title;
                

                ytActivity.Statistics = new YtStatistics();
                ytActivity.Statistics.Id = video.Id;
                ytActivity.Statistics.LikeCount = MapLongToUlong(video.
                    Engagement.LikeCount);
                ytActivity.Statistics.DislikeCount = MapLongToUlong(video.
                    Engagement.DislikeCount);

                ytActivity.Statistics.ViewCount = MapLongToUlong(video.
                    Engagement.ViewCount);

                // Nie ma komentarzy
                //ytActivity.Statistics.CommentCount;
                // Nie ma liczby dodania do ulubionych
                //ytActivity.Statistics.FavoriteCount

                ytActivity.Statistics.Details = new YtVideoDetails();
                ytActivity.Statistics.Details.Id = video.Id;

                if (video.Duration.HasValue)
                    ytActivity.Statistics.Details.Duration =
                        $"{video.Duration.Value.Minutes}" +
                        $"M{video.Duration.Value.Seconds}S";


                ytActivity.Thumbnails = new ThumbnailDetailsModel();


                foreach (var thumb in video.Thumbnails)
                {
                    if (thumb.Resolution.Width >= 1920)
                        ytActivity.Thumbnails.Maxres =
                            Map(thumb);

                    if (thumb.Resolution.Width <= 480)
                        ytActivity.Thumbnails.High =
                            Map(thumb);

                    if (thumb.Resolution.Width <= 336)
                        ytActivity.Thumbnails.Medium =
                            Map(thumb);

                    if (thumb.Resolution.Width <= 320)
                        ytActivity.Thumbnails.Standard =
                            Map(thumb);
                }

                if (ytActivity.Thumbnails.Maxres != null)
                    ytActivity.Thumbnails.Default__ = ytActivity.Thumbnails.Maxres;
                else if (ytActivity.Thumbnails.High != null)
                    ytActivity.Thumbnails.Default__ = ytActivity.Thumbnails.High;
                else if (ytActivity.Thumbnails.Medium != null)
                    ytActivity.Thumbnails.Default__ = ytActivity.Thumbnails.Medium;
                else if (ytActivity.Thumbnails.Standard != null)
                    ytActivity.Thumbnails.Default__ = ytActivity.Thumbnails.Standard;

                uploadModels.Add(ytActivity);

            }

            uploadModels = uploadModels.
                OrderByDescending(a => a.PublishedAt.Value).ToList();

            return uploadModels;
        }
        public static long MapUlongToLong(ulong ulongValue)
        {
            return unchecked((long)ulongValue + long.MinValue);
        }

        public static ThumbnailModel Map(Thumbnail a)
        {
            ThumbnailModel m = new ThumbnailModel();

            m.Url = a.Url;
            m.Height = a.Resolution.Height;
            m.Width = a.Resolution.Width;
            return m;
        }

        public static ulong MapLongToUlong(long longValue)
        {
            return unchecked((ulong)(longValue));
        }

        public Task<List<YtActivityVideoUploadModel>> GetActivity(string ApiKey)
        {
            return GetActivity();
        }
    }


}

