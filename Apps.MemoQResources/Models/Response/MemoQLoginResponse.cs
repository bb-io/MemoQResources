using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Models.Response
{
    public class MemoQLoginResponse
    {
        public string Name { get; set; }
        public string Sid { get; set; }
        public string AccessToken { get; set; }
    }
}
