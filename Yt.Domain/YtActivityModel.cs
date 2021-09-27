using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt.Models
{
    public class YtActivityModel
    {

        public string ChannelId
        {
            get;
            set;
        }

        public string ChannelTitle
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }


        public string PublishedAtRaw
        {
            get;
            set;
        }


        public DateTime? PublishedAt { get; set; }


        public ThumbnailDetailsModel Thumbnails
        {
            get;
            set;
        }


        public string Title
        {
            get;
            set;
        }


        public string Type
        {
            get;
            set;
        }




    }
}
