using StreamList = pili_sdk_csharp.pili.Stream.StreamList;
using Credentials = pili_sdk_csharp.pili_qiniu.Credentials;
using MessageConfig = pili_sdk_csharp.pili_common.MessageConfig;


namespace pili_sdk_csharp.pili
{

    public class Hub
    {

        #region 构造方法
        private Credentials mCredentials;
        private string mHubName;
        public Hub(Credentials credentials, string hubName)
        {
            if (hubName == null)
            {
                throw new System.ArgumentException(MessageConfig.NULL_HUBNAME_EXCEPTION_MSG);
            }
            if (credentials == null)
            {
                throw new System.ArgumentException(MessageConfig.NULL_CREDENTIALS_EXCEPTION_MSG);
            }
            mCredentials = credentials;
            mHubName = hubName;
        }
        public Hub(string accessKey, string secretKey, string hubName)
        {
            mCredentials = new Credentials(accessKey, secretKey);
            mHubName = hubName;
        }

        public static Hub Create(string accessKey, string secretKey, string hubName)
        {
            Hub hub = new Hub(accessKey, secretKey, hubName);
            return hub;
        }
        #endregion

        public virtual Stream createStream()
        {
            return API.createStream(mCredentials, mHubName, null, null, null);
        }

        public virtual Stream createStream(string title, string publishKey, string publishSecurity)
        {
            return API.createStream(mCredentials, mHubName, title, publishKey, publishSecurity);
        }

        public virtual Stream getStream(string streamId)
        {
            return API.getStream(mCredentials, streamId);
        }


        public virtual StreamList listStreams()
        {
            return API.listStreams(mCredentials, mHubName, null, 0, null);
        }


        public virtual StreamList listStreams(string marker, long limit)
        {
            return API.listStreams(mCredentials, mHubName, marker, limit, null);
        }


        public virtual StreamList listStreams(string marker, long limit, string titlePrefix)
        {
            return API.listStreams(mCredentials, mHubName, marker, limit, titlePrefix);
        }

    }

}