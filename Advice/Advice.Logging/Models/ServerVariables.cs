using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Logging.Models
{
    internal class ServerVariables
    {
        public string AllHttp { get; set; }
        public string AllRaw { get; set; }
        public string ApplMdPath { get; set; }
        public string ApplPhysicalPath { get; set; }
        public string AuthPassword { get; set; }
        public string AuthType { get; set; }
        public string AuthUser { get; set; }
        public string CertificateCookie { get; set; }
        public string CertificateFlags { get; set; }
        public string CertificateIssuer { get; set; }
        public string CertificateKeySize { get; set; }
        public string CertificateSecretKeySize { get; set; }
        public string CertificateSerialNumber { get; set; }
        public string CertificateServerIssuer { get; set; }
        public string CertificateServerSubject { get; set; }
        public string CertificateSubject { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string GateWayInterface { get; set; }
        public string HttpAccept{ get; set; } 
        public string HttpAcceptEncoding { get; set; } 
        public string HttpAcceptLanguage { get; set; }
        public string HttpConnection	 { get; set; }
        public string HttpCookie { get; set; }
        public string HttpHost { get; set; }	
        public string HttpReferer { get; set; }	
        public string HttpUserAgent { get; set; }	
        public string Https { get; set; }	
        public string HttpsKeySize { get; set; }	
        public string HttpSecretKeys { get; set; }
        public string HttpsServerIssuer { get; set; }	
        public string HttpsServerSubject { get; set; }
        public string InstanceId { get; set; }	
        public string InstanceMetaPath { get; set; }	
        public string LocalAddr { get; set; }
        public string LogonUser { get; set; }
        public string PathInfo { get; set; }
        public string PathTranslated { get; set; }
        public string QueryString { get; set; }
        public string RemoteAddr { get; set; }	
        public string RemoteHost { get; set; }	
        public string RemotePort { get; set; }
        public string RemoteUser { get; set; }
        public string RequestMethod { get; set; }
        public string ScriptName { get; set; }
        public string ServerName { get; set; }
        public string SeverPort { get; set; }
        public string ServerPortSecure { get; set; }	
        public string ServerProtocol { get; set; }
        public string ServerSoftware { get; set; }
        public string Url { get; set; }
    
    }
}
