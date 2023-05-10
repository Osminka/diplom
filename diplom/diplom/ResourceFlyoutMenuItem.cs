using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom
{
    public class ResourceFlyoutMenuItem
    {
        public ResourceFlyoutMenuItem()
        {
            TargetType = typeof(ResourceFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}