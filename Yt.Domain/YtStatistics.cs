using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt.Models
{
    public class YtStatistics
    {
        public string Id { get; set; }

        public YtVideoDetails Details { get; set; }


        public YtStatistics()
        {
            Details = new YtVideoDetails();
        }

        public ulong? CommentCount
        {
            get;
            set;
        }


        public ulong? DislikeCount
        {
            get;
            set;
        }

        //public ulong? FavoriteCount
        //{
        //    get;
        //    set;
        //}


        public ulong? LikeCount
        {
            get;
            set;
        }


        public ulong? ViewCount
        {
            get;
            set;
        }


    }
}
