using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt.Models
{
    public class YtActivityVideoUploadModel : YtActivityModel
    {
        public string UploadVideoId
        {
            get;
            set;
        }

        public string YouTubeUrl
        {
            get
            {
                return "https://www.youtube.com/watch?v=" + UploadVideoId;
            }
        }

        public string DescriptionCut
        {
            get
            {
                int index = Description.IndexOf("[end]");
                string end = Description;

                if (index > 0)
                    end = Description.Substring(0, index);


                end = end.Replace(".", ".<br />");
                end = end.Replace(".<br />NET", ".NET");
                end = end.Replace("global.<br />asax", "global.asax");
                end = end.Replace("ASP.<br />NET", "ASP.NET");
                end = end.Replace(".<br />", ".<br /><br />");
                end = end.Replace("-", "<br />-");
                return end;
            }
        }

        public bool IsThisLiveStreamVideo
        {
            get
            {
                int index = Description.IndexOf("[end]");
                if (index > 0)
                    return false;
                else
                    return true;
            }
        }

        public bool Visible
        {
            get
            {
                int index = Description.IndexOf("[visnot]");

                return !(index != -1);
            }
        }

        public string Category
        {
            get
            {
                int index = Description.IndexOf("[cat:");
                string end = Description;

                if (index > 0)
                    end = Description.Substring(index);
                else
                    return "";

                int indexEnd = end.IndexOf("]");

                try
                {
                    if (indexEnd > 0)
                    {
                        string cat = end.Substring(5, indexEnd - 5);
                        return cat.ToUpper();
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }


                return "";

            }
        }

        public YtStatistics Statistics { get; set; }

        public YtActivityVideoUploadModel()
        {

        }
    }
}
