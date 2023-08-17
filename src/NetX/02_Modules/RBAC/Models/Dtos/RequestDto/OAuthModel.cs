namespace NetX.RBAC.Models.Dtos.RequestDto
{
    public class OAuthModel
    {
        public OAuthPlatform OAuthPlatform { get; set; }

        public string State { get; set; } = "code";
    }
}
