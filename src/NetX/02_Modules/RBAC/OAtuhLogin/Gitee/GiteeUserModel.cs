using Authentication.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.OAtuhLogin
{
    public class GiteeUserModel : UserInfoModelBase
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("blog")]
        public string Blog { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreateAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdateAt { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }

        [JsonProperty("public_gists")]
        public int PublicGists { get; set; }

        [JsonProperty("followers")]
        public int Followers { get; set; }

        [JsonProperty("following")]
        public int Following { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
    }
}
