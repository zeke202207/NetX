namespace NetX.RBAC.Models.Dtos.RequestDto
{
    public class OAuthLoginModel
    {
        public string Code { get; set; }

        public string State { get; set; } = "code";
    }
}
