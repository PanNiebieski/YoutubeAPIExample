using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt.Models
{
    public class YtVideoDetails
    {
        public string Id { get; set; }

        public string Definition
        {
            get;
            set;
        }


        public string Dimension
        {
            get;
            set;
        }


        public string Duration
        {
            get;
            set;
        }


        public bool? HasCustomThumbnail
        {
            get;
            set;
        }


        public bool? LicensedContent
        {
            get;
            set;
        }

        public string Projection
        {
            get;
            set;
        }

    }
}
