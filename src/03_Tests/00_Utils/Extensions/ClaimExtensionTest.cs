using NetX.Authentication;
using NetX.Authentication.Core;
using NetX.Authentication.Jwt;
using System.Security.Claims;

namespace NetX.UnitTests._00_Utils.Extensions
{
    public class ClaimExtensionTest
    {
        [Fact]
        public void NullValueToClaimsIsNoData()
        {
            ClaimModel model = null;
            var result = model.ToClaims();
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void ToClaimsIsCorrectLength()
        {
            ClaimModel model = new ClaimModel()
            {
                UserId = "zeke",
                LoginName = "zeke1",
                DisplayName = "zeke@123.com"
            };
            var result = model.ToClaims();
            Assert.True(result.Length == 3);
        }

        [Fact]
        public void ToClaimsIsCorrectContent()
        {
            ClaimModel model = new ClaimModel()
            {
                UserId = "zeke",
                LoginName = "zeke1",
                DisplayName = "zeke@123.com"
            };
            var result = model.ToClaims();
            Assert.True(
                result.FirstOrDefault(p => p.Type.ToLower().Equals("userid")).Value.ToLower() == "zeke"
                && result.FirstOrDefault(p => p.Type.ToLower().Equals("loginname")).Value.ToLower() == "zeke1"
                && result.FirstOrDefault(p => p.Type.ToLower().Equals("displayname")).Value.ToLower() == "zeke@123.com");
        }

        [Fact]
        public void ToClaimModelIsNull()
        {
            List<Claim> claims = null;
            var result = claims.ToClaimModel();
            Assert.Null(result);
        }

        [Fact]
        public void ToClaimModelIsCorrectContent()
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("userid","zeke"),
                new Claim("loginname", "zeke1"),
                new Claim("displayname", "zeke@123.com"),
            };
            var result = claims.ToClaimModel();
            Assert.True(
                result.UserId == "zeke"
                && result.DisplayName == "zeke@123.com"
                && result.LoginName == "zeke1");
        }
    }
}
