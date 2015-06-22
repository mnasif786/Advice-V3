using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Nest;
using System.Configuration;

namespace Advice.Logging.Implementations
{
    internal class ElkLog : IElkLog
    {        
        private static Uri ElkUrl { get { return new Uri(url); } }
        private readonly Type _type;
        private readonly ElasticClient _elkClient;
        private static readonly bool IsLoggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLoggingEnabled"]);
        private static readonly string url = ConfigurationManager.AppSettings["ElasticSearchURL"];

        public ElkLog(Type type)
        {
            _type = type;
            var elkSettings = new ConnectionSettings(ElkUrl);
            _elkClient = new ElasticClient(elkSettings);
        }
       
        public void LogException(ExceptionLogRequest exceptionLogRequest, 
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                if (IsLoggingEnabled)
                {

                    string index = ElkIndexTypeProvider.ExceptionsException.Index;
                    string type = ElkIndexTypeProvider.ExceptionsException.Type;
                    var exception = exceptionLogRequest.Exception;
                    var requestServerVariables = exceptionLogRequest.ServerVariables;
                    ServerVariables serverVariables = null;
                    string userFromServerVariables = string.Empty;
                    string hostNameFromServerVariables = string.Empty;
                    if (requestServerVariables != null)
                    {
                        serverVariables = new ServerVariables()
                        {
                            AllHttp = requestServerVariables["ALL_HTTP"],
                            AllRaw = requestServerVariables["ALL_RAW"],
                            ApplMdPath = requestServerVariables["APPL_MD_PATH"],
                            ApplPhysicalPath = requestServerVariables["APPL_PHYSICAL_PATH"],
                            AuthPassword = requestServerVariables["AUTH_PASSWORD"],

                            AuthType = requestServerVariables["AUTH_TYPE"],
                            AuthUser = requestServerVariables["AUTH_USER"],
                            CertificateCookie = requestServerVariables["CERT_COOKIE"],
                            CertificateFlags = requestServerVariables["CERT_FLAGS"],
                            CertificateIssuer = requestServerVariables["CERT_ISSUER"],
                            CertificateKeySize = requestServerVariables["CERT_KEYSIZE"],
                            CertificateSecretKeySize = requestServerVariables["CERT_SECRETKEYSIZE"],
                            CertificateSerialNumber = requestServerVariables["CERT_SERIALNUMBER"],

                            CertificateServerIssuer = requestServerVariables["CERT_SERVER_ISSUER"],
                            CertificateServerSubject = requestServerVariables["CERT_SERVER_SUBJECT"],
                            CertificateSubject = requestServerVariables["CERT_SUBJECT"],
                            ContentLength = Convert.ToInt32(requestServerVariables["CONTENT_LENGTH"]),
                            ContentType = requestServerVariables["CONTENT_TYPE"],
                            GateWayInterface = requestServerVariables["GATEWAY_INTERFACE"],
                            HttpAccept = requestServerVariables["HTTP_ACCEPT"],
                            HttpAcceptEncoding = requestServerVariables["HTTP_ACCEPT_ENCODING"],
                            HttpAcceptLanguage = requestServerVariables["HTTP_ACCEPT_LANGUAGE"],
                            HttpConnection = requestServerVariables["HTTP_CONNECTION"],
                            HttpCookie = requestServerVariables["HTTP_COOKIE"],
                            HttpHost = requestServerVariables["HTTP_HOST"],
                            HttpReferer = requestServerVariables["HTTP_REFERER"],

                            HttpUserAgent = requestServerVariables["HTTP_USER_AGENT"],
                            Https = requestServerVariables["HTTPS"],
                            HttpsKeySize = requestServerVariables["HTTPS_KEYSIZE"],
                            HttpSecretKeys = requestServerVariables["HTTPS_SECRETKEYSIZE"],
                            HttpsServerIssuer = requestServerVariables["HTTPS_SERVER_ISSUER"],
                            HttpsServerSubject = requestServerVariables["HTTPS_SERVER_SUBJECT"],
                            InstanceId = requestServerVariables["INSTANCE_ID"],
                            InstanceMetaPath = requestServerVariables["INSTANCE_META_PATH"],
                            LocalAddr = requestServerVariables["LOCAL_ADDR"],
                            LogonUser = requestServerVariables["LOGON_USER"],
                            PathInfo = requestServerVariables["PATH_INFO"],
                            PathTranslated = requestServerVariables["PATH_TRANSLATED"],
                            QueryString = requestServerVariables["QUERY_STRING"],
                            RemoteAddr = requestServerVariables["REMOTE_ADDR"],
                            RemoteHost = requestServerVariables["REMOTE_HOST"],
                            RemotePort = requestServerVariables["REMOTE_PORT"],
                            RemoteUser = requestServerVariables["REMOTE_USER"],
                            RequestMethod = requestServerVariables["REQUEST_METHOD"],
                            ScriptName = requestServerVariables["SCRIPT_NAME"],
                            ServerName = requestServerVariables["SERVER_NAME"],
                            SeverPort = requestServerVariables["SERVER_PORT"],
                            ServerPortSecure = requestServerVariables["SERVER_PORT_SECURE"],
                            ServerProtocol = requestServerVariables["SERVER_PROTOCOL"],
                            ServerSoftware = requestServerVariables["SERVER_SOFTWARE"],
                            Url = requestServerVariables["URL"]

                        };

                        userFromServerVariables = !string.IsNullOrEmpty(requestServerVariables["LOGON_USER"]) ? requestServerVariables["LOGON_USER"] : requestServerVariables["AUTH_USER"];
                        hostNameFromServerVariables = requestServerVariables["LOCAL_ADDR"];
                    }

                    var exceptionLog = new ExceptionLog()
                    {
                        User = !string.IsNullOrEmpty(exceptionLogRequest.User) ? exceptionLogRequest.User : !string.IsNullOrEmpty(userFromServerVariables) ? userFromServerVariables : string.Empty,
                        AdditionalMessage = exceptionLogRequest.AdditionalMessage,
                        Host = hostNameFromServerVariables,
                        Code = Marshal.GetLastWin32Error(),
                        Error = exception.Message,
                        Details = exception.ToString(),
                        Type = exception.GetType().FullName,
                        StackTrace = exception.StackTrace,
                        Date = DateTime.Now,
                        Time = DateTime.Now,
                        CallingClass = _type.FullName,
                        CallingMethod = callerMemberName,
                        SourceFilePath = sourceFilePath,
                        SourceLineNumber = sourceLineNumber,
                        ServerVariables = serverVariables
                    };

                    _elkClient.Index(exceptionLog, client => client.Index(index).Type(type));
                }
            }
            catch (Exception ex)
            {
                Trace.Write("Exception occured while logging exception in ELK stack. The exception is "+ ex);
            }
        }

        public void Info(InfoLogRequest infoLogRequest,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                if (IsLoggingEnabled)
                {
                    string index, type;
                    if (infoLogRequest.IndexType != null)
                    {
                        index = infoLogRequest.IndexType.Index.ToLower();
                        type = infoLogRequest.IndexType.Type.ToLower();
                    }
                    else
                    {
                        index = ElkIndexTypeProvider.DefautsDefault.Index;
                        type = ElkIndexTypeProvider.DefautsDefault.Type;
                    }

                    var infoLog = new InfoLog
                    {
                        Message = infoLogRequest.Message,
                        User = infoLogRequest.User,
                        CallingClass = _type.FullName,
                        CallingMethod = callerMemberName,
                        SourceFilePath = sourceFilePath,
                        SourceLineNumber = sourceLineNumber,
                        Date = DateTime.Now,
                        Time = DateTime.Now.ToString("HH:mm:ss")
                    };

                    _elkClient.Index(infoLog, client => client.Index(index).Type(type));
                }
            }
            catch (Exception ex)
            {
                Trace.Write("Exception occured while logging information in ELK stack. The exception is " + ex);
            }
        }
    }
}
