using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt.Models
{
    public class ThumbnailDetailsModel
    {
        public virtual ThumbnailModel Default__ { get; set; }

        public virtual ThumbnailModel High { get; set; }

        public ThumbnailModel Maxres { get; set; }

        public ThumbnailModel Medium { get; set; }

        public ThumbnailModel Standard { get; set; }

        public string ETag { get; set; }
    }
}
