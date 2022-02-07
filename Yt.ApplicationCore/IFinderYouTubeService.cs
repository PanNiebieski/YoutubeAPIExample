using System.Collections.Generic;
using System.Threading.Tasks;
using Yt.Models;

namespace Yt.ApplicationCore
{
    public interface IFinderYouTubeService
    {
       Task<List<YtActivityVideoUploadModel>> GetActivity();

       Task<List<YtActivityVideoUploadModel>> GetActivity(string ApiKey);
    }
}