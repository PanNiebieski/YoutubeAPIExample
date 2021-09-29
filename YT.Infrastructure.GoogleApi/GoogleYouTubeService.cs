using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yt.ApplicationCore;
using Yt.Models;

namespace YT.Infrastructure.GoogleApi
{
    public class GoogleYouTubeService : IFinderYouTubeService
    {
        public async Task<List<YtActivityVideoUploadModel>> GetActivity(string ApiKey)
        {
            List<YtActivityVideoUploadModel> list =
    new List<YtActivityVideoUploadModel>();

            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = "YouTubeTestAPIGoogle"
            });

            List<string> parametersWhatIWantToGet =
                new List<string>() { "snippet", "contentDetails" };

            var searchListRequest = youtubeService.Activities.List
                (new Repeatable<string>(parametersWhatIWantToGet));

            searchListRequest.ChannelId = "UCaryk7_lKRI1EldZ6saVjBQ";
            searchListRequest.MaxResults = 100;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> ids = new List<string>();

            foreach (var item in searchListResponse.Items)
            {
                if (item.Snippet.Type != "upload")
                    continue;

                var youtubeActivityModel = new YtActivityVideoUploadModel();

                youtubeActivityModel.Title = item.Snippet.Title;
                youtubeActivityModel.Description = item.Snippet.Description;
                youtubeActivityModel.PublishedAt = item.Snippet.PublishedAt;
                youtubeActivityModel.PublishedAtRaw = item.Snippet.PublishedAtRaw;
                youtubeActivityModel.ChannelId = item.Snippet.ChannelId;
                youtubeActivityModel.ChannelTitle = item.Snippet.ChannelTitle;
                //youtubeActivityModel.GroupId = item.Snippet.GroupId;

                youtubeActivityModel.Thumbnails = new ThumbnailDetailsModel()
                {
                    Default__ = MapThumb(item.Snippet.Thumbnails.Default__),
                    High = MapThumb(item.Snippet.Thumbnails.High),
                    Maxres = MapThumb(item.Snippet.Thumbnails.Maxres),
                    Medium = MapThumb(item.Snippet.Thumbnails.Medium),
                    Standard = MapThumb(item.Snippet.Thumbnails.Standard),
                    ETag = item.Snippet.Thumbnails.ETag,
                };

                youtubeActivityModel.Type = item.Snippet.Type;
                //youtubeActivityModel.ETag = item.Snippet.ETag;
                youtubeActivityModel.UploadVideoId = item.ContentDetails?.Upload?.VideoId;

                if (youtubeActivityModel.UploadVideoId != null)
                    ids.Add(youtubeActivityModel.UploadVideoId);

                list.Add(youtubeActivityModel);

            }

            var stats = await GetStatistics(ids.ToArray(),youtubeService);

            foreach (var activity in list)
            {
                foreach (var stat in stats)
                {
                    if (activity.UploadVideoId == stat.Id)
                    {
                        activity.Statistics = stat;
                    }
                }
            }

            return list;
        }

        private async Task<List<YtStatistics>> GetStatistics(string[] ids, YouTubeService youtubeService)
        {
            List<string> parametersWhatIWantToGet = new List<string>()
            { "snippet", "contentDetails", "statistics" };

            var searchListRequest = youtubeService.Videos.List
                (new Repeatable<string>(parametersWhatIWantToGet));

            searchListRequest.Id = string.Join(",", ids);
            searchListRequest.MaxResults = 35;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<YtStatistics> listStat =
                new List<YtStatistics>();

            foreach (var item in searchListResponse.Items)
            {
                var YtStatisticsModel = new YtStatistics();
                YtStatisticsModel.Id = item.Id;
                YtStatisticsModel.DislikeCount = item.Statistics.DislikeCount;
                YtStatisticsModel.LikeCount = item.Statistics.LikeCount;
                YtStatisticsModel.CommentCount = item.Statistics.CommentCount;
                //YtStatisticsModel.FavoriteCount = item.Statistics.FavoriteCount;
                YtStatisticsModel.ViewCount = item.Statistics.ViewCount;

                var YtVideoDetails = new YtVideoDetails();
                YtVideoDetails.Duration = item.ContentDetails.Duration;
                YtVideoDetails.HasCustomThumbnail = item.ContentDetails.HasCustomThumbnail;
                YtVideoDetails.Definition = item.ContentDetails.Definition;
                YtVideoDetails.Dimension = item.ContentDetails.Dimension;
                YtVideoDetails.Projection = item.ContentDetails.Projection;
                YtVideoDetails.LicensedContent = item.ContentDetails.LicensedContent;
                listStat.Add(YtStatisticsModel);
            }
            return listStat;
        }

        private ThumbnailModel MapThumb(Thumbnail thumbnail)
        {
            if (thumbnail == null)
                return null;


            ThumbnailModel thumbnailModel = new ThumbnailModel();

            thumbnailModel.Height = thumbnail.Height;
            thumbnailModel.Url = thumbnail.Url;
            thumbnailModel.Width = thumbnail.Width;
            thumbnailModel.ETag = thumbnail.ETag;

            return thumbnailModel;

        }


        public Task<List<YtActivityVideoUploadModel>> GetActivity()
        {
            throw new NotImplementedException("Api Key is needed");
        }
    }
}
