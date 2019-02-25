using System;

namespace DevGuild.AspNetCore.Services.Logging
{
    public class RequestInformation
    {
        public RequestInformation(String requestHost, String requestMethod, String requestAddress, String userAddress, String userAgent, String userName, String requestId)
        {
            this.RequestHost = requestHost;
            this.RequestMethod = requestMethod;
            this.RequestAddress = requestAddress;
            this.UserAddress = userAddress;
            this.UserAgent = userAgent;
            this.UserName = userName;
            this.RequestId = requestId;
        }

        public String RequestHost { get; }

        public String RequestMethod { get; }

        public String RequestAddress { get; }

        public String UserAddress { get; }

        public String UserAgent { get; }

        public String UserName { get; }

        public String RequestId { get; }
    }
}
