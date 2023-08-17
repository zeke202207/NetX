namespace NetX.Authentication.Core;

/// <summary>
///  登录处理接口
/// </summary>
public interface ILoginHandler
{
    /// <summary>
    /// 登录处理
    /// </summary>
    /// <param name="claimModel">信息</param>
    /// <param name="extendData">扩展数据</param>
    /// <returns></returns>
    dynamic Handle(ClaimModel claimModel, string extendData);
}
